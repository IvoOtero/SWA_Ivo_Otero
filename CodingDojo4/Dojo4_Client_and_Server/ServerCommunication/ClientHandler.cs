using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.ServerCommunication
{
    //The class ClientHandler handles the communication between the server and one of the client instances (can be more than one client at the same time)
    public class ClientHandler
    {
        public Socket socket { get; private set; }
        public string Name { get; private set; }
        private byte[] buffer = new byte[512];        
        Action<string, Socket> GUIAction; //Action that encapsulates a method that takes String and Socket parameters and returns no value (void)
        string endMessage = "@quit";
        Task recieveThread;

        public ClientHandler(Socket socket, Action<string, Socket> action)
        {
            this.socket = socket;
            this.GUIAction = action;
            //start and recieve client thread 
            this.recieveThread = Task.Run(() => Recieve());
            //recieveThread.Start();
        }

        public void Recieve()
        {
            string message = "";
            int length;
            while (!message.Contains(endMessage) && socket.Connected)
            {
                length = socket.Receive(buffer);
                message = Encoding.UTF8.GetString(this.buffer, 0, length);
                //set name of the messager/user 
                if (this.Name == null && message.Contains(":"))
                {
                    this.Name = message.Split(':')[0];
                }

                GUIAction(message, socket);

            }
            Close();
        }

        public  void Close()
        {
            Send(endMessage + $"{Environment.NewLine}{Environment.NewLine}" + "SERVER: Server was manually closed, you cannot send any more messages"); //sends endmessage ("@quit") to the client/user  
            
            //abort client thread --> OUTDATED OPTION
            //recieveThread.Abort(); 

            //alternative with task cancelation instead of thread.abort (for NET.Core and NET 5) 
            var source = new CancellationTokenSource();
            CancellationToken cancellationToken = source.Token;
            cancellationToken.ThrowIfCancellationRequested();
            source.Cancel();
            
            //disconnect the socket connection. The bool "false" indicates that the socket instance cannot be reused 
            socket.Disconnect(false); 
            
        }

        public void Send(string message)
        {
            if (socket.Connected)
            {
                socket.Send(Encoding.UTF8.GetBytes(message));
            }
        }

        
    }
}
