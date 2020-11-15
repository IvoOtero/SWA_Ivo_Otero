using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;


namespace Server.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private ServerCommunication.Server server; //server instance, issue with project name and server class name
        private const int portNr = 10100; //always use the same port for sockets
        private const string ip = "127.0.0.1";
        private bool isConnected = false;
        
        public RelayCommand StartButtonCommand { get; set; }
        public RelayCommand StopButtonCommand { get; set; }
        public RelayCommand DropButtonCommand { get; set; }
        public RelayCommand SaveButtonCommand { get; set; }
        public ObservableCollection<string> Users { get; set; } //list of users to be displayed on left side of GUI
        public ObservableCollection<string> Messages { get; set; } //list of all messages, written by any user on the application
        public String selectedUser { get; set; } //selected user from the user list on GUI, useful for drop connection 
        public int messageCount { get { return Messages.Count; } }

        

        public MainViewModel()
        {         
            Messages = new ObservableCollection<string>();
            Users = new ObservableCollection<string>();

            //command for start button on the server side of application
            StartButtonCommand = new RelayCommand(
                () =>
                {
                    server = new ServerCommunication.Server(ip, portNr, UpdateMessageList);
                    server.AcceptingThread();
                    isConnected = true;
                }, () =>
                {
                    return !isConnected;
                });

            StopButtonCommand = new RelayCommand(
                () =>
                {
                    server.Stop(); //stop all connections and threads
                    isConnected = false;

                }, () =>
                {
                    return isConnected; 
                });

            DropButtonCommand = new RelayCommand(
                () =>
                {
                    server.DropUser(selectedUser); //stopp a specific user connection to the server
                    Users.Remove(selectedUser); //remove User from the GUI´s list of connected users 
                }, () =>
                {
                    return (selectedUser != null); //returns true if the selected user to drop is not null
                });


        }

        private void UpdateMessageList(string message)
        {
            //switch thread to GUI thread to write directly into GUI
            App.Current.Dispatcher.Invoke(
                () =>
                {
                    string username = message.Split(":")[0]; //string before ":" on recieved message states username/chatname 
                    if (!Users.Contains(username))
                    {
                        //if user is not on the list => added on the list
                        Users.Add(username);
                    }
                    if (message.Contains("@quit")) 
                    {
                        //if message contains the quit message, the client is disconnected 
                        server.DropUser(username);
                    }

                    //write the recieved message onto the message list on the GUI
                    Messages.Add(message);
                    //update no. of messages with every new message recieved
                    RaisePropertyChanged("messageCount");


                });
        }
    }
}
