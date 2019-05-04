using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace userver
{
    class Program
    {
        static void Main(string[] args)
                                         
        {

            if (args.Length > 1) throw new ArgumentException("Usage: userver [<PORT>]");

            int servPort = Int32.Parse(args[0]);

            UdpClient client = null;

            try
            {

                client = new UdpClient(servPort);


            }

            catch (SocketException e)
            {

                Console.WriteLine(e.Message);
                Environment.Exit(e.ErrorCode);

            }

            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, servPort);

            for (; ; ) {

                try
                {

                    byte[] buffer = client.Receive(ref endpoint);

                    Console.WriteLine("Handling at :" + endpoint.Address + " : " +Encoding.ASCII.GetString(buffer));

                    client.Send(buffer,buffer.Length, endpoint);

                    Console.WriteLine("Echoed {0} bytes", buffer.Length);

                }

                catch (SocketException e)
                {

                    Console.WriteLine(e.ErrorCode);

                }
            }         
        }
    }
}
