using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class GarageManger
    {
        private readonly Dictionary<string, Vehicle> r_VehiclesDict = new Dictionary<string, Vehicle>();

        // Get a vehicle and add it into Garage Dictionary
        public void InsertVehicle(object i_VehicleToInsert)
        {
            Vehicle vehicleToInsert = (i_VehicleToInsert as Vehicle);
            bool isVehicleExistInGarage = IsCarExistInGarage(vehicleToInsert.LicenseNumber);

            if (!isVehicleExistInGarage)
            {
                r_VehiclesDict.Add(vehicleToInsert.LicenseNumber,vehicleToInsert);
            }
        }

        // Return a string with all vehicle's license number that are in the requested status.
        public string GetLicenseNumbersByFilter(eVehicleStatus i_FilterType)
        {
            StringBuilder licensesString = new StringBuilder("License numbers filtered by status: " + i_FilterType.ToString() +"\n");

            foreach (Vehicle vehicle in r_VehiclesDict.Values)
            {
                if (vehicle.VehicleStatus == i_FilterType) 
                {
                    licensesString.Append(vehicle.LicenseNumber + "\n");
                }
            }

            return licensesString.ToString();
        }

        // Return a string with all the vehicle's license number that in the garage.
        public string GetAllLicenseNumbers()
        {
            StringBuilder licensesString = new StringBuilder("License numbers:\n");

            foreach (Vehicle vehicle in r_VehiclesDict.Values)
            { 
                licensesString.Append(vehicle.LicenseNumber + "\n");
            }

            return licensesString.ToString();
        }

        // Update vehicle's status according to requested status. Return false in case of no match vehicle in the garage, else true.
        public bool ChangeVehicleStatus(string i_LicenseNumber,eVehicleStatus i_StatusType)////Add an exception if no license plate is found
        {
            bool isCarExist = IsCarExistInGarage(i_LicenseNumber);

            if (isCarExist)
            {
                r_VehiclesDict[i_LicenseNumber].VehicleStatus = (eVehicleStatus)i_StatusType;
            }

            return isCarExist;
        }

        // Update vehicle's wheels air pressure according to requested add of air. Return false in case of no match vehicle in the garage, else true.
        public bool InflateWheels(string i_LicenseNumber, int i_AirPressureToAdd)////Add an exception if no license plate is found
        {
            bool isCarExist = IsCarExistInGarage(i_LicenseNumber);

            if (isCarExist)
            {
                Vehicle vehicle = r_VehiclesDict[i_LicenseNumber];
                vehicle.InflateWheels(i_AirPressureToAdd);
            }

            return isCarExist;
        }

        // Update vehicle's fuel status according to requested add of fuel. Return false in case of no match vehicle in the garage, else true.
        public bool RefuleCar(string i_LicenseNumber,eFuelType i_SelectedFuel, float i_AmountToFuel) 
        {
            bool isCarExist = IsCarExistInGarage(i_LicenseNumber);

            if (isCarExist)
            {
                eFuelType selectedFuel = (eFuelType)i_SelectedFuel;
                Vehicle vehicleToFuel = r_VehiclesDict[i_LicenseNumber];

                if (vehicleToFuel.FuelType == eFuelType.Electric)
                {
                    throw new ArgumentException("None fuel engine car choosed.");
                }
                else
                {
                    (vehicleToFuel as IStandardEngine).Refueling(selectedFuel,i_AmountToFuel);
                }
            }

            return isCarExist;
        }

        // Update vehicle's energy status according to requested add of energy. Return false in case of no match vehicle in the garage, else true.
        public bool ChargeElectricVehicle(string i_LicenseNumber, float i_AmountToCharge)
        {
            bool isCarExist = IsCarExistInGarage(i_LicenseNumber);
            if (isCarExist)
            {
                Vehicle vehicleToFuel = r_VehiclesDict[i_LicenseNumber];

                if (vehicleToFuel.FuelType != eFuelType.Electric)
                {
                    throw new ArgumentException("None electric engine car choosed.");
                }
                else
                {
                    (vehicleToFuel as IElectricEngine).BatteryCharging(i_AmountToCharge);
                }
            }

            return isCarExist;
        }

        // Return a string with all data of all vehicles that in the garage.
        public string GetAllVehicleDataString()
        {
            StringBuilder dataString = new StringBuilder();

            foreach (Vehicle vehicle in r_VehiclesDict.Values)
            {
                dataString.Append(vehicle.DataToString() + "\n");
            }

            return dataString.ToString();
        }

        public bool IsCarExistInGarage(string i_LicenseNumber)
        {
            return r_VehiclesDict.ContainsKey(i_LicenseNumber);
        }
    }
}
