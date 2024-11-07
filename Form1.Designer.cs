namespace p_1_Test_Prog
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRxStatus1 = new System.Windows.Forms.PictureBox();
            this.lblTxStatus1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtPressureValue1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblTxStatus2 = new System.Windows.Forms.PictureBox();
            this.lblRxStatus2 = new System.Windows.Forms.PictureBox();
            this.comboBox_ComPort1 = new System.Windows.Forms.ComboBox();
            this.comboBox_ComPort2 = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtPressureValue2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btnCalibrate = new System.Windows.Forms.Button();
            this.txtReferenceValue4 = new System.Windows.Forms.TextBox();
            this.txtErrorValue4 = new System.Windows.Forms.TextBox();
            this.txtMeasuredValue4 = new System.Windows.Forms.TextBox();
            this.txtReferenceValue19_5 = new System.Windows.Forms.TextBox();
            this.txtMeasuredValue19_5 = new System.Windows.Forms.TextBox();
            this.txtErrorValue19_5 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnMeasure = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lblRxStatus1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTxStatus1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTxStatus2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRxStatus2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStop
            // 
            this.btnStop.Font = new System.Drawing.Font("돋움", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStop.Location = new System.Drawing.Point(496, 269);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(152, 48);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림체", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(178, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "p-1 프로그램(작업중)";
            // 
            // lblRxStatus1
            // 
            this.lblRxStatus1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblRxStatus1.Location = new System.Drawing.Point(571, 104);
            this.lblRxStatus1.Name = "lblRxStatus1";
            this.lblRxStatus1.Size = new System.Drawing.Size(13, 13);
            this.lblRxStatus1.TabIndex = 4;
            this.lblRxStatus1.TabStop = false;
            // 
            // lblTxStatus1
            // 
            this.lblTxStatus1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTxStatus1.Location = new System.Drawing.Point(610, 104);
            this.lblTxStatus1.Name = "lblTxStatus1";
            this.lblTxStatus1.Size = new System.Drawing.Size(13, 13);
            this.lblTxStatus1.TabIndex = 5;
            this.lblTxStatus1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림체", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(606, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "TX";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("굴림체", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(566, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 21);
            this.label4.TabIndex = 8;
            this.label4.Text = "RX";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Font = new System.Drawing.Font("굴림체", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(12, 85);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.MaximumSize = new System.Drawing.Size(200, 150);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(3);
            this.label5.Size = new System.Drawing.Size(178, 33);
            this.label5.TabIndex = 10;
            this.label5.Text = "측정값";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnExit
            // 
            this.btnExit.Font = new System.Drawing.Font("돋움", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnExit.Location = new System.Drawing.Point(496, 365);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(152, 48);
            this.btnExit.TabIndex = 11;
            this.btnExit.Text = "프로그램 종료";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 438);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(660, 22);
            this.statusStrip1.TabIndex = 12;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtPressureValue1
            // 
            this.txtPressureValue1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPressureValue1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtPressureValue1.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPressureValue1.Location = new System.Drawing.Point(12, 123);
            this.txtPressureValue1.Name = "txtPressureValue1";
            this.txtPressureValue1.Size = new System.Drawing.Size(178, 29);
            this.txtPressureValue1.TabIndex = 14;
            this.txtPressureValue1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(565, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "차압계 통신";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(565, 123);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 20;
            this.label7.Text = "I/O보드 통신";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("굴림체", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.Location = new System.Drawing.Point(566, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 21);
            this.label8.TabIndex = 19;
            this.label8.Text = "RX";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("굴림체", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.Location = new System.Drawing.Point(606, 135);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 21);
            this.label9.TabIndex = 18;
            this.label9.Text = "TX";
            // 
            // lblTxStatus2
            // 
            this.lblTxStatus2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblTxStatus2.Location = new System.Drawing.Point(610, 159);
            this.lblTxStatus2.Name = "lblTxStatus2";
            this.lblTxStatus2.Size = new System.Drawing.Size(13, 13);
            this.lblTxStatus2.TabIndex = 17;
            this.lblTxStatus2.TabStop = false;
            // 
            // lblRxStatus2
            // 
            this.lblRxStatus2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblRxStatus2.Location = new System.Drawing.Point(571, 159);
            this.lblRxStatus2.Name = "lblRxStatus2";
            this.lblRxStatus2.Size = new System.Drawing.Size(13, 13);
            this.lblRxStatus2.TabIndex = 16;
            this.lblRxStatus2.TabStop = false;
            // 
            // comboBox_ComPort1
            // 
            this.comboBox_ComPort1.FormattingEnabled = true;
            this.comboBox_ComPort1.Location = new System.Drawing.Point(448, 86);
            this.comboBox_ComPort1.Name = "comboBox_ComPort1";
            this.comboBox_ComPort1.Size = new System.Drawing.Size(92, 20);
            this.comboBox_ComPort1.TabIndex = 21;
            // 
            // comboBox_ComPort2
            // 
            this.comboBox_ComPort2.FormattingEnabled = true;
            this.comboBox_ComPort2.Location = new System.Drawing.Point(448, 130);
            this.comboBox_ComPort2.Name = "comboBox_ComPort2";
            this.comboBox_ComPort2.Size = new System.Drawing.Size(92, 20);
            this.comboBox_ComPort2.TabIndex = 22;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(446, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(74, 12);
            this.label10.TabIndex = 23;
            this.label10.Text = "PORT1(P-1)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(446, 115);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 12);
            this.label11.TabIndex = 24;
            this.label11.Text = "PORT2(I/O 보드)";
            // 
            // txtPressureValue2
            // 
            this.txtPressureValue2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPressureValue2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtPressureValue2.Font = new System.Drawing.Font("굴림", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtPressureValue2.Location = new System.Drawing.Point(196, 123);
            this.txtPressureValue2.Name = "txtPressureValue2";
            this.txtPressureValue2.Size = new System.Drawing.Size(178, 29);
            this.txtPressureValue2.TabIndex = 25;
            this.txtPressureValue2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label12.Font = new System.Drawing.Font("굴림체", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.Location = new System.Drawing.Point(196, 85);
            this.label12.Margin = new System.Windows.Forms.Padding(3);
            this.label12.MaximumSize = new System.Drawing.Size(200, 150);
            this.label12.Name = "label12";
            this.label12.Padding = new System.Windows.Forms.Padding(3);
            this.label12.Size = new System.Drawing.Size(178, 33);
            this.label12.TabIndex = 26;
            this.label12.Text = "I/O보드";
            this.label12.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnCalibrate
            // 
            this.btnCalibrate.Location = new System.Drawing.Point(104, 331);
            this.btnCalibrate.Name = "btnCalibrate";
            this.btnCalibrate.Size = new System.Drawing.Size(86, 43);
            this.btnCalibrate.TabIndex = 27;
            this.btnCalibrate.Text = "교정 모드";
            this.btnCalibrate.UseVisualStyleBackColor = true;
            this.btnCalibrate.Click += new System.EventHandler(this.btnCalibrate_Click);
            // 
            // txtReferenceValue4
            // 
            this.txtReferenceValue4.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtReferenceValue4.Location = new System.Drawing.Point(30, 253);
            this.txtReferenceValue4.Name = "txtReferenceValue4";
            this.txtReferenceValue4.Size = new System.Drawing.Size(57, 32);
            this.txtReferenceValue4.TabIndex = 28;
            // 
            // txtErrorValue4
            // 
            this.txtErrorValue4.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtErrorValue4.Location = new System.Drawing.Point(155, 253);
            this.txtErrorValue4.Name = "txtErrorValue4";
            this.txtErrorValue4.Size = new System.Drawing.Size(57, 32);
            this.txtErrorValue4.TabIndex = 29;
            // 
            // txtMeasuredValue4
            // 
            this.txtMeasuredValue4.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtMeasuredValue4.Location = new System.Drawing.Point(93, 253);
            this.txtMeasuredValue4.Name = "txtMeasuredValue4";
            this.txtMeasuredValue4.Size = new System.Drawing.Size(57, 32);
            this.txtMeasuredValue4.TabIndex = 30;
            // 
            // txtReferenceValue19_5
            // 
            this.txtReferenceValue19_5.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtReferenceValue19_5.Location = new System.Drawing.Point(217, 253);
            this.txtReferenceValue19_5.Name = "txtReferenceValue19_5";
            this.txtReferenceValue19_5.Size = new System.Drawing.Size(57, 32);
            this.txtReferenceValue19_5.TabIndex = 31;
            // 
            // txtMeasuredValue19_5
            // 
            this.txtMeasuredValue19_5.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtMeasuredValue19_5.Location = new System.Drawing.Point(280, 253);
            this.txtMeasuredValue19_5.Name = "txtMeasuredValue19_5";
            this.txtMeasuredValue19_5.Size = new System.Drawing.Size(57, 32);
            this.txtMeasuredValue19_5.TabIndex = 32;
            // 
            // txtErrorValue19_5
            // 
            this.txtErrorValue19_5.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtErrorValue19_5.Location = new System.Drawing.Point(343, 253);
            this.txtErrorValue19_5.Name = "txtErrorValue19_5";
            this.txtErrorValue19_5.Size = new System.Drawing.Size(57, 32);
            this.txtErrorValue19_5.TabIndex = 33;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(28, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 25);
            this.label3.TabIndex = 34;
            this.label3.Text = "  4mA      기준값";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label13.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label13.Location = new System.Drawing.Point(91, 225);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(59, 25);
            this.label13.TabIndex = 35;
            this.label13.Text = "  4mA      측정값";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label14.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label14.Location = new System.Drawing.Point(156, 225);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(59, 25);
            this.label14.TabIndex = 36;
            this.label14.Text = "  4mA      오차값";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label15.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label15.Location = new System.Drawing.Point(221, 225);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 25);
            this.label15.TabIndex = 37;
            this.label15.Text = "  19.5mA    기준값";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label18.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label18.Location = new System.Drawing.Point(278, 225);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(59, 25);
            this.label18.TabIndex = 40;
            this.label18.Text = "  19.5mA    측정값";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label16.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label16.Location = new System.Drawing.Point(343, 225);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(59, 25);
            this.label16.TabIndex = 41;
            this.label16.Text = "  19.5mA    오차값";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnStart.Location = new System.Drawing.Point(496, 215);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(152, 48);
            this.btnStart.TabIndex = 42;
            this.btnStart.Text = "START";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnMeasure
            // 
            this.btnMeasure.Location = new System.Drawing.Point(12, 381);
            this.btnMeasure.Name = "btnMeasure";
            this.btnMeasure.Size = new System.Drawing.Size(86, 43);
            this.btnMeasure.TabIndex = 43;
            this.btnMeasure.Text = "측정 모드";
            this.btnMeasure.UseVisualStyleBackColor = true;
            this.btnMeasure.Click += new System.EventHandler(this.btnMeasure_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(314, 365);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 43);
            this.button1.TabIndex = 44;
            this.button1.Text = "I/O_board";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(660, 460);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnMeasure);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtErrorValue19_5);
            this.Controls.Add(this.txtMeasuredValue19_5);
            this.Controls.Add(this.txtReferenceValue19_5);
            this.Controls.Add(this.txtMeasuredValue4);
            this.Controls.Add(this.txtErrorValue4);
            this.Controls.Add(this.txtReferenceValue4);
            this.Controls.Add(this.btnCalibrate);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtPressureValue2);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.comboBox_ComPort2);
            this.Controls.Add(this.comboBox_ComPort1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblTxStatus2);
            this.Controls.Add(this.lblRxStatus2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtPressureValue1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTxStatus1);
            this.Controls.Add(this.lblRxStatus1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStop);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lblRxStatus1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTxStatus1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTxStatus2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRxStatus2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox lblRxStatus1;
        private System.Windows.Forms.PictureBox lblTxStatus1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label txtPressureValue1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox lblTxStatus2;
        private System.Windows.Forms.PictureBox lblRxStatus2;
        private System.Windows.Forms.ComboBox comboBox_ComPort1;
        private System.Windows.Forms.ComboBox comboBox_ComPort2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label txtPressureValue2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnCalibrate;
        private System.Windows.Forms.TextBox txtReferenceValue4;
        private System.Windows.Forms.TextBox txtErrorValue4;
        private System.Windows.Forms.TextBox txtMeasuredValue4;
        private System.Windows.Forms.TextBox txtReferenceValue19_5;
        private System.Windows.Forms.TextBox txtMeasuredValue19_5;
        private System.Windows.Forms.TextBox txtErrorValue19_5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnMeasure;
        private System.Windows.Forms.Button button1;
    }
}

