using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO.Ports; 	// necessário para ter acesso as portas

namespace ControleRemotoIR
{
    public partial class Form1 : Form
    {
        //*Importando DLL para clicar com o Mouse* https://social.msdn.microsoft.com/Forums/vstudio/pt-BR/241c6422-b631-41d3-b77c-12f2dda63bd3/mouse-mover-e-clicar?forum=vscsharppt
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        public const int MOUSEEVENTF_LEFTDOWN = 0x02;
        public const int MOUSEEVENTF_LEFTUP = 0x04;
        public const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        public const int MOUSEEVENTF_RIGHTUP = 0x10;
        //****************************************

        string RxString;
        string comandoAntigo = "";
        bool testaHW = false;
        int mouseX = 0, mouseY = 0, aumento = 1; //variaveis para controlar velocidade do mouse
        byte tentativasConexao = 3;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            atualizaListaCOMs();
            autoConecta();
        }

        private void autoConecta()
        {
            textBoxReceber.AppendText(" serialPort1.IsOpen = " + serialPort1.IsOpen.ToString() + "\n");
            if (serialPort1.IsOpen == false)
            {
                try
                {
                    textBoxReceber.AppendText("Conectando...\n");
                    //textBoxReceber.AppendText("\n");
                    textBoxReceber.AppendText("Recebendo porta COM...\n");
                    serialPort1.PortName = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                    textBoxReceber.AppendText(" Porta COM recebida: " + serialPort1.PortName.ToString() + "\n");
                    textBoxReceber.AppendText("Abrindo Porta...\n");
                    serialPort1.Open();
                    textBoxReceber.AppendText(" serialPort1.IsOpen = " + serialPort1.IsOpen.ToString() + "\n");
                    textBoxReceber.AppendText("Auto recetar CONTROLE = " + cbResetStart.Checked.ToString() + "\n");
                    if (cbResetStart.Checked)
                    {
                        textBoxReceber.AppendText(" Atribuindo DtrEnable = true\n");
                        serialPort1.DtrEnable = true;
                        textBoxReceber.AppendText(" Atribuindo DtrEnable = false\n");
                        textBoxReceber.AppendText("Aguardando Reset = ");
                        serialPort1.DtrEnable = false;
                    }
                    else
                    {//se nao tiver o auto reset a placa não envia CONTROLE quando recebe conexão. Por isso criou-se esse else Uma maneira de verificar
                        //Se conectou certinho
                        textBoxReceber.AppendText("QMEH?\n");
                        textBoxReceber.AppendText("Aguardando resposta = ");
                        if (serialPort1.IsOpen == true)          //porta está aberta
                            serialPort1.Write("QMEH");
                    }

                }
                catch
                {
                    return;
                }
                if (serialPort1.IsOpen)
                {
                    btConectar.Text = "Desconectar";
                    comboBox1.Enabled = false;
                }
            }
            else
            {
                try
                {
                    textBoxReceber.AppendText("Desconectando...\n");
                    textBoxReceber.AppendText("Fechando comunicação com porta serial\n");
                    serialPort1.Close();
                    comboBox1.Enabled = true;
                    btConectar.Text = "Conectar";
                }
                catch
                {
                    return;
                }
            }
        }

        private void btConectar_Click(object sender, EventArgs e)
        {
            autoConecta();
        }

        void atualizaListaCOMs()
        {
            int i;
            bool quantDiferente;    //flag para sinalizar que a quantidade de portas mudou

            i = 0;
            quantDiferente = false;

            //se a quantidade de portas mudou
            if (comboBox1.Items.Count == SerialPort.GetPortNames().Length) //incluir using System.IO.Ports; para acesso ao SerialPort
            {
                foreach (string s in SerialPort.GetPortNames())
                {
                    if (comboBox1.Items[i++].Equals(s) == false)
                    {
                        quantDiferente = true;
                    }
                }
            }
            else
            {
                quantDiferente = true;
            }

            //Se não foi detectado diferença
            if (quantDiferente == false)
            {
                return;                     //retorna
            }

            //limpa comboBox
            comboBox1.Items.Clear();

            //adiciona todas as COM diponíveis na lista
            foreach (string s in SerialPort.GetPortNames())
            {
                comboBox1.Items.Add(s);
            }
            //seleciona a primeira posição da lista
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_Enter(object sender, EventArgs e)
        {
            atualizaListaCOMs();
        }

        private void btConectar_Enter(object sender, EventArgs e)
        {
            atualizaListaCOMs();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            RxString = serialPort1.ReadLine();  //Le dados na serial até "/n"
            this.Invoke(new EventHandler(trataDadoRecebido));   //chama outra thread para escrever o dado no text box
        }

        private void trataDadoRecebido(object sender, EventArgs e)
        {
            if (RxString == "CONTROLE\r")
            {
                lbDadosRecebidos.Text = "OK";
                textBoxReceber.AppendText("OK\n");
                testaHW = true;
                return;
            }
            lbDadosRecebidos.Text = RxString;
            string valordoLabel = lbDadosRecebidos.Text;   //variavel para acrescentar \n new line
            valordoLabel += "\n";
            textBoxReceber.AppendText(valordoLabel);
            if (testaHW)  // Se testa recebe true quando recebe do arduino a informação "CONTROLE"
                comandoparateclado(RxString);
            else
            {
                if(tentativasConexao > 0)
                {
                    string msg = "Tentativas de comunicação: ";
                    textBoxReceber.AppendText(string.Concat(msg,tentativasConexao.ToString())+"\n");
                    textBoxReceber.AppendText("QMEH foi enviado pela serial.");
                    btQMEH.PerformClick();
                    tentativasConexao--;
                    return;
                }
                MessageBox.Show("Comandos recebidos pela serial não parecem ser do controle. \rEsperado: CONTROLE\rRecebido: " + RxString);
                
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen == true)  // se porta aberta
                serialPort1.Close();        	//fecha a porta
        }

        void comandoparateclado(string comando)
        {
            if (comando.TrimEnd() != "FFFFFFFF")
            {
                comandoAntigo = comando;
                lbComandoAntigo.Text = comando;
                mouseX = 0;
                mouseY = 0;
                aumento = 1;
            }
            else
            {
                comando = comandoAntigo;
                mouseX += aumento;
                mouseY += aumento;
                aumento++;
            }


            switch (comando.TrimEnd())
            {
                case "FFA25D": //CH-
                    SendKeys.Send("{ESC}");
                    break;
                case "FF629D": //CH
                    System.Diagnostics.Process.Start("chrome.exe", "https://www.netflix.com/");
                    SendKeys.Send("{F11}");
                    break;
                case "FFE21D": //CH+
                    
                    break;
//-------------------------------------------------------------
                case "FF22DD": // |<<
                    SendKeys.Send("+({LEFT})");
                    break;
                case "FF02FD": // >>|
                    SendKeys.Send("+({RIGHT})");
                    break;
                case "FFC23D": // >|| Play/Pause
                    SendKeys.Send("{ENTER}");
                    Thread.Sleep(200);
                    break;
//-------------------------------------------------------------
                case "FFE01F": // -
                    SendKeys.Send("{Down}");
                    break;
                case "FFA857": // +
                    SendKeys.Send("{UP}");
                    break;
                case "FF906F": // EQ
                    SendKeys.Send("f");
                    break;
//-------------------------------------------------------------
                case "FF6897": // 0
                    mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                    break;
                case "FF9867": // 100+
                    SendKeys.Send("");
                    break;
                case "FFB04F": // 200+
                    SendKeys.Send("f");
                    break;
//-------------------------------------------------------------
                case "FF30CF": // 1
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X - mouseX, Cursor.Position.Y - mouseY);
                    break;
                case "FF18E7": // 2                    
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X - 0, Cursor.Position.Y - mouseY);
                    break;
                case "FF7A85": // 3
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X + mouseX, Cursor.Position.Y - mouseY);
                    break;
//-------------------------------------------------------------
                case "FF10EF": // 4
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X - mouseX, Cursor.Position.Y - 0);
                    break;
                case "FF38C7": // 5                    
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    break;
                case "FF5AA5": // 6
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X + mouseX, Cursor.Position.Y - 0);
                    break;
                //-------------------------------------------------------------
                case "FF42BD": // 7
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X - mouseX, Cursor.Position.Y + mouseY);
                    break;
                case "FF4AB5": // 8                    
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X - 0, Cursor.Position.Y + mouseY);
                    break;
                case "FF52AD": // 9
                    this.Cursor = new Cursor(Cursor.Current.Handle);
                    Cursor.Position = new Point(Cursor.Position.X + mouseX, Cursor.Position.Y + mouseY);
                    break;
                    //-------------------------------------------------------------
            }
        }

        private void btQMEH_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)          //porta está aberta
                serialPort1.Write("QMEH");  //envia o texto 
            else
                textBoxReceber.AppendText("Porta de COM não esta aberta.\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Set the Current cursor, move the cursor's Position,
            // and set its clipping rectangle to the form. 

            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(Cursor.Position.X - 0, Cursor.Position.Y - 2);
            //Cursor.Clip = new Rectangle(this.Location, this.Size);

        }
    }
}


/* Mapa do controle
FFA25D	FF629D	FFE21D		CH-	    CH	    CH+
FF22DD	FF02FD	FFC23D		|<<	    >>|	    >||
FFE01F	FFA857	FF906F		-	     +   	 EQ
FF6897	FF9867	FFB04F		0	    100+	200+
FF30CF	FF18E7	FF7A85		1	     2	     3
FF10EF	FF38C7	FF5AA5		4	     5	     6
FF42BD	FF4AB5	FF52AD		7	     8	     9

    https://msdn.microsoft.com/pt-br/library/system.windows.forms.sendkeys.send(v=vs.110).aspx
    https://pt.wikipedia.org/wiki/ASCII
    https://support.microsoft.com/pt-br/help/12445/windows-keyboard-shortcuts
    http://www.techtudo.com.br/noticias/noticia/2014/08/lista-tem-16-atalhos-e-comandos-especiais-para-netflix-no-pc.html

*/
