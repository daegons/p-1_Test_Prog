using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace p_1_Test_Prog
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort1, serialPort2; // 포트 객체 선언
        private System.Threading.Timer Timer_Com1Tx, Timer_Com2Tx, Timer_Com2Rx; // 데이터 전송 타이머
        private bool isReading1, isReading2;  // 데이터 읽기 상태 플래그
        private double[] FBuff_mA = new double[8]; // 전류값 저장 배열
        
        private string FCom2_TxCmd = string.Empty; // 포트 2 전송 명령
        private int FCom2_RxDelay = 100; // 포트 2 수신 대기 시간
        private int FCom2_TxDelay = 500; // 포트 2 전송 대기 시간

        // 모드 선택 플래그
        private bool isMeasurementMode = false;
        private bool isCalibrationMode = false;

        private string FCom2_RxStr = string.Empty;  // 포트 2의 수신 데이터 저장





        public Form1()
        {
            InitializeComponent();
            InitializePorts(); // 포트 초기화
            InitializeTimers(); // 타이머 초기화
            InitializeStatusIndicators(); // 통신 상태 표시 초기화
            LoadAvailablePorts(); // 사용 가능한 포트 목록 불러오기
            LoadSavedPortSettings(); // 저장된 포트 설정 불러오기
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 폼이 로드될 때 실행되는 코드
            LoadSavedPortSettings(); // 저장된 포트 설정을 로드
        }

        private void PortRefreshTimer_Tick(object sender, EventArgs e)
        {
            LoadAvailablePorts(); // 포트 목록 새로고침
        }

        // 각 포트의 SerialPort 설정 초기화
        private void InitializePorts()
        {
            serialPort1 = CreateSerialPort();
            serialPort2 = CreateSerialPort();
        }

        // SerialPort 기본 설정 함수
        private SerialPort CreateSerialPort() =>
            new SerialPort
            {
                BaudRate = 9600,
                Parity = Parity.None,
                DataBits = 8,
                StopBits = StopBits.One,
                Handshake = Handshake.None,
                ReadTimeout = 2000,
                WriteTimeout = 500
            };

        // 각 포트의 타이머 설정
        private void InitializeTimers()
        {
            Timer_Com1Tx = new System.Threading.Timer(ReadPressureData1, null, Timeout.Infinite, 250);  // 포트 1 데이터 읽기 타이머
            Timer_Com2Tx = new System.Threading.Timer(SendPort2Command, null, Timeout.Infinite, FCom2_TxDelay); // 포트 2 전송 타이머
            Timer_Com2Rx = new System.Threading.Timer(ReadPort2Data, null, Timeout.Infinite, FCom2_RxDelay); // 포트 2 수신 타이머

        }

        // 통신 상태 표시 초기화
        private void InitializeStatusIndicators()
        {
            lblRxStatus1.BackColor = Color.Gray;
            lblTxStatus1.BackColor = Color.Gray;
            lblRxStatus2.BackColor = Color.Gray;
            lblTxStatus2.BackColor = Color.Gray;
            MakeCircular(lblRxStatus1);
            MakeCircular(lblTxStatus1);
            MakeCircular(lblRxStatus2);
            MakeCircular(lblTxStatus2);
        }

        // 사용 가능한 포트를 콤보박스에 추가
        private void LoadAvailablePorts()
        {
            comboBox_ComPort1.Items.Clear();
            comboBox_ComPort2.Items.Clear();

            string[] ports = SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                try
                {
                    using (var testPort = new SerialPort(port))
                    {
                        testPort.Open();
                        testPort.Close();

                        // 열리고 닫힌 포트만 콤보박스에 추가
                        comboBox_ComPort1.Items.Add(port);
                        comboBox_ComPort2.Items.Add(port);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine($"포트 {port}는 사용 중입니다.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"포트 {port}를 열 수 없습니다: {ex.Message}");
                }
            }
        }

        // 프로그램 시작 시 저장된 포트 설정을 로드
        private void LoadSavedPortSettings()
        {
            comboBox_ComPort1.SelectedItem = Properties.Settings.Default.Port1;
            comboBox_ComPort2.SelectedItem = Properties.Settings.Default.Port2;
        }

        // 설정된 포트를 저장
        private void SavePortSettings()
        {
            Properties.Settings.Default.Port1 = comboBox_ComPort1.SelectedItem?.ToString();
            Properties.Settings.Default.Port2 = comboBox_ComPort2.SelectedItem?.ToString();
            Properties.Settings.Default.Save();
            MessageBox.Show("포트 설정이 저장되었습니다.");
        }
        // 포트 2 Modbus 메시지 생성 함수
        private byte[] CreateModbusMessage(byte functionCode, int reg, ushort[] data = null, ushort count = 1)
        {
            byte addr = 0x01;
            int dataLength = data != null ? data.Length * 2 : count * 2;
            byte[] message = new byte[5 + dataLength + 2];
            message[0] = addr;
            message[1] = functionCode;
            message[2] = (byte)(reg >> 8);
            message[3] = (byte)(reg & 0xFF);

            if (data != null)
            {
                message[4] = (byte)(data.Length >> 8);
                message[5] = (byte)(data.Length);
                for (int i = 0; i < data.Length; i++)
                {
                    message[6 + i * 2] = (byte)(data[i] >> 8);
                    message[7 + i * 2] = (byte)(data[i] & 0xFF);
                }
            }
            else
            {
                message[4] = (byte)(count >> 8);
                message[5] = (byte)(count & 0xFF);
            }

            ushort crc = CalculateCRC(message.Take(message.Length - 2).ToArray());
            message[message.Length - 2] = (byte)(crc & 0xFF);
            message[message.Length - 1] = (byte)(crc >> 8);

            return message;
        }
        // 포트 2 CRC 계산 함수
        private ushort CalculateCRC(byte[] data)
        {
            ushort crc = 0xFFFF;
            for (int pos = 0; pos < data.Length; pos++)
            {
                crc ^= (ushort)data[pos];
                for (int i = 8; i != 0; i--)
                {
                    if ((crc & 0x0001) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0xA001;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }

        // 포트 2 메시지 전송
        private void SendPort2Message(byte[] message)
        {
            if (serialPort2.IsOpen)
            {
                serialPort2.Write(message, 0, message.Length);
                Console.WriteLine("포트 2로 데이터 전송: " + BitConverter.ToString(message));
            }
            else
            {
                Console.WriteLine("포트 2가 열려 있지 않습니다.");
            }
        }
        // 포트 2에서 다중 쓰기 명령 전송
        private void Com2_MODBUS_MWrite(int reg, ushort[] aryData)
        {
            byte[] message = CreateModbusMessage(0x10, reg, aryData);
            SendPort2Message(message);
        }
        // 포트 2에서 단일 쓰기 명령 전송
        private void Com2_MODBUS_SWrite(byte addr, int reg, ushort data)
        {
            byte[] message = CreateModbusMessage(0x06, reg, new ushort[] { data });
            SendPort2Message(message);
        }
        // 포트 2에서 읽기 명령 전송
        private void Com2_MODBUS_Read(byte addr, int reg, ushort count)
        {
            byte[] message = CreateModbusMessage(0x03, reg, null, count);
            SendPort2Message(message);
        }
        // 포트 2에 명령을 주기적으로 전송
        private void SendPort2Command(object state)
        {
            if (!string.IsNullOrEmpty(FCom2_TxCmd))
            {
                SendPort2Message(System.Text.Encoding.ASCII.GetBytes(FCom2_TxCmd));
                FCom2_TxCmd = string.Empty;
            }
            else
            {
                Com2_MODBUS_Read(0x01, 0x0200, 8); // 포트 2 Modbus 데이터 읽기
            }
        }

        // 포트 2에서 주기적으로 데이터 읽기
        private void ReadPort2Data(object state)
        {
            if (serialPort2.IsOpen)
            {
                try
                {
                    Thread.Sleep(100); // 데이터 전송 후 충분한 대기시간
                    int bytesToRead = serialPort2.BytesToRead;
                    if (bytesToRead > 0)
                    {
                        byte[] buffer = new byte[bytesToRead];
                        serialPort2.Read(buffer, 0, bytesToRead);

                        // 응답 데이터가 유효한지 CRC를 통해 확인
                        if (ValidateModbusResponse(buffer))
                        {
                            ProcessModbusData(buffer);
                        }
                        else
                        {
                            Console.WriteLine("수신된 데이터의 CRC가 유효하지 않습니다.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("수신된 데이터가 없습니다.");
                    }
                }
                catch (TimeoutException)
                {
                    Console.WriteLine("포트 2 데이터 수신 시간 초과");
                }
            }
        }
        // 수신된 데이터에서 전류값 추출 및 표시
        private void ParseReceivedData(string data)
        {
            if (data.Contains("="))
            {
                string numericPart = new string(data.Where(c => char.IsDigit(c) || c == '.' || c == '-').ToArray());
                if (double.TryParse(numericPart, out double currentValue))
                {
                    Invoke((MethodInvoker)(() => txtPressureValue2.Text = $"{currentValue:F2} mA"));
                    Console.WriteLine("포트 2의 1번 채널 전류값: " + currentValue + " mA");
                }
                else
                {
                    Console.WriteLine("전류값 파싱 실패: " + data);
                }
            }
            else
            {
                Console.WriteLine("응답 데이터에 전류 정보가 포함되지 않음: " + data);
            }
        }
        // 포트 저장 버튼 클릭 시 포트 설정을 저장
        private void btnSavePorts_Click(object sender, EventArgs e) => SavePortSettings();

        // PictureBox를 원형으로 만드는 함수
        // UI 컨트롤들을 원형으로 설정하는 함수
        private void MakeCircular(PictureBox pictureBox)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, pictureBox.Width - 1, pictureBox.Height - 1);
            pictureBox.Region = new Region(gp);

            pictureBox.Paint += (sender, e) =>
            {
                using (Pen pen = new Pen(Color.DarkGray, 3))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawEllipse(pen, 1, 1, pictureBox.Width - 3, pictureBox.Height - 3);
                }
            };
        }

        // 수신된 Modbus 데이터를 처리하고 표시
        private void ProcessModbusData(byte[] data)
        {
            // Modbus 응답 예: [장치 주소][기능 코드][데이터 길이][데이터][CRC]
            int byteCount = data[2]; // 데이터 길이 바이트 추출
            byte[] payload = data.Skip(3).Take(byteCount).ToArray(); // 실제 데이터 추출

            // 예를 들어, 첫 번째 2바이트가 전류값이라 가정
            double currentValue = BitConverter.ToInt16(payload, 0) * 0.001; // mA로 변환
            Invoke((MethodInvoker)(() => txtPressureValue2.Text = $"{currentValue:F2} mA"));
            Console.WriteLine("포트 2의 1번 채널 전류값: " + currentValue + " mA");
        }

        // 수신된 데이터의 CRC 유효성 검사
        private bool ValidateModbusResponse(byte[] data)
        {
            int dataLength = data.Length;
            if (dataLength < 5) return false; // 최소 길이 확인

            ushort receivedCrc = BitConverter.ToUInt16(data, dataLength - 2); // 마지막 2바이트 CRC
            ushort calculatedCrc = CalculateCRC(data.Take(dataLength - 2).ToArray()); // CRC 계산

            return receivedCrc == calculatedCrc;
        }

        // Start 버튼 클릭: 선택된 모드에 따라 작업 수행
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (isMeasurementMode)
            {
                EnsurePortIsOpen(serialPort1, comboBox_ComPort1, ref isReading1, Timer_Com1Tx, 300, "Port 1");
                StartMeasurementMode();
            }
            else if (isCalibrationMode)
            {
                StopMeasurementMode(); // 교정 모드 시작 전에 측정 모드 중지

                EnsurePortIsOpen(serialPort1, comboBox_ComPort1, ref isReading1, Timer_Com1Tx, 300, "Port 1");
                EnsurePortIsOpen(serialPort2, comboBox_ComPort2, ref isReading2, Timer_Com2Tx, 500, "Port 2");
                CalibrateOutputMode();
            }
            else
            {
                MessageBox.Show("먼저 측정 모드 또는 교정 모드를 선택하세요.");
            }
        }

        // 포트를 열기 위한 함수
        private void EnsurePortIsOpen(SerialPort port, ComboBox comboBox, ref bool isReading, System.Threading.Timer timer, int interval, string portName)
        {
            try
            {
                if (!port.IsOpen && comboBox.SelectedItem != null)
                {
                    port.PortName = comboBox.SelectedItem.ToString();
                    port.Open();
                    isReading = true;
                    timer.Change(0, interval); // 포트가 열리면 타이머 시작
                    Console.WriteLine($"{portName}에서 데이터 읽기 시작.");
                }
                else if (!port.IsOpen)
                {
                    MessageBox.Show($"{portName} 포트를 선택 후 다시 시도하세요.");
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"포트를 여는 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        // Stop 버튼 클릭: 모드 중지 및 모든 포트 통신 중지
        private void btnStop_Click(object sender, EventArgs e)
        {
            StopMeasurementMode(); // 측정 모드 중지
            StopPortCommunication(serialPort1, ref isReading1, Timer_Com1Tx);
            StopPortCommunication(serialPort2, ref isReading2, Timer_Com2Tx);
            isMeasurementMode = false;
            isCalibrationMode = false;
            Console.WriteLine("모든 포트로부터 데이터 읽기 중지.");

        }

        // 포트 통신 중지 함수: 포트를 닫고 타이머를 중지
        private void StopPortCommunication(SerialPort port, ref bool isReading, System.Threading.Timer timer)
        {
            isReading = false;
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            if (port.IsOpen)
            {
                try
                {
                    port.Close();
                }
                catch (IOException ex)
                {
                    MessageBox.Show($"포트를 닫는 중 오류가 발생했습니다: {ex.Message}");
                }
            }
        }

        // 포트 1의 주기적 데이터 읽기 함수
        private void ReadPressureData1(object state) => ReadPressureData(serialPort1, isReading1, lblTxStatus1, lblRxStatus1, txtPressureValue1, "차압계");

        // 포트 2의 주기적 데이터 읽기 함수
        private void ReadPressureData2(object state)
        {
            ReadPressureData(serialPort2, isReading2, lblTxStatus2, lblRxStatus2, txtPressureValue2, "I/O보드");
        }
        // 주어진 포트에서 데이터를 읽고 수신 및 송신 상태를 업데이트
        private void ReadPressureData(SerialPort port, bool isReading, PictureBox lblTxStatus, PictureBox lblRxStatus, Label outputLabel, string pcbName)
        {
            if (!isReading) return;

            try
            {
                if (port.IsOpen)
                {
                    UpdateTxStatus(lblTxStatus, true); // TX 상태 업데이트 (빨간색)
                    port.WriteLine("#G");  // 데이터 요청 명령어 전송
                    Thread.Sleep(100);  // 송신 후 대기
                    UpdateTxStatus(lblTxStatus, false);  // TX 상태 해제 (회색)

                    string response = port.ReadExisting();  // 수신 데이터 읽기

                    if (!string.IsNullOrEmpty(response))
                    {
                        UpdateRxStatus(lblRxStatus, true);  // 수신 데이터가 있을 경우 초록색
                        Console.WriteLine(response);
                        outputLabel.Invoke((MethodInvoker)(() => outputLabel.Text = ParsePressureValue(response))); // 수신 값 표시
                    }
                    else
                    {
                        UpdateRxStatus(lblRxStatus, false);  // 수신 데이터가 없을 경우 회색
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{pcbName} 오류: " + ex.Message);
            }
        }

        // TX 상태 업데이트 메서드
        private void UpdateTxStatus(PictureBox lblTxStatus, bool isActive)
        {
            lblTxStatus.BackColor = isActive ? Color.Red : Color.DarkGray;
        }

        // RX 상태 업데이트 메서드
        private void UpdateRxStatus(PictureBox lblRxStatus, bool isActive)
        {
            lblRxStatus.BackColor = isActive ? Color.Green : Color.DarkGray;
        }



        // 압력 값을 레이블에 업데이트하는 메서드
        private void UpdatePressureValue(Label label, string value)
        {
            if (label.InvokeRequired)
                label.BeginInvoke(new Action(() => { label.Text = value; }));
            else
                label.Text = value;
        }

        // 압력 값을 파싱하여 문자열로 반환
        private string ParsePressureValue(string response)
        {
            string numericPart = new string(response.Where(c => char.IsDigit(c) || c == '.' || c == '-' || c == '+').ToArray());
            return decimal.TryParse(numericPart, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal pressureValue)
                ? pressureValue.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)
                : "잘못된 응답";
        }

        // 종료 버튼 클릭 시 프로그램 종료
        private void btnExit_Click(object sender, EventArgs e) => Application.Exit();


        // 교정 모드 버튼 클릭 이벤트
        private void btnCalibrate_Click(object sender, EventArgs e)
        {
            isCalibrationMode = true;
            isMeasurementMode = false;
            MessageBox.Show("교정 모드가 선택되었습니다. 'Start' 버튼을 눌러 시작하세요.");
        }

        // 교정 모드 실행 함수
        private void CalibrateOutputMode()
        {
            StopMeasurementMode(); // 측정 모드가 켜져 있으면 중지

            // 포트 2에서 4.00mA 출력 설정
            double referenceValue4 = 4.00;
            SetReferenceCurrent(referenceValue4, serialPort2); // 포트 2에 4mA 명령 전송
            Thread.Sleep(500); // 명령 전송 후 충분한 대기 시간 추가

            // 포트 1에서 4mA에 따른 측정값 읽기
            double measuredValue4_Port1 = GetMeasuredCurrent(serialPort1); // 포트 1에서 측정값 읽기
            double error4 = CalculateError(referenceValue4, measuredValue4_Port1);
            DisplayCalibrationResult(referenceValue4, measuredValue4_Port1, error4, txtReferenceValue4, txtMeasuredValue4, txtErrorValue4);

            // 4mA 명령이 적용되었는지 로그 출력
            Console.WriteLine("4mA 출력 설정 및 측정 완료");

            // 포트 2에서 19.5mA 출력 설정
            double referenceValue19_5 = 19.5;
            SetReferenceCurrent(referenceValue19_5, serialPort2); // 포트 2에 19.5mA 명령 전송
            Thread.Sleep(500); // 명령 전송 후 충분한 대기 시간 추가

            // 포트 1에서 19.5mA에 따른 측정값 읽기
            double measuredValue19_5_Port1 = GetMeasuredCurrent(serialPort1); // 포트 1에서 측정값 읽기
            double error19_5 = CalculateError(referenceValue19_5, measuredValue19_5_Port1);
            DisplayCalibrationResult(referenceValue19_5, measuredValue19_5_Port1, error19_5, txtReferenceValue19_5, txtMeasuredValue19_5, txtErrorValue19_5);

            // 19.5mA 명령이 적용되었는지 로그 출력
            Console.WriteLine("19.5mA 출력 설정 및 측정 완료");
        }

        // 기준 전류를 설정하는 메서드 (명령을 통해 4.00mA 또는 19.5mA 출력 설정)
        private void SetReferenceCurrent(double referenceValue, SerialPort port)
        {
            string command = referenceValue == 4.00 ? "#T0" : "#T1";
            int retryCount = 3;

            try
            {
                if (port.IsOpen)
                {
                    port.DiscardInBuffer(); // 입력 버퍼 비우기

                    for (int attempt = 0; attempt < retryCount; attempt++)
                    {
                        port.WriteLine(command);
                        Console.WriteLine($"명령 전송 성공: {command}");

                        Thread.Sleep(1000); // 더 긴 대기 시간

                        string response = port.ReadExisting(); // 응답 읽기
                        Console.WriteLine($"수신된 응답: {response}");

                        if (!string.IsNullOrEmpty(response) && response.Contains("="))
                        {
                            UpdateRxStatus(lblRxStatus2, true); // 수신 성공 상태
                            txtPressureValue2.Invoke((MethodInvoker)(() => txtPressureValue2.Text = ParsePressureValue(response)));
                            break;
                        }
                        else
                        {
                            UpdateRxStatus(lblRxStatus2, false); // 수신 실패 상태
                            Console.WriteLine("응답 재시도 중...");
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"{port.PortName}가 열려 있지 않습니다. 포트를 확인하세요.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"포트를 통해 명령을 전송하는 중 오류가 발생했습니다: {ex.Message}");
            }
        }

        // 현재 측정된 전류값을 얻는 메서드
        private double GetMeasuredCurrent(SerialPort port)
        {
            string response = port.ReadExisting();
            Console.WriteLine($"수신된 응답: {response}"); // 수신 응답 로그
            return ParseCurrentValue(response);
        }

        // 수신된 데이터를 파싱하여 전류 값으로 변환
        private double ParseCurrentValue(string response)
        {
            string numericPart = new string(response.Where(c => char.IsDigit(c) || c == '.' || c == '-' || c == '+').ToArray());
            return double.TryParse(numericPart, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double currentValue)
                ? currentValue
                : 0.0;
        }


        // 오차를 계산하는 메서드
        private double CalculateError(double reference, double measured) => reference - measured;

        // 교정 결과를 UI에 표시하는 메서드
        private void DisplayCalibrationResult(double reference, double measured, double error, TextBox txtReference, TextBox txtMeasured, TextBox txtError)
        {
            txtReference.Text = $"{reference:F2} mA";
            txtMeasured.Text = $"{measured:F2} mA";
            txtError.Text = $"{error:F2} mA";
        }

        // 측정 모드 버튼 클릭 이벤트
        private void btnMeasure_Click(object sender, EventArgs e)
        {
            isMeasurementMode = true;
            isCalibrationMode = false;
            MessageBox.Show("측정 모드가 선택되었습니다. 'Start' 버튼을 눌러 시작하세요.");
        }

        // 측정 모드 시작 함수
        private void StartMeasurementMode()
        {
            if (serialPort1.IsOpen)
            {
                Timer_Com1Tx.Change(0, 300); // 주기적인 데이터 읽기를 위한 타이머 시작
                Console.WriteLine("측정 모드가 시작되었습니다.");
            }
            else
            {
                MessageBox.Show("측정을 위해 포트 1이 열려 있는지 확인하세요.");
            }
        }
        // 측정 모드 중지 함수
        private void StopMeasurementMode()
        {
            Timer_Com1Tx.Change(Timeout.Infinite, Timeout.Infinite); // 측정 타이머를 중지
            Console.WriteLine("측정 모드가 중지되었습니다.");
        }

    }
}







