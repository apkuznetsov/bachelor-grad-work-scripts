using System;
using System.Net.Sockets;
using System.Text;

namespace UdpServer
{
    class Program
    {
        private static int LOCAL_PORT = 8888;
        static string REMOTE_ADDRESS = "127.0.0.1"; // IP-address of the target listener
        static int REMOTE_PORT = 8888; // Port of the target listener

        static void Main(string[] args)
        {
            try
            {
                SendMessage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void SendMessage()
        {
            UdpClient sender = new UdpClient();

            try
            {
                while (true)
                {
                    string message = Console.ReadLine();

                    // byte[] encodedMessage = Encoding.Unicode.GetBytes(message);
                    byte[] encodedMessage = Encoding.ASCII.GetBytes(message);

                    sender.Send(encodedMessage, encodedMessage.Length, REMOTE_ADDRESS, REMOTE_PORT);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                sender.Close();
            }
        }
    }
}