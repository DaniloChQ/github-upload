using System;
using System.Net;
using System.Net.Sockets;
using System.Text;



namespace Cliente
{
    class Cliente
    {
        IPHostEntry host;
        IPAddress ipAddr;
        IPEndPoint endPoint;
        int contaux = 0;
        static Socket s_Client;

        public Cliente(string ip, int port)    //constructor
        {
            host = Dns.GetHostByName(ip);
            ipAddr = host.AddressList[0];
            endPoint = new IPEndPoint(ipAddr, port);

            s_Client = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);   //instancia del socket

        }

        public void Start()
        {
            s_Client.Connect(endPoint);
        }

        public void Send(int contador)
        {

            string puerto = ((IPEndPoint)s_Client.LocalEndPoint).Port.ToString();
            string direccion = ((IPEndPoint)s_Client.LocalEndPoint).Address.ToString();
            byte[] byteMsg = Encoding.ASCII.GetBytes("IP: " + direccion + "   Puerto: " + puerto + " Contador: " + contador);
            s_Client.Send(byteMsg);
            Console.WriteLine("Mensaje Enviado \n");

        }

        public void mostrar()
        {
            string mensajerecibido;
            int endIndex;
            byte[] buffRx = new byte[1024];


            s_Client.Receive(buffRx);
            contaux++;

            int aux2 = contaux;
            mensajerecibido = Encoding.ASCII.GetString(buffRx);
            endIndex = mensajerecibido.IndexOf('\0');
            if (endIndex > 0)
            {
                mensajerecibido = mensajerecibido.Substring(0, endIndex);
            }

            Console.WriteLine("Mensaje recibido: " + mensajerecibido);
            Console.Beep();
            Console.Out.Flush();

            Send(aux2);

        }

    }
}
















