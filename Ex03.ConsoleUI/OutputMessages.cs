using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.ConsoleUI
{
    internal class OutputMessages
    {
        public static string MenuMsg()
        {
            int index_main = 1;
            StringBuilder mainString = new StringBuilder();
            mainString.Append(string.Format("What is the requested action?\n"));
            mainString.Append(string.Format("{0}.Insert new Vehicle to the garage\n", index_main++));
            mainString.Append(string.Format("{0}.View the list of vehicle license numbers in the garage (you can filter by their status)\n", index_main++));
            mainString.Append(string.Format("{0}.Change the status of a vehicle in the garage\n", index_main++));
            mainString.Append(string.Format("{0}.Inflate wheels of vehicle\n", index_main++));
            mainString.Append(string.Format("{0}.Refuel a vehicle powered by fuel\n", index_main++));
            mainString.Append(string.Format("{0}.Charge electric vehicle\n", index_main++));
            mainString.Append(string.Format("{0}.View complete vehicles data (by license number)\n", index_main++));
            mainString.Append(string.Format("{0}.Finish Work", index_main++));
            return mainString.ToString();
        }

        public static string PauseForDisplayMsg()
        {
            return "Press Enter to continue";
        }

        public static string AskLicenseNumberMsg()
        {
            return "a license number";
        }

        public static string AskOwnerNameMsg()
        {
            return "the owner name";
        }

        public static string AskOwnerPhoneMsg()
        {
            return "the owner phone";
        }

        public static string AskLicensePlateMsg()
        {
            return "a license plate";
        }

        public static string AskModelMsg()
        {
            return "the model";
        }

        public static string AskEnergyLeftMsg()
        {
            return "the amount of energy remaining in its energy source";
        }

        public static string AskWheelManufacturerMsg()
        {
            return "the name of the wheel manufacturer";
        }

        public static string AskCurrentAirPressureMsg()
        {
            return "the current amount of air pressure in the tires";
        }

        public static string AskStatusToPrintMsg()
        {
            return "Please select the status that you want filter by";
        }

        public static string AskAmountOfFuelMsg()
        {
            return "Please enter amount of fuel in litters.";
        }

        public static string AskChargingMsg()
        {
            return "Please enter desired charge time in hours.";
        }

        public static string VechileDoesntExist()
        {
            return "Operation failed - Vehicle is not exist in the garage.";
        }

        public static string VechileAlreadyExist()
        {
            return "Vehicle already exist in the garage - Status changed to InRepair.";
        }
    }
}
