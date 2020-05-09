using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UdpClient
{
        public class Program
        {
            private static int LOCAL_PORT = 8888; // local port for listening incoming data

            static void Main(string[] args)
            {
                try
                {
                    // Console.Write("localPort:  "); // local port
                    // localPort = Int32.Parse(Console.ReadLine());

                    Console.WriteLine("Listening to port: {0}...", LOCAL_PORT); // local port

                    Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage));
                    receiveThread.Start();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            private static void ReceiveMessage()
            {
                System.Net.Sockets.UdpClient receiver = new System.Net.Sockets.UdpClient(LOCAL_PORT); // UdpClient for receiving incoming data

            // IPEndPoint remoteIp = new IPEndPoint(IPAddress.Parse("80.234.45.88"), 8888); // address of the sending server
            IPEndPoint remoteIp = null; // address of the sending server (NULL means Any)
                try
                {
                    while (true)
                    {
                        byte[] data = receiver.Receive(ref remoteIp); // receive data from the server

                        // string message = Encoding.Unicode.GetString(data);
                        //Console.WriteLine("server data: {0}", message);


                        Console.WriteLine($"Received broadcast from {remoteIp}");
                        Console.WriteLine($" {Encoding.ASCII.GetString(data, 0, data.Length)}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    receiver.Close();
                }
            }
    }
}
