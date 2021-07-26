using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal abstract class Vehicle ////abstract class, all the vehicles created are inherited. all the vehicles that are created are inherited from it
    {
        ////**Fields**
        protected string m_OwnerName;
        protected string m_OwnerPhone;
        protected string m_LicenseNumber;
        protected string m_Model;
        protected float m_PercentageOfEnergyRemaining;
        protected float m_RemainingEnergy;
        protected float m_MaxEnergyCapacity;
        protected eVehicleStatus m_VehicleStatus;
        protected eFuelType m_FuelType;
        protected List<Wheel> m_Wheels;
        protected bool m_IsDataSet = false;

        //// **Properties**
        internal string OwnerName
        {
            get
            {
                return m_OwnerName;
            }
        }

        internal string OwnerPhone
        {
            get
            {
                return m_OwnerPhone;
            }
        }

        internal string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
        }

        internal eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }

        internal eFuelType FuelType
        {
            get
            {
                return m_FuelType;
            }
        }

        internal string Model
        {
            get
            {
                return m_Model;
            }
        }

        internal float PercentageOfEnergyRemaining ////Returns the current amount of energy in the vehicle. A liter of fuel in a regular engine or time in an electric engine
        {
            get
            {
                return m_PercentageOfEnergyRemaining;
            }

            set
            {
                m_PercentageOfEnergyRemaining = value;
            }
        }

        internal float RemainingEnergy
        {
            get
            {
                return m_RemainingEnergy;
            }
            set
            {
                m_RemainingEnergy = value;
            }
        }

        internal float MaxEnergyCapacity
        {
            get
            {
                return m_MaxEnergyCapacity;
            }
        }

        internal abstract float MaximumEnergyCapacity(); ////An abstract method implemented according to the engine and capacity of the vehicle

        // **Methods**
        internal abstract string DataToString(); ////An abstract method that prints all the data according to the vehicle created

        internal abstract Tuple<Type,string>[] GetParamsRequiredForCreationArr(); //// Return a array of Tuple<Type,String> which represent fields that not common to other Vehicle Sub-Classes. Type is the type of field, String - The explanation of the field

        internal virtual void SetInitialData(string i_OwnerName, string i_OwnerPhone, string i_LicenseNum, string i_Model, float i_EnergyLeft, string i_WheelManufacturer, int i_CurrentAirPressure, object[] i_Params)
        {
            ////Initializes the variables because an empty constructor is used and initializes everything to default values
            m_OwnerName = i_OwnerName;
            m_OwnerPhone = i_OwnerPhone;
            m_LicenseNumber = i_LicenseNum;
            m_Model = i_Model;
            m_RemainingEnergy = i_EnergyLeft;
            if (m_RemainingEnergy < 0 || m_RemainingEnergy > MaximumEnergyCapacity())
            {
                throw new ValueOutOfRangeException("Remaining energy invalid.", MaximumEnergyCapacity(), 0);
            }

            foreach (Wheel wheel in m_Wheels) 
            {
                if (i_CurrentAirPressure > wheel.MaximumAirPressure) 
                {
                    throw new ValueOutOfRangeException("Air pressure is max than allowed.", wheel.MaximumAirPressure, 0);
                }
                else
                {
                    wheel.CurrentAirPressure = i_CurrentAirPressure;
                    wheel.Manufacturer = i_WheelManufacturer;
                }
            }

            CalcEnergyRemainPrecent();  
        }

        internal void InflateWheels(int i_AirPressureToInflate)
        {
            if (m_Wheels != null) 
            {
                foreach (Wheel wheel in m_Wheels) 
                {
                    wheel.Inflation(i_AirPressureToInflate);
                }
            }
        }

        protected void CalcEnergyRemainPrecent()
        {
            m_PercentageOfEnergyRemaining = m_RemainingEnergy / m_MaxEnergyCapacity * 100;
        }
    }
}
