using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Shared.BaseModels;
using Shared.Interfaces;
using Shared.Models;
using Simulation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace CodingDojo2.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Simulator sim; //simulate data for datagrid in the GUI

        //empty list of Items that will be used to store the Items given by the Simulator
        private List<ItemVm> modelItems = new List<ItemVm>(); 
        public ObservableCollection<ItemVm> SensorList { get; set; }
        public ObservableCollection<ItemVm> ActorList { get; set; }
        public RelayCommand SensorAddBtnClickCmd { get; set; }
        public RelayCommand SensorDelBtnCmd { get; set; }
        public RelayCommand ActuatorAddBtnClickCmd { get; set; }
        public RelayCommand ActuatorDelBtnClickCmd { get; set; }
        private string currentTime = DateTime.Now.ToLocalTime().ToShortTimeString(); //current time in short formatted string 
        private string currentDate = DateTime.Now.ToLocalTime().ToShortDateString(); //current date in short dormatted string 

        public ObservableCollection<string> ModeSelectionList { get; private set; }

        public string CurrentDate
        {
            get { return currentDate; }
            set { currentDate = value; RaisePropertyChanged(); }
        }

        public string CurrentTime
        {
            get { return currentTime; }
            set { currentTime = value; RaisePropertyChanged(); }
        }


        public MainViewModel()
        {
            SensorList = new ObservableCollection<ItemVm>(); //list of sensors to be displayed
            ActorList = new ObservableCollection<ItemVm>(); //list of actuators to be displayed
            ModeSelectionList = new ObservableCollection<string>();

            //Fill ModeSelectionList
            foreach (var item in Enum.GetNames(typeof(SensorModeType)))   
            {
                //each enum value (Sensor´s different modes) are stored in the 'ModeSelectionList'
                ModeSelectionList.Add(item);
            }
            foreach (var item in Enum.GetNames(typeof(ModeType)))
            {
                ModeSelectionList.Add(item);

            }

            //for time /date update
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Tick += UpdateTime;

            //the full sensor and actuator data, as well as the timer only are started if the desing mode for the WPF-App is off
            if (!IsInDesignMode)
            {
                //load Data
                LoadData();

                //start timer for date/time update
                timer.Start();
            }

        }


        private void LoadData()
        {
            //loading the simulation data into the data grids with Binding 
            this.sim = new Simulator(modelItems);
            foreach (var item in sim.Items)
            {
                if (item.ItemType.Equals(typeof(ISensor)))
                    SensorList.Add(item);
                else if (item.ItemType.Equals(typeof(IActuator)))
                    ActorList.Add(item);
            }

        }



        private void UpdateTime(object sender, EventArgs e)
        {
            CurrentTime = DateTime.Now.ToLocalTime().ToShortTimeString();
            CurrentDate = DateTime.Now.ToLocalTime().ToShortDateString();
        }
    }
}