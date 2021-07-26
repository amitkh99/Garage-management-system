using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal abstract class Car : Vehicle
    {
        //// **Const**
        protected const int k_NumberOfWheels = 4;
        protected const float k_MaximumAirPressure = 32;

        //// **Static**
        protected static Tuple<Type,string>[] s_ParamRequiredToCreate = { new Tuple<Type, string>(typeof(eColor), "Car color"), new Tuple<Type, string>(typeof(eNumberOfDoors), "Number of doors") }; // Holds The explanation of the field and its type that relevant to the specific vehicle

        ////**Fields**
        protected eColor m_Color;
        protected eNumberOfDoors m_DoorsNumber;

        //// **Properties**
        internal eNumberOfDoors DoorsNumber
        {
            get
            {
                return m_DoorsNumber;
            }
        }

        internal eColor Color
        {
            get
            {
                return m_Color;
            }
        }

        //// **Methods**
        internal Car() ////Defines the relevant fields for the specific vehicle (SetInitialData will be run after creation to complete the initialization)
        {
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
            dataString.Append(string.Format("{0}.fuelType: {1}\n", index++, FuelType));
            dataString.Append(string.Format("{0}.Model: {1}\n", index++,Model));
            dataString.Append(string.Format("{0}.Percentage of energy remaining: {1}%\n", index++, PercentageOfEnergyRemaining));
            dataString.Append(string.Format("{0}.Tire manufacturer: {1}\n", index++, m_Wheels[0].Manufacturer));
            dataString.Append(string.Format("{0}.Current air pressure: {1}\n", index++, m_Wheels[0].CurrentAirPressure));
            dataString.Append(string.Format("{0}.Color: {1}\n", index++, Color));
            dataString.Append(string.Format("{0}.Number of doors: {1}\n", index++, DoorsNumber));
            return dataString.ToString();
        }

        internal override Tuple<Type,string>[] GetParamsRequiredForCreationArr() // Return a array of Tuple<Type,String> which represent fields that not common to other Vehicle Sub-Classes. Type is the type of field, String - The explanation of the field
        {
            return s_ParamRequiredToCreate;
        }

        internal override void SetInitialData(string i_OwnerName, string i_OwnerPhone, string i_LicenseNum, string i_Model, float i_EnergyLeft, string i_WheelManufacturer, int i_CurrentAirPressure, object[] i_Params)
        {
            if (m_IsDataSet == false)
            {
                base.SetInitialData(i_OwnerName, i_OwnerPhone, i_LicenseNum, i_Model, i_EnergyLeft, i_WheelManufacturer, i_CurrentAirPressure, i_Params);
                m_Color = (eColor)i_Params[0];
                m_DoorsNumber = (eNumberOfDoors)i_Params[1];
                m_IsDataSet = true;
            }
        }
    }
}
