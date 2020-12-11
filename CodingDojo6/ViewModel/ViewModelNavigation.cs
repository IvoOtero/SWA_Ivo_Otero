using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingDojo6.ViewModel
{
    public class ViewModelNavigation
    {
        private string currentViewDescription = null;


        public ViewModelBase NavigateTo(string viewDescription)
        {
            return SimpleFactory(viewDescription);
        }


        //Method that returns the chosen ViewModel based on a given string
        private ViewModelBase SimpleFactory(string viewDescription)
        {

            switch (viewDescription)
            {
                case "Overview":
                    this.currentViewDescription = viewDescription;
                    return SimpleIoc.Default.GetInstance<OverviewViewModel>();

                case "MyToys":
                    this.currentViewDescription = viewDescription;
                    return SimpleIoc.Default.GetInstance<MyToysViewModel>();

                default:
                    return SimpleFactory(currentViewDescription);
            }


        }
    }
}
