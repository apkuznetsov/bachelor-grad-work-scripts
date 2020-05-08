using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpServer
{
    class Program
    {
        private static string _sendingIpAddress = "127.22.22.1";
        // private static string _sendingIpAddress = "127.0.0.1";
        private static int _sendingPort = 6969;

        static string TARGET_ADDRESS = "127.0.0.1"; // IP-address of the target listener
        static int TARGET_PORT = 8888; // Port of the target listener

        static void Main(string[] args)
        {
            // Console.Write("Enter ip address to send from: ");
            // _sendingIpAddress = int.Parse(Console.ReadLine());

            Console.Write("Enter port to send from: ");
            _sendingPort = int.Parse(Console.ReadLine());

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
            if (!IPAddress.TryParse(_sendingIpAddress, out IPAddress sendingIpAddress))
            {
                throw new FormatException("Invalid ip-adress");
            }

            IPEndPoint localIpEndPoint = new IPEndPoint(sendingIpAddress, _sendingPort);

            UdpClient sender = new UdpClient(localIpEndPoint);

            Console.WriteLine($"You\'re sending from: {localIpEndPoint.Address}:{localIpEndPoint.Port}");
            
            try
            {
                while (true)
                {
                    Console.Write("Type message to send: ");

                    string message = Console.ReadLine();

                    // byte[] encodedMessage = Encoding.Unicode.GetBytes(message);
                    byte[] encodedMessage = Encoding.ASCII.GetBytes(message);

                    sender.Send(encodedMessage, encodedMessage.Length, TARGET_ADDRESS, TARGET_PORT);
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