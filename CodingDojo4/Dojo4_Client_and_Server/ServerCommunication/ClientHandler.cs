using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server.ServerCommunication
{
    //The class ClientHandler handles the communication between the server and one of the client instances (can be more than one client at the same time)
    public class ClientHandler
    {
        public Socket socket { get; private set; }
        public string Name { get; private set; }
        private byte[] buffer = new byte[512];
        Action<string, Socket> GUIAction;
        string endMessage = "@quit";
        Thread recieveThread;

        public ClientHandler(Socket socket, Action<string, Socket> action)
        {
            this.socket = socket;
            this.GUIAction = action;
            //reciving client thread 
            this.recieveThread = new Thread(Recieve);
            //start the current thread
            recieveThread.Start();
        }

        public void Recieve()
        {
            string message = "";
            int length;
            while (!message.Contains(endMessage))
            {
                length = socket.Receive(buffer);
                message = Encoding.UTF8.GetString(this.buffer, 0, length);
                //set name of the messager
                if (this.Name == null && message.Contains(":"))
                {
                    this.Name = message.Split(':')[0];
                }

                GUIAction(message, socket);

            }
            Close();
        }

        public void Send(string message)
        {
            socket.Send(Encoding.UTF8.GetBytes(message));
        }

        public  void Close()
        {
            Send(endMessage); //sends endmessage ("@quit") to client 
            this.recieveThread.Abort(); //abort client thread
            socket.Close(1); //disconnect
        }

        
    }
}
