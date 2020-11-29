using CodingDojo5.Items;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CodingDojo5
{
    public class ToyGroup : ViewModelBase
    {
        public string Description { get; set; }
        public BitmapImage Image { get; set; }

        public ObservableCollection<Toy> ToyList { get; set; }

        public ToyGroup(string description, BitmapImage image)
        {
            this.Description = description;
            this.Image = image;
        }

        public void AddToys(Toy toy)
        {
            if (ToyList == null)
            {
                ToyList = new ObservableCollection<Toy>();
            }

            ToyList.Add(toy);
        }

    }
}
