using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Server.ServerCommunication
{
    public class Server
    {

        Socket socket;
        List<ClientHandler> users = new List<ClientHandler>();
        Action<string> GUIAction;
        Thread acceptingThread; //accepts new clients into the server 

        public Server(string ip, int portNr, Action<string> action)
        {
            GUIAction = action;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(new IPEndPoint(IPAddress.Parse(ip), portNr));
            socket.Listen(5); 
        }

        public void AcceptingThread()
        {
            acceptingThread = new Thread(new ThreadStart(Accept));
            acceptingThread.IsBackground = true;
            acceptingThread.Start(); //starts new thread
        }

        public void Accept()
        {
            while (acceptingThread.IsAlive)
            {
                try
                {
                    users.Add(new ClientHandler(socket.Accept(), new Action<string, Socket>(newMessage)));
                }
                catch (Exception e)
                {

                    Console.WriteLine("Server was succesfully closed (it finally works)!! ");
                }
            }
        }

        public void Stop()
        {
            socket.Close();
            acceptingThread.Abort();
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
