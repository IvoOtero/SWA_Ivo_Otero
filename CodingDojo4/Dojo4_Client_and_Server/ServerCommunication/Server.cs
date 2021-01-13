using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.ServerCommunication
{
    public class Server
    {

        Socket socket;
        List<ClientHandler> users = new List<ClientHandler>();
        Action<string> GUIAction;
        Task acceptingThread; //Task to accept new clients into the server 

        public Server(string ip, int portNr, Action<string> action)
        {
            GUIAction = action;

            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse(ip), portNr));  //binding/associating the socket to a local endpoint
            socket.Listen(5);
        }

        public void AcceptingThread()
        { 
            acceptingThread = Task.Run(
                () => Accept());


            //acceptingThread.Start(); //starts new thread
        }

        public void Accept()
        {
            while (acceptingThread.IsCompleted.Equals(false))
            {
                try
                {
                    //
                    //if the accepting Thread is not completed, then the program
                    //adds a new ClientHandler to the users list with a new Socket and an Action referencing the method "newMessage"
                    //
                    users.Add(new ClientHandler(socket.Accept(), new Action<string, Socket>(newMessage)));
                }
                catch (Exception e)
                {

                    Console.WriteLine($"Server was succesfully closed (it finally works)!! {Environment.NewLine} " + $"Exception thrown: {e.Message}");
                }
            }
        }

        public void Stop()
        {
            socket.Close();
            var source = new CancellationTokenSource();
            CancellationToken cancellationToken = source.Token;
            cancellationToken.ThrowIfCancellationRequested();
            source.Cancel();
            foreach (var user in users)
            {
                user.Close();
            }
            //removes all clients from the users list
            users.Clear();
        }

        public void DropUser(string username)
        {
            foreach (var user in users)
            {
                if (user.Name.Contains(username))
                {
                    user.Close();
                    users.Remove(user); //removes the user from the connected users list 

                    break;
                }
            }
        }

        public void newMessage(string message, Socket senderSocket)
        {
            //sends message to be display in the GUI
            GUIAction(message);
            //write message to all clients
            foreach (var user in users)
            {
                if (user.socket != senderSocket)
                {
                    user.Send(message);
                }
            }
        }
    }
}
