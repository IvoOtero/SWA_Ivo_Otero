using DataHandling_and_Logging;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Server.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        private ServerCommunication.Server server; //server instance (issue with project name and server class name)
        private const int portNr = 10100; //the port number is always set to 10100, as it guarantees communication with TCP 
        private const string ip = "127.0.0.1"; //local ip adress, as the server runs in the local computer
        private bool isConnected = false;
        public DataHandler dataHandler; //self made data handler from project "DataHandling_and_Logging"
        public int messageCount { get { return Messages.Count; } }


        #region Relay Commands for GUI
        public RelayCommand StartButtonCommand { get; set; }
        public RelayCommand StopButtonCommand { get; set; }
        public RelayCommand DropUserButtonCommand { get; set; }
        public RelayCommand SaveToLogBtnClickCommand { get; set; }
        public RelayCommand OpenFileButtonCommand { get; set; }
        public RelayCommand DropFileButtonCommand { get; set; }
        #endregion


        public ObservableCollection<string> Users { get; set; } //list of users to be displayed on left side of GUI
        public ObservableCollection<string> Messages { get; set; } //list of all messages, written by any user on the application
        public ObservableCollection<string> LogMessages { get; set; }
        public ObservableCollection<string> Files //list of all saved log-files 
        {
            get => new ObservableCollection<string>(dataHandler.ListFilesFromFolder());
        }
        

        private string _selectedUser;
        public String SelectedUser {
            get
            {
                return _selectedUser;
            }
            set 
            {
                _selectedUser = value;
                RaisePropertyChanged("SelectedUser");

                //update the status of the user (if it was indeed selected in the GUI), which affects if the DropUserButtonCommand can be excecuted 
                DropUserButtonCommand.RaiseCanExecuteChanged(); 
            }
        } 


        private string _selectedFile;
        public String SelectedFile
        {
            get
            {
                return _selectedFile;
            }
            set
            {
                _selectedFile = value;
                RaisePropertyChanged("SelectedFile");

                //update the status of the file (if it was indeed selected in the GUI), which affects if the following commands can be excecuted 
                OpenFileButtonCommand.RaiseCanExecuteChanged();
                DropFileButtonCommand.RaiseCanExecuteChanged();
            }
        } 



        public MainViewModel()
        {         
            this.Messages = new ObservableCollection<string>();
            this.Users = new ObservableCollection<string>();
            this.dataHandler = new DataHandler();
            this.LogMessages = new ObservableCollection<string>();


            //set command for start connection on the server side of application
            StartButtonCommand = new RelayCommand(
                () =>
                {
                    //action to be excecuted...
                    server = new ServerCommunication.Server(ip, portNr, UpdateMessageList);
                    server.AcceptingThread();
                    isConnected = true;
                    StartButtonCommand.RaiseCanExecuteChanged();
                    StopButtonCommand.RaiseCanExecuteChanged();
                }, () => !isConnected); //command can be excecuted only when bool expression is "true" 


            //set command for stopping the server on server window
            StopButtonCommand = new RelayCommand(
                () =>
                {
                    server.Stop(); //stop all connections and threads
                    isConnected = false;
                    StartButtonCommand.RaiseCanExecuteChanged();
                    StopButtonCommand.RaiseCanExecuteChanged();

                }, () => isConnected);


            //set command for disconnecting a user
            DropUserButtonCommand = new RelayCommand(
                () =>
                {
                    server.DropUser(_selectedUser); //stopp a specific user connection to the server
                    Users.Remove(_selectedUser); //remove User from the GUI´s list of connected users 
                    RaisePropertyChanged("Users");
                }, () =>
                {
                    return (SelectedUser != null); //returns true if the selected user to drop is not null
                });


            //set command for saving a log-file in the system
            SaveToLogBtnClickCommand = new RelayCommand(
                () =>
                {
                    dataHandler.Save(Messages.ToList());
                    RaisePropertyChanged("Files"); //updates the list of log-files in the GUI
                    SaveToLogBtnClickCommand.RaiseCanExecuteChanged();

                }, () => this.Messages != null);


            //set command for opening a log-file into the ListBox in the GUI
            OpenFileButtonCommand = new RelayCommand(
                () =>
                {
                    //load all messages from the selected file and save them in the variable "LogMessages"
                    this.LogMessages = new ObservableCollection<string>(dataHandler.Load(SelectedFile));

                    //updates the list of Messages to be displayed in the Log-ListBox on the GUI 
                    RaisePropertyChanged("LogMessages");

                }, () => SelectedFile != null); //only sets command when the selected file exists or contains messages


            //set command for deleting or "dropping" a log-file 
            DropFileButtonCommand = new RelayCommand(
                () =>
                {
                    this.dataHandler.Delete(SelectedFile);

                    //updates the list of log-files to be displayed in the GUI
                    RaisePropertyChanged("Files");

                }, () => this.SelectedFile != null);//only sets command when the selected file exists

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
