using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsSirial
{
    class SerialPortProcessor
    {
        private SerialPort myPort = null;
        private Thread receiveThread = null;
        private Control mainThreadForm;

        public string PortName { get; set; }
        public int BoudRate { get; set; }
        public Parity Parity { get; set; }
        public int DataBits { get; set; }
        public StopBits StopBits { get; set; }

        public SerialPortProcessor(Control mainThreadForm) {
            this.mainThreadForm = mainThreadForm;
        }

        public void Start() {
            if (myPort != null)
            {
                return;
            }
            myPort = new SerialPort(PortName, BoudRate, Parity, DataBits, StopBits);
            myPort.Open();

            receiveThread = new Thread(SerialPortProcessor.ReceiveWork);
            receiveThread.Start(this);
        }

        public static void ReceiveWork(object target) {
            SerialPortProcessor my = target as SerialPortProcessor;
            my.ReciveData();
        }


        public void WriteData(byte[] buffer) {

            myPort.Write(buffer, 0, buffer.Length);
        }

        public delegate void DataReceivedHandler(SerialPortEventArgs e);
        public event DataReceivedHandler DataReceived;


        public void ReciveData() {
            if (myPort == null) {
                return;
            }
            do
            {
                try {
                    int rbyte = myPort.BytesToRead;
                    byte[] buffer = new byte[rbyte];
                    int read = 0;
                    while (read < rbyte)
                    {
                        int length = myPort.Read(buffer, read, rbyte - read);
                        read += length;
                    }
                    if (rbyte > 0)
                    {
                        mainThreadForm.Invoke(DataReceived, new SerialPortEventArgs(buffer));
                    }
                    
                }
                catch (InvalidOperationException ex)
                {

                }
            

            } while (myPort.IsOpen);
        }


        public void Close() {
            if (myPort != null && receiveThread != null) {
                myPort.Close();
                receiveThread.Join();
            }
        }

    }

    public class SerialPortEventArgs : EventArgs
    {
        public byte[] data;
        public SerialPortEventArgs (byte[] data)
        {
            this.data = data;
        }
    }
        
}
