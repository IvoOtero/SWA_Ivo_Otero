﻿using CodingDojo6.MessageBarProperties;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace CodingDojo6.ViewModel
{
    public class MessageBarViewModel : ViewModelBase
    {
        //private Messenger messenger;
        private DispatcherTimer timer;

        private string _message;
        public string Message
        {
            get => _message;
            set { _message = value; RaisePropertyChanged(); }
        }


        private BitmapImage _symbol;
        public BitmapImage Symbol
        {
            get => _symbol;
            set { _symbol = value; RaisePropertyChanged(); }
        }


        private Visibility _visible;
        public Visibility Visible
        {
            get => _visible;
            set { _visible = value; RaisePropertyChanged(); }
        }

        //constructor MessageBar
        public MessageBarViewModel()
        {
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 2); //message should only appear 2 seconds on screen after selecting an item

        }

        //method that shows the info in the message
        public void ShowInfo(Message msg)
        {
            //display message on screen
            Visible = Visibility.Visible;

            //switch method that trecieves the message state of the incoming message, this can be an error or success message
            switch (msg.State)
            {                
                case MessageState.Error:
                    Symbol = new BitmapImage(new Uri("../Images/error.png", UriKind.Relative));
                    break;
                case MessageState.Ok:
                    Symbol = new BitmapImage(new Uri("../Images/ok.png", UriKind.Relative));
                    break;
                default:
                    break;
            }

            //set the message to display on screen and start the timer (2 seconds)
            Message = msg.Text;
            timer.Start();
        }


        //Register on Messenger (recieves the message)
        public void RegisterOnMessenger(string token)
        {
            //this.messenger = messenger;
            this.MessengerInstance.Register<PropertyChangedMessage<Message>>(this, token, showContent);
        }

        //Display Info
        private void showContent(PropertyChangedMessage<Message> obj)
        {
            ShowInfo(obj.NewValue);
        }






    }
}
