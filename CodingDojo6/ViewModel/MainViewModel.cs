using CodingDojo6.Items;
using CodingDojo6.ViewModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media.Imaging;

namespace CodingDojo6
{
    public class MainViewModel : ViewModelBase 
    {


        public ViewModelNavigation navigation = SimpleIoc.Default.GetInstance<ViewModelNavigation>();

        public ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; RaisePropertyChanged("CurrentView"); }
        }

        

        private RelayCommand _switchToOverviewViewCommand;
        public RelayCommand SwitchToOverviewViewCommand
        {
            get => _switchToOverviewViewCommand;
            set
            {
                _switchToOverviewViewCommand = value;
                RaisePropertyChanged("SwitchToOverviewViewCommand");
            }

        }

        private RelayCommand _switchToMyToysViewCommand;
        public RelayCommand SwitchToMyToysViewCommand
        {
            get => _switchToMyToysViewCommand;
            set
            {
                _switchToMyToysViewCommand = value;
                RaisePropertyChanged("SwitchToMyToysViewCommand");
            }

        }


        //constructor for MainViewModel
        public MainViewModel()
        {

            SwitchToOverviewViewCommand = new RelayCommand(
                () =>
                {
                    CurrentView = navigation.NavigateTo("Overview");
                    RaisePropertyChanged("CurrentView");

                });

            SwitchToMyToysViewCommand = new RelayCommand(
                () =>
                {
                    CurrentView = navigation.NavigateTo("MyToys");
                    RaisePropertyChanged("CurrentView");
                });

            //display the default view when running (overview view in this case)
            CurrentView = navigation.NavigateTo("Overview");


            //init MessageBar by geting the ViewModelLocator via the App Resources
            //(App.Current.Resources["Locator"] as ViewModelLocator).MessageBar.RegisterOnMessenger("@Message");
            //after that messages sent via messanger at @Message are displayed in the bar

        }



    }
}
