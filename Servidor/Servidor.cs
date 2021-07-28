using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Servidor
{

    class Servidor
    {
        IPHostEntry host;
        IPAddress ipAddr;
        IPEndPoint endPoint;

        Socket s_Server;
        Socket s_Client;

        static List<Socket> lista = new List<Socket>();

        public Servidor(string ip, int port)    //constructor
        {
            host = Dns.GetHostByName(ip);
            ipAddr = host.AddressList[0];
            endPoint = new IPEndPoint(ipAddr, port);

            s_Server = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);   //instancia del socket
            s_Server.Bind(endPoint);
            s_Server.Listen(100);        //# maximo de conexiones pendientes

        }

        public void Start()
        {
            Thread t;
            while (true)
            {
                Console.Write("Esperando conexion...\n");
                s_Client = s_Server.Accept();   //aceptacion de conexion 
                lista.Add(s_Client);
                t = new Thread(clienConnection);
                t.Start(s_Client);
                Console.WriteLine("Se ha conectado un cliente \n");
            }
        }

        public void clienConnection(object s)
        {
            Socket socketaux = (Socket)s;    //conversion de objeto a socket

            while (true)
            {
                EnvioGlobal(socketaux);
            }
        }

        public void EnvioGlobal(Socket saux)
        {
            byte[] benvio;

            DateTime fecha = DateTime.Now;
            string fechaactual = fecha.ToString();

            Console.WriteLine("Mensaje: \n");
            string mensaje = Console.ReadLine();
            string enviar = (mensaje + "   " + fechaactual);
            benvio = Encoding.ASCII.GetBytes(enviar);

            foreach (Socket soc in lista)
            {
                soc.Send(benvio);
                Console.Out.Flush();
            }

            byte[] buffer;
            string mensajellegada;
            int endIndex;

            buffer = new byte[1024];

            foreach (Socket soclista in lista)
            {
                soclista.Receive(buffer);
                mensajellegada = Encoding.ASCII.GetString(buffer);
                endIndex = mensajellegada.IndexOf('\0');
                if (endIndex > 0)
                {
                    mensajellegada = mensajellegada.Substring(0, endIndex);
                }

                Console.WriteLine("\n Recibido: " + mensajellegada);
                Console.Out.Flush();
            }
        }

    }
}
