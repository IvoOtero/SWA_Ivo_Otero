using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientCommunication
    {
        //socket for communication
        Socket socket;
        byte[] buffer = new byte[512]; //buffer serves as storage location for data transmitted by TCP
        Action<string> MessageInformer; //informs the server about the text message
        Action AbortInformer;  //informs the server in case the instance needs to be aborted

        public ClientCommunication(string ip, int port, Action<string> messageInformer, Action abortInformer)
        {
            try
            {
                this.AbortInformer = abortInformer;
                this.MessageInformer = messageInformer;
                //new tcp client for the connection
                TcpClient client = new TcpClient();
                client.Connect(IPAddress.Parse(ip), port);
                //sets the client socket
                socket = client.Client;
                //starts recieving messages
                StartReceiving();
            }
            catch (Exception)
            {
                messageInformer("Server not ready");
                AbortInformer(); //reset Client Communication
            }

        }

        public void StartReceiving()
        {
            //create and start a task with the "Recieve" method
            Task.Factory.StartNew(Recieve);
        }

        public void Recieve()
        {
            string message = "";
            int length;
            while (!message.Contains("@quit"))
            {
                length = socket.Receive(buffer);
                message = Encoding.UTF8.GetString(buffer, 0, length);
                //inform the GUI and pass the message
                MessageInformer(message);
            }
            Close(); //if "@quit" was sent, then the socket will be closed
        }

        //method to send specific data to the connected socket on the server-side
        public void Send(string message)
        {
            if (socket != null) //check if clientsocket connected
            {
                socket.Send(Encoding.UTF8.GetBytes(message));
            }
        }

        

        public void Close()
        {
            socket.Close();
            AbortInformer();
        }
    }
}
