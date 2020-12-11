
using CodingDojo6.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingDojo6
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<OverviewViewModel>();
            SimpleIoc.Default.Register<MyToysViewModel>();
            SimpleIoc.Default.Register<ViewModelNavigation>();
            //SimpleIoc.Default.Register<Messenger>();
            SimpleIoc.Default.Register<MessageBarViewModel>();



        }
        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();
        public OverviewViewModel Overview => SimpleIoc.Default.GetInstance<OverviewViewModel>();
        public MyToysViewModel MyToys => SimpleIoc.Default.GetInstance<MyToysViewModel>();
        public MessageBarViewModel MessageBar => SimpleIoc.Default.GetInstance<MessageBarViewModel>();




    }
}
