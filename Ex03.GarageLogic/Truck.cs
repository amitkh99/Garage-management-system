using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Truck : Vehicle, IStandardEngine
    {
        //// **Const**
        protected const int k_NumberOfWheels = 16;
        protected const float k_MaximumAirPressure = 26;
        protected const float k_MaximumEnergyCapacity = (float)120;
        protected const eFuelType k_FuelType = eFuelType.Soler;

        protected static Tuple<Type, string>[] s_ParamRequiredToCreate = // Holds The explanation of the field and its type that relevant to the specific vehicle
            {
                new Tuple<Type, string>(typeof(bool), "Is driving hazardous materials"),
                new Tuple<Type, string>(typeof(float), "Maximum carry weight")
            };

        ////**Fields**
        protected bool m_IsDrivingHazardousMaterials;
        protected float m_MaximumCarryingWeight;

        //// **Properties**
        internal bool IsDrivingHazardousMaterials
        {
            get
            {
                return m_IsDrivingHazardousMaterials;
            }
        }

        internal float MaximumCarryingWeight
        {
            get
            {
                return m_MaximumCarryingWeight;
            }
        }

        eFuelType IStandardEngine.GetFuelType()
        {
            return k_FuelType;
        }

        float IStandardEngine.GetCurrentAmountOfFuel()
        {
            return PercentageOfEnergyRemaining;
        }

        float IStandardEngine.GetMaximumAmountOfFuel()
        {
            return k_MaximumEnergyCapacity;
        }

        // **Methods**
        internal Truck() ////Defines the relevant fields for the specific vehicle (SetInitialData will be run after creation to complete the initialization)
        {
            m_FuelType = k_FuelType;
            m_MaxEnergyCapacity = k_MaximumEnergyCapacity;
            m_Wheels = new List<Wheel>(k_NumberOfWheels);

            for (int i = 0; i < k_NumberOfWheels; i++) 
            {
                m_Wheels.Add(new Wheel(k_MaximumAirPressure));
            }
        }

        internal override string DataToString()
        {
            int index = 1;
            StringBuilder dataString = new StringBuilder();
            dataString.Append(string.Format("{0}.Vehicle type: {1}\n", index++, this.GetType().Name));
            dataString.Append(string.Format("{0}.Owner name: {1}\n", index++, OwnerName));
            dataString.Append(string.Format("{0}.Owner phone: {1}\n", index++, OwnerPhone));
            dataString.Append(string.Format("{0}.License number: {1}\n", index++, LicenseNumber));
            dataString.Append(string.Format("{0}.Vehicle status: {1}\n", index++, VehicleStatus));
            dataString.Append(string.Format("{0}.Fuel type: {1}\n", index++, FuelType));
            dataString.Append(string.Format("{0}.Model: {1}\n", index++, Model));
            dataString.Append(string.Format("{0}.Percentage of energy remaining: {1}%\n", index++, PercentageOfEnergyRemaining));
            dataString.Append(string.Format("{0}.Tire manufacturer: {1}\n", index++, m_Wheels[0].Manufacturer));
            dataString.Append(string.Format("{0}.Current air pressure: {1}\n", index++, m_Wheels[0].CurrentAirPressure));
            dataString.Append(string.Format("{0}.Drives hazardous materials: {1}\n", index++, IsDrivingHazardousMaterials));
            dataString.Append(string.Format("{0}.Maximum carrying weight: {1}\n", index++, MaximumCarryingWeight));
            return dataString.ToString();
        }

        internal override Tuple<Type,string>[] GetParamsRequiredForCreationArr()
        {
            return s_ParamRequiredToCreate;
        }

        internal override float MaximumEnergyCapacity()
        {
            return k_MaximumEnergyCapacity;
        }

        internal override void SetInitialData(string i_OwnerName, string i_OwnerPhone, string i_LicenseNum, string i_Model, float i_EnergyLeft, string i_WheelManufacturer, int i_CurrentAirPressure, object[] i_Params)
        {
            ////Initializes the variables because an empty constructor is used and initializes everything to default values
            if (m_IsDataSet == false) 
            {
                base.SetInitialData(i_OwnerName, i_OwnerPhone, i_LicenseNum, i_Model, i_EnergyLeft,i_WheelManufacturer,i_CurrentAirPressure, i_Params);
                m_IsDrivingHazardousMaterials = (bool)i_Params[0];
                m_MaximumCarryingWeight = (float)i_Params[1];
                if (m_MaximumCarryingWeight < 0) 
                {
                    throw new ValueOutOfRangeException("Max carry weight cant be negative.", float.MaxValue, 0);
                }

                m_IsDataSet = true;
            }
        }

        void IStandardEngine.Refueling(eFuelType i_FuelType, float i_litersToAdd)
        {
            if (i_FuelType != m_FuelType)
            {
                throw new ArgumentException("Wrong fuel type.");
            }

            if (RemainingEnergy + i_litersToAdd <= MaxEnergyCapacity)
            {
                RemainingEnergy += i_litersToAdd;
                CalcEnergyRemainPrecent();
            }
            else
            {
                float minToAdd = 0;
                float maxToAdd = MaxEnergyCapacity - RemainingEnergy;
                throw new ValueOutOfRangeException("To much energy to add.", maxToAdd, minToAdd);
            }
        }
    }
}
