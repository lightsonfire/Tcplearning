using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using Server.Headers;
using Server.Handlers;

namespace Server
{
    class Server
    {
        private bool running = false;
        public int port = 8362;
        public static Dictionary<short, Handler> handlers = new Dictionary<short, Handler>();
        private TcpListener listener;

        public Server(int port)
        {
            this.port = port;
        }

        #pragma warning disable
        public bool init()
        {
            listener = new TcpListener(port);
            registerHandlers();
            new Thread(new ThreadStart(delegate()
            {
                listener.Start();
                running = true;
                while (running)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ClientHandler ch = new ClientHandler(client);
                    Thread t = new Thread(new ThreadStart(ch.run));

                    ch.MyThread = t;
                    t.Start();
                }
            })).Start();
            return false;
        }

        private void registerHandlers()
        {
            registerHandler((short)InHeaders.LOGIN, new LoginHandler());
        }
        private void registerHandler(short header, Handler handler)
        {
            if (!handlers.ContainsValue(handler))
            {
                handlers.Add(header, handler);
            }
        }
        public void stopServer()
        {
            running = false;
        }
    }
}
