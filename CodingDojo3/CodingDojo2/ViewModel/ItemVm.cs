using GalaSoft.MvvmLight;
using Shared.BaseModels;
using Shared.Interfaces;
using Shared.Models;
using System;

namespace CodingDojo2.ViewModel
{
    public class ItemVm : ViewModelBase
    {
        private ItemBase baseItem;


        public int Id
        {
            get => baseItem.Id; 
        }

        public string Description
        {
            get => baseItem.Description; 
            set { baseItem.Description = value; RaisePropertyChanged(); }
        }

        public string Name
        {
            get => baseItem.Name; 
            set { baseItem.Name = value; RaisePropertyChanged(); }
        }

        public string Room
        {
            get => baseItem.Room; 
            set { baseItem.Room = value; RaisePropertyChanged(); }
        }

        public int PosX
        {
            get => baseItem.PosX; 
            set { baseItem.PosX = value; RaisePropertyChanged(); }
        }

        public int PosY
        {
            get => baseItem.PosY; 
            set { baseItem.PosY = value; RaisePropertyChanged(); }
        }

        public string ValueType
        {
            get
            {
                //if the item is a Sensor, then the type of sensor will be returned, same logic if actuator
                if (baseItem is ISensor)
                    return (baseItem as ISensor).SensorValueType.Name;
                else if (baseItem is IActuator)
                    return (baseItem as IActuator).ActuatorValueType.Name;
                else
                    throw new NotImplementedException();
            }

        }


        
        public Type ItemType
        {
            //gets what type of item (with c# typeof) the elements are
            get
            {
                if (baseItem is ISensor) return typeof(ISensor);
                else if (baseItem is IActuator) return typeof(IActuator);
                else throw new NotImplementedException();
            }
        }


        public string Mode
        {
            get
            {
                // if Item is a Sensor, the mode (enabled or disabled) will be returned
                if (baseItem is ISensor) return (baseItem as ISensor).SensorMode.ToString();
                if (baseItem is IActuator) return (baseItem as IActuator).ActuatorMode.ToString();
                else return null;
            }
            set
            {
                //sets the Mode of the Item as an Enum that can be either 'Enabled' or 'Disabled'
                if (baseItem is ISensor)
                    (baseItem as ISensor).SensorMode = (SensorModeType)Enum.Parse(typeof(SensorModeType), value, false);
                if (baseItem is IActuator)
                    (baseItem as BaseActuator).ActuatorMode = (ModeType)Enum.Parse(typeof(ModeType), value, false);

                RaisePropertyChanged();
            }
        }

        public object Value
        {
            get
            {
                if (baseItem is ISensor)
                    return (baseItem as ISensor).SensorValue;
                else if (baseItem is IActuator)
                    return (baseItem as IActuator).ActuatorValue;
                else throw new NotImplementedException();
            }

            set
            {
                //else if (baseItem is IActuator) (baseItem as BaseActuator).ActuatorValue = value;
                
                if (baseItem is ISensor) (baseItem as ISensor).SensorValue = value;
                else if (baseItem is IActuator) (baseItem as IActuator).ActuatorValue = value;
                else throw new NotImplementedException();
                RaisePropertyChanged();

            }
        }



        public ItemVm(ISensor sensor)
        {
            //if a Sensor is passed as variable, then the 'baseItem' becomes a sensor with all attributes form the 'ItemBase' abstract class
            baseItem = sensor as ItemBase;
        }

        public ItemVm(IActuator actuator)
        {
            //if an Actuator is passed as variable, then the 'baseItem' becomes a sensor with all attributes form the 'ItemBase' abstract class
            baseItem = actuator as ItemBase;
        }
    }
}