using System;
using System.IO.Ports;  // SerialPort 클래스 사용
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;  // Debug.WriteLine()을 사용하기 위한 네임스페이스
using System.IO;  // 파일 로그를 사용하기 위한 네임스페이스
using System.Drawing.Drawing2D;  // Region과 GraphicsPath 사용을 위해 필요
using System.Drawing;

namespace p_1_Test_Prog
{
    public partial class Form1 : Form
    {
        // 포트 및 타이머 객체 선언
        private SerialPort serialPort1, serialPort2;
        private System.Threading.Timer Timer_Com1Tx, Timer_Com2Tx;
        private bool isReading1, isReading2;  // 데이터 읽기 상태 플래그

        public Form1()
        {
            InitializeComponent();
            InitializePorts();  // 포트 설정 초기화
            InitializeTimers();  // 타이머 초기화
            InitializeStatusIndicators();  // 통신 상태 표시 초기화
            LoadAvailablePorts();  // 사용 가능한 포트 목록 로드
            LoadSavedPortSettings();  // 이전에 저장된 포트 설정 로드
        }

        // 각 포트의 SerialPort 설정
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
                ReadTimeout = 500,
                WriteTimeout = 500
            };

        // 각 포트에 대한 타이머 설정
        private void InitializeTimers()
        {
            Timer_Com1Tx = new System.Threading.Timer(ReadPressureData1, null, Timeout.Infinite, 250);  // 포트 1 데이터 읽기 타이머
            Timer_Com2Tx = new System.Threading.Timer(ReadPressureData2, null, Timeout.Infinite, 500);  // 포트 2 데이터 읽기 타이머
        }

        // 통신 상태 표시 (초기 색상 설정 및 원형 표시)
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
            string[] ports = SerialPort.GetPortNames();
            comboBox_ComPort1.Items.AddRange(ports);
            comboBox_ComPort2.Items.AddRange(ports);
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

        // 포트 저장 버튼 클릭 시 포트 설정 저장
        private void btnSavePorts_Click(object sender, EventArgs e) => SavePortSettings();

        // PictureBox를 원형으로 만드는 함수
        private void MakeCircular(PictureBox pictureBox)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, pictureBox.Width - 1, pictureBox.Height - 1);
            pictureBox.Region = new Region(gp);
        }

        // Start 버튼 클릭: 각 포트의 데이터 읽기 시작
        private void btnStart_Click(object sender, EventArgs e)
        {
            StartPortCommunication(serialPort1, comboBox_ComPort1, ref isReading1, Timer_Com1Tx, 250, "Port 1");
            StartPortCommunication(serialPort2, comboBox_ComPort2, ref isReading2, Timer_Com2Tx, 500, "Port 2");

            if (comboBox_ComPort1.SelectedItem == null && comboBox_ComPort2.SelectedItem == null)
                MessageBox.Show("통신을 시작할 포트를 선택하세요.");
        }

        // 포트 통신 시작 함수: 지정된 포트가 열리고, 타이머가 설정된 인터벌로 작동하도록 설정
        private void StartPortCommunication(SerialPort port, ComboBox comboBox, ref bool isReading, System.Threading.Timer timer, int interval, string portName)
        {
            if (comboBox.SelectedItem != null && !port.IsOpen)
            {
                port.PortName = comboBox.SelectedItem.ToString();
                port.Open();
                isReading = true;
                timer.Change(0, interval);
                Debug.WriteLine($"{portName}에서 데이터 읽기 시작.");
            }
        }

        // Stop 버튼 클릭: 포트 통신 중지
        private void btnStop_Click(object sender, EventArgs e)
        {
            StopPortCommunication(serialPort1, ref isReading1, Timer_Com1Tx);
            StopPortCommunication(serialPort2, ref isReading2, Timer_Com2Tx);
            Debug.WriteLine("모든 PCB로부터 데이터 읽기 중지.");
        }

        // 포트 통신 중지 함수: 포트를 닫고 타이머를 중지
        private void StopPortCommunication(SerialPort port, ref bool isReading, System.Threading.Timer timer)
        {
            isReading = false;
            timer.Change(Timeout.Infinite, Timeout.Infinite);
            if (port.IsOpen) port.Close();
        }

        // 포트 1의 주기적 데이터 읽기 함수
        private void ReadPressureData1(object state) => ReadPressureData(serialPort1, isReading1, lblTxStatus1, lblRxStatus1, txtPressureValue1, "PCB 1");

        // 포트 2의 주기적 데이터 읽기 함수
        private void ReadPressureData2(object state) => ReadPressureData(serialPort2, isReading2, lblTxStatus2, lblRxStatus2, txtPressureValue2, "PCB 2");

        // 주어진 포트에서 데이터를 읽고 수신 및 송신 상태를 업데이트
        private void ReadPressureData(SerialPort port, bool isReading, PictureBox lblTxStatus, PictureBox lblRxStatus, Label outputLabel, string pcbName)
        {
            if (!isReading) return;

            try
            {
                if (port.IsOpen)
                {
                    UpdateStatus(lblTxStatus, true);  // TX 상태 업데이트 (빨간색)
                    port.WriteLine("#G");  // 데이터 요청 명령어 전송
                    Thread.Sleep(100);  // 송신 후 대기
                    UpdateStatus(lblTxStatus, false);  // TX 상태 해제 (회색)

                    string response = port.ReadExisting();  // 수신 데이터 읽기
                    UpdateStatus(lblRxStatus, !string.IsNullOrEmpty(response));  // RX 상태 업데이트

                    if (!string.IsNullOrEmpty(response))
                    {
                        Debug.WriteLine($"{pcbName} mH2O: " + response);
                        Console.WriteLine($"{pcbName} mH2O: " + response);
                        UpdatePressureValue(outputLabel, ParsePressureValue(response));  // 수신 값 표시
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{pcbName} 오류: " + ex.Message);
            }
        }

        // 상태 표시를 업데이트 (활성화 시 초록색, 비활성화 시 회색)
        private void UpdateStatus(PictureBox statusIndicator, bool isActive)
        {
            statusIndicator.BackColor = isActive ? Color.Green : Color.Gray;
        }

        // 압력 값을 레이블에 업데이트하는 메서드
        private void UpdatePressureValue(Label label, string value)
        {
            if (label.InvokeRequired)
                label.BeginInvoke(new Action(() => { label.Text = value; }));
            else
                label.Text = value;
        }

        // 수신된 데이터를 파싱하여 압력 값으로 변환
        private string ParsePressureValue(string response)
        {
            string numericPart = new string(response.Where(c => char.IsDigit(c) || c == '.' || c == '-' || c == '+').ToArray());
            return decimal.TryParse(numericPart, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal pressureValue)
                ? pressureValue.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)
                : "잘못된 응답";
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // 폼이 로드될 때 실행될 코드 추가
            LoadSavedPortSettings(); // 저장된 포트 설정 로드
        }

        // 종료 버튼 클릭 시 프로그램 종료
        private void btnExit_Click(object sender, EventArgs e) => Application.Exit();
    }
}
