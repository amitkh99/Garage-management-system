using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    /* This class is used to create instances on fly according to user requested type in 2 steps:
       1. Initially an empty instance of the requested type create, And an array of Tuple<Type,String> is returning to UI in order to know which parameters it should get from user.
       2. after the UI call SetVehicleData with the requested parameters - The recent instance that created get all of his data.
    */
    public class CreateObjectForInsertion
    {
        private static readonly List<eVehicleType> sr_SupportedVehiclesList = Enum.GetValues(typeof(eVehicleType)).Cast<eVehicleType>().ToList();
        private static Vehicle s_CurrentVehicleToCreate = null;
        private static eVehicleType s_CurrentTypeToCreate;

        // This method create a empty instance according to requested type. Return an array of Tuple<Type,String> - which every tuple in the array represent an EXTRA parameter to get - beside Vehicle parameters.
        public static Tuple<Type,string>[] CreateNewVehicle(eVehicleType i_Type)
        {
            Vehicle newVehicle = null;

            switch (i_Type)
            {
                case eVehicleType.RegularCar:
                    newVehicle = new RegularCar();
                    break;
                case eVehicleType.ElectricCar:
                    newVehicle = new ElectricCar();
                    break;
                case eVehicleType.RegularMotorcycle:
                    newVehicle = new RegularMotorcycle();
                    break;
                case eVehicleType.ElectricMotorcycle:
                    newVehicle = new ElectricMotorcycle();
                    break;
                case eVehicleType.Truck:
                    newVehicle = new Truck();
                    break;
            }

            s_CurrentVehicleToCreate = newVehicle; // Update reference to current vehicle to update it data from user.
            s_CurrentTypeToCreate = i_Type;
            return newVehicle.GetParamsRequiredForCreationArr();
        }

        // This method get first 7 parameters which common to every type of Vehicle, And and object array which contain specific parameters to instace. The method update all Vehicle data.
        public static object SetVehicleData(string i_OwnerName,string i_OwnerPhone, string i_LicenseNum, string i_Model, float i_EnergyLeft,string i_WheelManufacturer,int i_CurrentAirPressure, object[] i_Params)
        {
            if (s_CurrentVehicleToCreate != null)
            { 
                s_CurrentVehicleToCreate.SetInitialData(i_OwnerName, i_OwnerPhone, i_LicenseNum, i_Model, i_EnergyLeft, i_WheelManufacturer,i_CurrentAirPressure, i_Params);
            }

            return s_CurrentVehicleToCreate;
        }
    }
}


