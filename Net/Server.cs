using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient.Net
{
    internal class Server
    {
        TcpClient client;

        public Server()
        {
            client = new TcpClient();
        }

        public void ConnectToServer()
        {
            if(!client.Connected) {
                client.Connect("127.0.0.1", 7891);            
            }
        }
    }
}
