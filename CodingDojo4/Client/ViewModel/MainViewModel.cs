using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Client.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private ClientCommunication clientCommunication;
        private bool isConnected = false; 

        public string ChatName { get; set; }
        public string Message { get; set; }
        public ObservableCollection<string> Recieved { get; set; } //list with all recieved messages 
        public RelayCommand ConnectButtonCommand { get; set; } //connect to serever button
        public RelayCommand SendButtonCommand { get; set; } //send message button on GUI 


        public MainViewModel()
        {
            Recieved = new ObservableCollection<string>();
            //initialize the Command for connection by clicking button "connect"
            ConnectButtonCommand = new RelayCommand(
                () =>
                {
                    this.isConnected = true;
                    this.clientCommunication = new ClientCommunication("127.0.0.1", 10100, new Action<string>(NewMessage), ClientDisconnected); 
                }, () =>
                {
                    return (isConnected==false); //returns boolean for RelayCommand´s "canExcecute" parameter
                });

            //initialize the Command for "send" button to send a message
            SendButtonCommand = new RelayCommand(
                () =>
                {
                    clientCommunication.Send(ChatName + ": " + Message); //format of message to send (chatname or username and the message written on GUI)
                    Recieved.Add("YOU: " + Message); //write the own message to be displayed as message sent by the user himself
                }, () =>
                {
                    return(isConnected && Message.Length > 0); //can only excecute if connection was created and a non-empty message was written
                });


        }

        private void NewMessage(string message)
        {
            //write new message in Collection to display in GUI
            //use the GUI thread to avoid threading problems 
            App.Current.Dispatcher.Invoke(
                () =>
                {
                    Recieved.Add(message);
                });
        }

        private void ClientDisconnected()
        {
            this.isConnected = false;
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
