using CodingDojo6.Items;
using CodingDojo6.MessageBarProperties;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CodingDojo6.ViewModel
{
    public class MyToysViewModel : ViewModelBase
    {
        //private Messenger messenger = SimpleIoc.Default.GetInstance<Messenger>();
        public ObservableCollection<Toy> Selected { get; set; }

        private RelayCommand<Toy> _deleteFromListCommand;
        public RelayCommand<Toy> DeleteFromListCommand
        {
            get => _deleteFromListCommand;
            set
            {
                _deleteFromListCommand = value;
                RaisePropertyChanged("DeleteFormListCommand");
            }
        }

        public MyToysViewModel()
        {
            Selected = new ObservableCollection<Toy>();

            this.MessengerInstance.Register<PropertyChangedMessage<Toy>>(this, "Write" , updateSelectedList);


            DeleteFromListCommand = new RelayCommand<Toy>(
                (Item) =>
                {
                    Selected.Remove(Item);
                    this.MessengerInstance.Send<PropertyChangedMessage<Message>>(new PropertyChangedMessage<Message>(null, new Message("Item deleted", MessageState.Delete), "MessageBar"), "ScreenMessage");
                    (App.Current.Resources["Locator"] as ViewModelLocator).MessageBar.RegisterOnMessenger("ScreenMessage");
                
                });
        }

        private void updateSelectedList(PropertyChangedMessage<Toy> obj)
        {
            //list is updated with the new value of the list after recieving a property changed message
            Selected.Add(obj.NewValue);
        }
    }
}
