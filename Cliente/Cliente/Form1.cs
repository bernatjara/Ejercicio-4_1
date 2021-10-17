using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        Socket server;
        public Form1()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //IPEndPoint con el ip y el puerto del servidor al que queremos conectarnos
            IPAddress direc = IPAddress.Parse("192.168.56.102");
            IPEndPoint ipep = new IPEndPoint(direc, 9010);

            //Creamos el socket 
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

             try
            {
                //Intentamos conectar el socket
                server.Connect(ipep);
                this.BackColor = Color.Green;
                MessageBox.Show("Conectado");
            }

             catch (SocketException)
             {
                 //Si hay excepcion imprimimos error y salimos del programa con return 
                 MessageBox.Show("No he podido conectar con el servidor");
                 return;
             } 
        }

        private void button1_Click(object sender, EventArgs e)
        {

                if (Longitud.Checked)
                {
                    if (string.IsNullOrEmpty(nombre.Text))
                    {
                        MessageBox.Show("Introduzca su nombre porfavor.");
                    }
                    else
                    {
                        // Quiere la longitud del nombre
                        string mensaje = "1/" + nombre.Text;
                        // Enviamos al servidor el nombre
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);

                        //Recibimos la respuesta del servidor
                        byte[] msg2 = new byte[80];
                        server.Receive(msg2);
                        mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                        MessageBox.Show("La longitud de tu nombre es: " + mensaje);
                    }
                }
                else if (Bonito.Checked)
                {
                    if (string.IsNullOrEmpty(nombre.Text))
                    {
                        MessageBox.Show("Introduzca su nombre porfavor.");
                    }
                    else
                    {
                        // Quiere saber si el nombre es bonito o no
                        string mensaje = "2/" + nombre.Text;
                        // Enviamos al servidor el nombre
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);

                        //Recibimos la respuesta del servidor
                        byte[] msg2 = new byte[80];
                        server.Receive(msg2);
                        mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];


                        if (mensaje == "SI")
                            MessageBox.Show("Tu nombre ES bonito.");
                        else
                            MessageBox.Show("Tu nombre NO es bonito. Lo siento.");
                    }


                }
                else if (Altura.Checked)
                {
                    if (string.IsNullOrEmpty(alturaBox.Text))
                    {
                        MessageBox.Show("Introduzca su altura porfavor.");
                    }
                    else
                    {
                        //Enviamos el nombre y la altura
                        string mensaje = "3/" + nombre.Text + "/" + alturaBox.Text;

                        //Enviamos al servidor el nombre y la altura
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);

                        //Recibimos la respuesta del servidor
                        byte[] msg2 = new byte[80];
                        server.Receive(msg2);
                        mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                        MessageBox.Show(mensaje);
                    }
                }
                else if (Palindromo.Checked)
                {
                    if (string.IsNullOrEmpty(nombre.Text))
                    {
                        MessageBox.Show("Introduzca su nombre porfavor.");
                    }
                    else
                    {
                        // Quiere saber si el nombre es bonito o no
                        string mensaje = "4/" + nombre.Text;
                        // Enviamos al servidor el nombre
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);

                        //Recibimos la respuesta del servidor
                        byte[] msg2 = new byte[80];
                        server.Receive(msg2);
                        mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];


                        if (mensaje == "SI")
                            MessageBox.Show("Tu nombre ES palíndromo.");
                        else
                            MessageBox.Show("Tu nombre NO es palindromo.");
                    }
                }
                else if (Mayuscula.Checked)
                {
                    if (string.IsNullOrEmpty(nombre.Text))
                    {
                        MessageBox.Show("Introduzca su nombre porfavor.");
                    }
                    else
                    {
                        // Quiere el nombre en mayúsculas
                        string mensaje = "5/" + nombre.Text;
                        // Enviamos al servidor el nombre
                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                        server.Send(msg);

                        //Recibimos la respuesta del servidor
                        byte[] msg2 = new byte[80];
                        server.Receive(msg2);
                        mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                        MessageBox.Show(mensaje);
                    }
                }

                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Enviamos mensajje de desconexión
            string mensaje = "0/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            // Nos desconectamos
            this.BackColor = Color.Gray;
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

       
    }
}
