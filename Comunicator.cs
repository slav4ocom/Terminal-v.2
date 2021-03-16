using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;

namespace Terminal
{
    public static class Comunicator
    {
        private static string _comName = "COM5";
        private static int _baudRate = 115200;
        private static readonly Parity _parity = Parity.None;
        private static readonly int _dataBits = 8;
        private static readonly StopBits _stopBits = StopBits.One;


        static SerialPort myPort;


        public static void ConfigurePort()
        {
            Console.WriteLine("Enter COM number: ");
            _comName = $"COM{int.Parse(Console.ReadLine())}";

            Console.WriteLine("Enter baud rate: ");
            _baudRate = int.Parse(Console.ReadLine());

        }
        public static bool InitPort()
        {
            myPort = new SerialPort(_comName, _baudRate, _parity, _dataBits, _stopBits);
            try
            {
                myPort.Open();
                return true;
            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.WriteLine($"{ex.FileName} not found");
                return false;
            }
        }

        public static void Close()
        {

            myPort.Close();
            myPort.Dispose();
        }

        public static void ShowPortInfo()
        {
            Console.WriteLine(myPort.PortName + "\r\n"
                + myPort.BaudRate + "\r\n"
                + myPort.Parity + "\r\n"
                + myPort.DataBits + "\r\n"
                + myPort.StopBits + "\r\n"
                );
        }

        public static async Task Receive()
        {
            byte[] receiveBuffer = new byte[1024];
            var numBytesReaden = await myPort.BaseStream.ReadAsync(receiveBuffer);
            Console.Write(Encoding.UTF8.GetString(receiveBuffer, 0, numBytesReaden));
        }



        public static async Task SendString(string inputString)
        {
            if (inputString == "+++" || inputString == "\\0")
            {
                byte[] sendBuffer = Encoding.UTF8.GetBytes(inputString);
                await myPort.BaseStream.WriteAsync(sendBuffer);
            }
            else
            {
                byte[] sendBuffer = Encoding.UTF8.GetBytes(inputString + "\r\n");
                await myPort.BaseStream.WriteAsync(sendBuffer);

            }
        }



    }
}
