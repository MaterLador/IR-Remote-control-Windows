namespace ControleRemotoIR
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btConectar = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.lbDadosRecebidos = new System.Windows.Forms.Label();
            this.textBoxReceber = new System.Windows.Forms.TextBox();
            this.lbComandoAntigo = new System.Windows.Forms.Label();
            this.cbResetStart = new System.Windows.Forms.CheckBox();
            this.btQMEH = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btConectar
            // 
            this.btConectar.Location = new System.Drawing.Point(197, 13);
            this.btConectar.Name = "btConectar";
            this.btConectar.Size = new System.Drawing.Size(75, 23);
            this.btConectar.TabIndex = 0;
            this.btConectar.Text = "Conectar";
            this.btConectar.UseVisualStyleBackColor = true;
            this.btConectar.Click += new System.EventHandler(this.btConectar_Click);
            this.btConectar.Enter += new System.EventHandler(this.btConectar_Enter);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(13, 13);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(117, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            this.comboBox1.Enter += new System.EventHandler(this.comboBox1_Enter);
            // 
            // lbDadosRecebidos
            // 
            this.lbDadosRecebidos.AutoSize = true;
            this.lbDadosRecebidos.Location = new System.Drawing.Point(12, 47);
            this.lbDadosRecebidos.Name = "lbDadosRecebidos";
            this.lbDadosRecebidos.Size = new System.Drawing.Size(35, 13);
            this.lbDadosRecebidos.TabIndex = 2;
            this.lbDadosRecebidos.Text = "label1";
            // 
            // textBoxReceber
            // 
            this.textBoxReceber.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxReceber.Location = new System.Drawing.Point(12, 73);
            this.textBoxReceber.Multiline = true;
            this.textBoxReceber.Name = "textBoxReceber";
            this.textBoxReceber.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxReceber.Size = new System.Drawing.Size(260, 176);
            this.textBoxReceber.TabIndex = 3;
            // 
            // lbComandoAntigo
            // 
            this.lbComandoAntigo.AutoSize = true;
            this.lbComandoAntigo.Location = new System.Drawing.Point(210, 47);
            this.lbComandoAntigo.Name = "lbComandoAntigo";
            this.lbComandoAntigo.Size = new System.Drawing.Size(35, 13);
            this.lbComandoAntigo.TabIndex = 4;
            this.lbComandoAntigo.Text = "label1";
            // 
            // cbResetStart
            // 
            this.cbResetStart.AutoSize = true;
            this.cbResetStart.Checked = true;
            this.cbResetStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbResetStart.Location = new System.Drawing.Point(77, 46);
            this.cbResetStart.Name = "cbResetStart";
            this.cbResetStart.Size = new System.Drawing.Size(79, 17);
            this.cbResetStart.TabIndex = 5;
            this.cbResetStart.Text = "Reset Start";
            this.cbResetStart.UseVisualStyleBackColor = true;
            // 
            // btQMEH
            // 
            this.btQMEH.Location = new System.Drawing.Point(136, 13);
            this.btQMEH.Name = "btQMEH";
            this.btQMEH.Size = new System.Drawing.Size(55, 23);
            this.btQMEH.TabIndex = 6;
            this.btQMEH.Text = "QMEH?";
            this.btQMEH.UseVisualStyleBackColor = true;
            this.btQMEH.Click += new System.EventHandler(this.btQMEH_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(162, 42);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 17);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btQMEH);
            this.Controls.Add(this.cbResetStart);
            this.Controls.Add(this.lbComandoAntigo);
            this.Controls.Add(this.textBoxReceber);
            this.Controls.Add(this.lbDadosRecebidos);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btConectar);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btConectar;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label lbDadosRecebidos;
        private System.Windows.Forms.TextBox textBoxReceber;
        private System.Windows.Forms.Label lbComandoAntigo;
        private System.Windows.Forms.CheckBox cbResetStart;
        private System.Windows.Forms.Button btQMEH;
        private System.Windows.Forms.Button button1;
    }
}

