using System;
using System.Threading.Tasks;
using Terminal;

namespace Terminal_v._2
{
    class Program
    {
        static async void ReceiveCycle()
        {
            while (true)
            {
                await Comunicator.Receive();
            }
        }


        static async Task TransmitCycle()
        {
            while (true)
            {
                var atCommand = Console.ReadLine();

                if (atCommand.ToLower() == "exit")
                {
                    break;
                }
                else if (atCommand.ToLower() == "cls".ToLower())
                {
                    Console.Clear();
                }
                else
                {
                    await Comunicator.SendString(atCommand);

                }

            }
        }


        static async Task Main(string[] args)
        {

            Comunicator.ListPorts();

            while (true)
            {
                Comunicator.ConfigurePort();

                if (Comunicator.InitPort() == true)
                {
                    Comunicator.ShowPortInfo();
                    ReceiveCycle();
                    await TransmitCycle();
                    Comunicator.Close();
                    break;
                }
                else
                {
                    Console.WriteLine("Could not connect.");
                }

            }


            Console.WriteLine("bye . . .");
        }
    }
}
