using System;
using System.IO.Ports;  // SerialPort 클래스 사용
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;  // Debug.WriteLine()을 사용하기 위한 네임스페이스
using System.IO;  // 파일 로그를 사용하기 위한 네임스페이스
using System.Drawing.Drawing2D;  // Region과 GraphicsPath 사용을 위해 필요
using System.Drawing;
using System.Timers;

namespace p_1_Test_Prog
{
    public partial class Form1 : Form
    {
        private SerialPort serialPort;
        private System.Threading.Timer Timer_Com2Tx;  // 데이터를 주기적으로 읽기 위한 타이머
        private bool isReading = false;  // 데이터 읽기 중인지 확인하는 플래그
        private string FCom2_TxCmd;
        private bool isTimerRunning = false; // 타이머 실행 상태를 추적할 변수
        public Form1()
        {
            InitializeComponent();

            // SerialPort 설정
            serialPort = new SerialPort();
            serialPort.PortName = "COM12";   // 사용 중인 COM 포트 설정
            serialPort.BaudRate = 9600;     // 차압계의 BaudRate 동일 적용
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;
            serialPort.ReadTimeout = 500;
            serialPort.WriteTimeout = 500;

            // 타이머 설정 (주기적으로 데이터를 읽음, 기본 250ms 간격)
            Timer_Com2Tx = new System.Threading.Timer(ReadPressureData, null, Timeout.Infinite, 250);

            // 초기 통신 상태 표시
            lblRxStatus.BackColor = System.Drawing.Color.Gray; // RX 초기 상태
            lblTxStatus.BackColor = System.Drawing.Color.Gray; // TX 초기 상태

            // PictureBox를 원형으로 만드는 메서드 호출
            MakeCircular(lblRxStatus);
            MakeCircular(lblTxStatus);
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            // 폼이 로드될 때 실행될 코드를 여기에 작성
            // 예: 폼 초기화 코드
        }

        // PictureBox를 원형으로 만드는 메서드
        private void MakeCircular(PictureBox pictureBox)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddEllipse(0, 0, pictureBox.Width - 1, pictureBox.Height - 1);
            pictureBox.Region = new Region(gp);
        }

        // Start 버튼 클릭: 데이터 읽기 시작
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                serialPort.Open();  // 포트가 열려 있지 않으면 한 번만 연다
            }

            isReading = true;
            Timer_Com2Tx.Change(0, 250);  // 250ms 간격으로 데이터 읽기 시작
            Debug.WriteLine("Started Reading Data.");
        }

        // Stop 버튼 클릭: 데이터 읽기 중지
        private void btnStop_Click(object sender, EventArgs e)
        {
            isReading = false;
            Timer_Com2Tx.Change(Timeout.Infinite, Timeout.Infinite);  // 타이머 중지
            Debug.WriteLine("Timer Stopped.");
            Console.WriteLine("Stop");
            if (serialPort.IsOpen)
            {
                serialPort.Close();  // 포트를 닫는다
            }
            Debug.WriteLine("Stopped Reading Data.");
        }

        // 주기적으로 데이터를 읽는 타이머 콜백 함수
        private void ReadPressureData(object state)
        {
            try
            {
                if (serialPort.IsOpen)
                {
                    // TX 표시: 데이터를 송신 중임을 표시 (빨간색)
                    UpdateTxStatus(true);

                    // 차압계에 값을 요청하는 명령어 전송
                    string command = "#G";
                    serialPort.WriteLine(command);

                    // 잠시 대기 후 TX 상태 해제
                    Thread.Sleep(100);
                    UpdateTxStatus(false);

                    // RX 표시: 데이터를 수신 중임을 표시 (초록색)
                    UpdateRxStatus(true);

                    // 차압계로부터 응답 읽기
                    string response = serialPort.ReadLine();

                    // RX 상태 해제
                    UpdateRxStatus(false);

                    // Debug 창에 값 출력
                    Debug.WriteLine("mH2O: " + response);

                    // 파일로 로그 남기기
                    LogToFile("mH2O: " + response);

                    // 응답을 처리 (예: 압력 값 추출 및 표시)
                    UpdatePressureValue(ParsePressureValue(response));

                    // 콘솔 창에 출력
                    Console.WriteLine("mH2O: " + response);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
        }

        // TX 상태 업데이트 메서드
        private void UpdateTxStatus(bool isActive)
        {
            lblTxStatus.BackColor = isActive ? System.Drawing.Color.Red : System.Drawing.Color.Gray;
        }

        // RX 상태 업데이트 메서드
        private void UpdateRxStatus(bool isActive)
        {
            lblRxStatus.BackColor = isActive ? System.Drawing.Color.Green : System.Drawing.Color.Gray;
        }

        // 파일로 로그 남기는 메서드
        private void LogToFile(string message)
        {
            File.AppendAllText("log.txt", DateTime.Now + " - " + message + Environment.NewLine);
        }

        // UI 스레드에서 안전하게 텍스트박스를 업데이트하는 메서드
        private void UpdatePressureValue(string value)
        {
            if (txtPressureValue.InvokeRequired)
            {
                txtPressureValue.BeginInvoke(new Action(() =>
                {
                    txtPressureValue.Text = value;
                }));
            }
            else
            {
                txtPressureValue.Text = value;
            }
        }

        private string ParsePressureValue(string response)
        {
            if (string.IsNullOrWhiteSpace(response) || response.Length < 2)
            {
                return "Invalid response";
            }

            string numericPart = new string(response.Where(c => char.IsDigit(c) || c == '.' || c == '-' || c == '+').ToArray());

            if (decimal.TryParse(numericPart, System.Globalization.NumberStyles.Any,
                                 System.Globalization.CultureInfo.InvariantCulture,
                                 out decimal pressureValue))
            {
                return pressureValue.ToString("F2", System.Globalization.CultureInfo.InvariantCulture);
            }

            return "Invalid response";
        }

        // 종료 버튼 클릭: 프로그램 종료
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();  // 프로그램 종료
        }

    }
}
