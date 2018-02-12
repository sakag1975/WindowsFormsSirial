using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace WindowsFormsSirial
{
    public partial class Form1 : Form
    {
        SerialPortProcessor myPort = null;
        public static TextBox textBox = new TextBox();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (myPort != null) {
                return;
            }
            myPort = new SerialPortProcessor(this);
            myPort.PortName = "COM4";
            myPort.BoudRate = 4800;
            myPort.DataBits = 8;
            myPort.Parity = Parity.None;
            myPort.StopBits = StopBits.One;
            myPort.DataReceived += DataReceivedHandler;
            myPort.Start();
        }

        
        private void DataReceivedHandler(SerialPortEventArgs e)
        {
            string text = System.Text.Encoding.ASCII.GetString(e.data);
            textBoxReceiveData.AppendText(text);
        }



        private void button2_Click(object sender, EventArgs e)
        {
            string sendText = textBoxSendData.Text;
            byte[] buffer = System.Text.Encoding.ASCII.GetBytes(sendText);
            myPort.WriteData(buffer);
            textBoxSendData.Clear();

        }

    }
}
