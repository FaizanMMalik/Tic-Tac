using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
namespace tactucserver
{
    class user
    {
        public TcpClient client { get; set; }
        public int player { get; set; }
    }
    class Program
    {
        static List<user> lclient = new List<user>();

        static void Main(string[] args)
        {
            TcpListener server = new TcpListener(9267);
            server.Start();
            int user = 0;
            while (true)
            {
                if (lclient.Count < 2)
                {
                    TcpClient client = server.AcceptTcpClient();
                    user++;
                    lock (lclient)
                    {
                        user u = new user();
                        u.player = user;
                        u.client = client;
                        lclient.Add(u);
                    }
                    BinaryFormatter bf = new BinaryFormatter();
                    if (lclient.Count == 2)
                    {
                        foreach (user us in lclient)
                        {
                            Console.WriteLine(us.player);
                            byte[] datafromserver = new byte[10];
                            datafromserver = Encoding.ASCII.GetBytes(us.player.ToString());
                            us.client.GetStream().Write(datafromserver, 0, datafromserver.Length);
                         
                        }
                    }
                    new Thread(() => readsend(client)).Start();

                }
            }
        }
        public static void readsend(TcpClient client)
        {
            BinaryFormatter bf = new BinaryFormatter();
            string msg = "";
            while (true)
            {
                msg = (string)bf.Deserialize(client.GetStream());
                foreach (user u in lclient)
                {
                    if (!u.client.Equals(client))
                    {
                        Console.WriteLine(msg);
                        byte[] datafromserver = new byte[10];
                        datafromserver = Encoding.ASCII.GetBytes(msg);
                        u.client.GetStream().Write(datafromserver, 0, datafromserver.Length);
                       
                    }
                }

            }
        }
    }
}
