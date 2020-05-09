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
using System.IO;
using System.Collections.ObjectModel;

namespace nadajnik
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        //Deklaracje
        string konwertowana = string.Empty;
        string serwerIP = "localhost";
        int port = 8080;
        int port1 = 808;
        string filepath = @"SLOWNIK.txt";
        string[] words;
        string odwrocona;
        string start = "1";
        bool clear = false;
        int k;
        string ostateczna;
        //Funckje    
        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        //Obiekty
        private void Form1_Load(object sender, EventArgs e)
        {

            List<string> lines = File.ReadAllLines(filepath).ToList();

            foreach (string line in lines)
            {
                words = line.Split(',');
                
            }
              
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ascii = richTextBox1.Text;

            for (int i = 0; i < words.Length; i++)
            {
                if (richTextBox1.Text == words[i])
                {
                    ascii = "******";

                }
           }
           
       
            
            byte[] bity = Encoding.ASCII.GetBytes(ascii);
           

            for (int i = 0; i < bity.Length; i++)
            {

               // konwertowana = "1";
           
               for (int o = 0; o < 8; o++)
                {
                    konwertowana += (bity[i] & 0x80) > 0 ? "1" : "0";
                    bity[i] <<= 1;
                }

                
                odwrocona = ReverseString(konwertowana);
                
                odwrocona += "00";

            }
          
            

            StringBuilder sb = new StringBuilder();
            for (int p = 0; p < odwrocona.Length; p++)
            {

                if (p % 11 == 0)
                { 
                    sb.Append("  ");
                }
                sb.Append(odwrocona[p]);

            }

            string formatted = sb.ToString();

            richTextBox2.Text = start+odwrocona;

        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {
           
        }
         
        private void button2_Click(object sender, EventArgs e)
        {
            TcpClient client = new TcpClient(serwerIP, port);
            string ascii = richTextBox1.Text;
            char[] znaki = ascii.ToCharArray();

            for (int i = 0; i < words.Length; i++)
            {
                if (richTextBox1.Text == words[i])
                {
                    for (int k = 0; k < znaki.Length; k++)
                    {
                        znaki[k] = '*';
                    }
                }
            }
            ascii = new string(znaki);
            byte[] bity = Encoding.ASCII.GetBytes(ascii);
            NetworkStream stream = client.GetStream();
            stream.Write(bity, 0, bity.Length);
            stream.Close();
            client.Close();




            TcpClient client1 = new TcpClient(serwerIP, port1);
            for (int i = 0; i < bity.Length; i++)
            {

                // konwertowana = "1";

                for (int o = 0; o < 8; o++)
                {
                    konwertowana += (bity[i] & 0x80) > 0 ? "1" : "0";
                    bity[i] <<= 1;
                }

                odwrocona = ReverseString(konwertowana);

                odwrocona += "00";

            }

            string koniec = start + odwrocona;
            byte[] bity_os = Encoding.ASCII.GetBytes(koniec);

            NetworkStream stream1 = client1.GetStream();
            stream1.Write(bity_os, 0, bity_os.Length);
            stream1.Close();
            client1.Close();
            

            

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            clear = true;
            richTextBox1.Text = " ";
            richTextBox2.Text = " ";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
