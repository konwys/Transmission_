using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;


namespace odbiornik
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();



        }
        //Deklaracje
        string konwertowana = string.Empty;
        bool b_ascii = false;
        int dek;

        //Funkcje
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
            TcpListener serwer = new TcpListener(ip, 8080);
            TcpListener serwer1 = new TcpListener(ip, 808);
            TcpClient client = default(TcpClient);
            TcpClient client1 = default(TcpClient);
            
            try
            {

                serwer.Start();
                serwer1.Start();
                textBox1.Invoke(new Action(delegate ()
                {
                    textBox1.AppendText("Serwer aktywny...");

                }));
             
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());

            }

            while (true)

            {
                client = serwer.AcceptTcpClient();

                client1 = serwer1.AcceptTcpClient();//

                byte[] otrzymane = new byte[100];
                byte[] dekompozycja = new byte[100];

                NetworkStream stream = client.GetStream();
                NetworkStream stream1 = client1.GetStream();
                stream.Read(otrzymane, 0, otrzymane.Length);
                stream1.Read(dekompozycja, 0, dekompozycja.Length);
                    
                string wiadomosc = Encoding.ASCII.GetString(otrzymane, 0, otrzymane.Length);
                string wiadomosc2 = Encoding.ASCII.GetString(dekompozycja, 0, dekompozycja.Length);

                richTextBox1.Invoke(new Action(delegate ()
                {
                    richTextBox1.AppendText(wiadomosc2);
                }));


                richTextBox2.Invoke(new Action(delegate ()
                {
                    richTextBox2.AppendText(wiadomosc);
                }));


            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();

            textBox1.Invoke(new Action(delegate ()
            {
                textBox1.AppendText("Serwer nieaktywny...");

            }));
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            b_ascii = true;
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
