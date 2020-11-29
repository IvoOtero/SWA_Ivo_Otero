using CodingDojo5.Items;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Media.Imaging;

namespace CodingDojo5
{
    public class MainViewModel : ViewModelBase 
    {


        public ObservableCollection<ToyGroup> Toys { get; set; }
        public ObservableCollection<Toy> Selected { get; set; }

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


        public MainViewModel()
        {
            Selected = new ObservableCollection<Toy>();
            Toys = new ObservableCollection<ToyGroup>();
            GenerateDemoData();

            SelectToyButtonCommand = new RelayCommand<Toy>(
                (Item) =>
                {
                    Selected.Add(Item);
                });

            DeleteFromListCommand = new RelayCommand<Toy>(
                (Item) =>
                {
                    Selected.Remove(Item);
                });



        }


        private void GenerateDemoData()
        {
            //Generates new demo toy categories, in this case LEGO, Retro Gaming.Consoles and Modern gaming Consoles
            Toys.Add(new ToyGroup("LEGO", new BitmapImage(new Uri("Images/LEGO.jpg", UriKind.Relative))));
            Toys.Add(new ToyGroup("Retro Consoles", new BitmapImage(new Uri("Images/RetroCoonsoles.jpg", UriKind.Relative))));
            Toys.Add(new ToyGroup("Modern Consoles", new BitmapImage(new Uri("Images/ModernConsoles.jpg", UriKind.Relative))));

            //Adding LEGO Sets
            Toys[0].AddToys(new Toy("LEGO Batman", new BitmapImage(new Uri("Images/LEGO_Batman.jpg", UriKind.Relative)), "5+"));
            Toys[0].AddToys(new Toy("LEGO City", new BitmapImage(new Uri("Images/LEGO_City.jpg", UriKind.Relative)), "5+"));
            Toys[0].AddToys(new Toy("LEGO Star Wars", new BitmapImage(new Uri("Images/LEGO_Starwars.jpg", UriKind.Relative)), "5+"));

            //Adding Consoles to the "Retro Consoles" list
            Toys[1].AddToys(new Toy("Gameboy Color", new BitmapImage(new Uri("Images/GameBoyColor.jpg", UriKind.Relative)), "6+"));
            Toys[1].AddToys(new Toy("Gameboy Advanced", new BitmapImage(new Uri("Images/GameBoyAdvanced.jpg", UriKind.Relative)), "6+"));
            Toys[1].AddToys(new Toy("Playstation 1", new BitmapImage(new Uri("Images/PS1.jpg", UriKind.Relative)), "3+"));
            Toys[1].AddToys(new Toy("Sega Genesis", new BitmapImage(new Uri("Images/SegaGenesis.jpg", UriKind.Relative)), "7+"));

            //Adding Consoles to the "Modern Consoles" list
            Toys[2].AddToys(new Toy("Playstation 4", new BitmapImage(new Uri("Images/PS4.jpg", UriKind.Relative)), "6+"));
            Toys[2].AddToys(new Toy("Playstation 5", new BitmapImage(new Uri("Images/PS5.jpg", UriKind.Relative)), "6+"));
            Toys[2].AddToys(new Toy("XBOX One", new BitmapImage(new Uri("Images/XBOX_One.jpg", UriKind.Relative)), "6+"));
            Toys[2].AddToys(new Toy("XBOX Series X", new BitmapImage(new Uri("Images/XBOX_X.jpg", UriKind.Relative)), "6+"));
            Toys[2].AddToys(new Toy("Nintendo Switch", new BitmapImage(new Uri("Images/Switch.jpg", UriKind.Relative)), "6+"));

        }
    }
}
