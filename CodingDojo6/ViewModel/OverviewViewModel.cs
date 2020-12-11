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
using System.Windows.Media.Imaging;

namespace CodingDojo6.ViewModel
{
    public class OverviewViewModel : ViewModelBase
    {
        //private Messenger messenger = SimpleIoc.Default.GetInstance<Messenger>(); 
        public ObservableCollection<ToyGroup> ToyGroups { get; set; }

        //current group between LEGOS, and Consoles
        private ToyGroup _currentToyGroup;
        public ToyGroup CurrentToyGroup
        {
            get => _currentToyGroup;
            set
            {
                _currentToyGroup = value;
                RaisePropertyChanged("CurrentToyGroup");
            }
        }


        //command that starts when user clicks the Select-Button for a specific toy in the GUI
        private RelayCommand<Toy> _selectToyButtonCommand;
        public RelayCommand<Toy> SelectToyButtonCommand
        {
            get => _selectToyButtonCommand;
            set
            {
                _selectToyButtonCommand = value;
                RaisePropertyChanged("SelectToyButtonCommand");
            }
        }


        //constructor
        public OverviewViewModel()
        {
            ToyGroups = new ObservableCollection<ToyGroup>();
            GenerateDemoData();

            SelectToyButtonCommand = new RelayCommand<Toy>(
                (Item) =>
                {
                    this.MessengerInstance.Send<PropertyChangedMessage<Toy>>(new PropertyChangedMessage<Toy>(null, Item, "AddToys"), "Write");

                    this.MessengerInstance.Send<PropertyChangedMessage<Message>>(new PropertyChangedMessage<Message>(null, new Message("New Entry Added", MessageState.Ok), ""), "@Message");
                });

        }






        private void GenerateDemoData()
        {
            //Generates new demo toy categories, in this case LEGO, Retro Gaming.Consoles and Modern gaming Consoles
            ToyGroups.Add(new ToyGroup("LEGO", new BitmapImage(new Uri("../Images/LEGO.jpg", UriKind.Relative))));
            ToyGroups.Add(new ToyGroup("Retro Consoles", new BitmapImage(new Uri("../Images/RetroCoonsoles.jpg", UriKind.Relative))));
            ToyGroups.Add(new ToyGroup("Modern Consoles", new BitmapImage(new Uri("../Images/ModernConsoles.jpg", UriKind.Relative))));

            //Adding LEGO Sets
            ToyGroups[0].AddToys(new Toy("LEGO Batman", new BitmapImage(new Uri("../Images/LEGO_Batman.jpg", UriKind.Relative)), "5+"));
            ToyGroups[0].AddToys(new Toy("LEGO City", new BitmapImage(new Uri("../Images/LEGO_City.jpg", UriKind.Relative)), "5+"));
            ToyGroups[0].AddToys(new Toy("LEGO Star Wars", new BitmapImage(new Uri("../Images/LEGO_Starwars.jpg", UriKind.Relative)), "5+"));

            //Adding Consoles to the "Retro Consoles" list
            ToyGroups[1].AddToys(new Toy("Gameboy Color", new BitmapImage(new Uri("../Images/GameBoyColor.jpg", UriKind.Relative)), "6+"));
            ToyGroups[1].AddToys(new Toy("Gameboy Advanced", new BitmapImage(new Uri("../Images/GameBoyAdvanced.jpg", UriKind.Relative)), "6+"));
            ToyGroups[1].AddToys(new Toy("Playstation 1", new BitmapImage(new Uri("../Images/PS1.jpg", UriKind.Relative)), "3+"));
            ToyGroups[1].AddToys(new Toy("Sega Genesis", new BitmapImage(new Uri("../Images/SegaGenesis.jpg", UriKind.Relative)), "7+"));

            //Adding Consoles to the "Modern Consoles" list
            ToyGroups[2].AddToys(new Toy("Playstation 4", new BitmapImage(new Uri("../Images/PS4.jpg", UriKind.Relative)), "6+"));
            ToyGroups[2].AddToys(new Toy("Playstation 5", new BitmapImage(new Uri("../Images/PS5.jpg", UriKind.Relative)), "6+"));
            ToyGroups[2].AddToys(new Toy("XBOX One", new BitmapImage(new Uri("../Images/XBOX_One.jpg", UriKind.Relative)), "6+"));
            ToyGroups[2].AddToys(new Toy("XBOX Series X", new BitmapImage(new Uri("../Images/XBOX_X.jpg", UriKind.Relative)), "6+"));
            ToyGroups[2].AddToys(new Toy("Nintendo Switch", new BitmapImage(new Uri("../Images/Switch.jpg", UriKind.Relative)), "6+"));

        }
    }
}
