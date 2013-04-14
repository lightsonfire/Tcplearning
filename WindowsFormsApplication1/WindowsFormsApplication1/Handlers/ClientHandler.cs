using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.IO;

namespace Server.Handlers
{
    class ClientHandler
    {
        private BinaryReader br;
        private BinaryWriter bw;
        private Thread myThread;

        public Thread MyThread
        {
            get { return myThread; }
            set { myThread = value; }
        }
        private TcpClient myClient;

        public ClientHandler(TcpClient client)
        {
            myClient = client;
            br = new BinaryReader(client.GetStream());
            bw = new BinaryWriter(client.GetStream());
        }

        public void run()
        {
            while (true)
            {
                try
                {
                    short header = br.ReadInt16();
                    if (Server.handlers.ContainsKey(header))
                    {

                    }
                }
                catch
                {
                }
            }
        }
    }
}
