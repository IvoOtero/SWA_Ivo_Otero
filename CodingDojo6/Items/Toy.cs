using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace CodingDojo6.Items
{
    public class Toy : ViewModelBase
    {
        public string Description { get; set; }
        public BitmapImage Image { get; set; }
        public string RecommendedAge { get; set; }

        public Toy(string description, BitmapImage image, string recommendedAge)
        {
            this.Description = description;
            this.Image = image;
            this.RecommendedAge = recommendedAge;
        }

    }
}
