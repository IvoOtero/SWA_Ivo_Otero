using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dojo2
{
    public class MainViewModel : ViewModelBase
    {

        private string _title;

        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                Title = "Ivo´s Smarthome System";
            } else
            {
                Title = "Main Title";
            }
        }


       // public RelayCommand 


        public string Title
        {
            get =>  _title; 
            set => Set(ref _title, value, nameof(Title)); 
        }
    }
}
