using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class ConsoleUI
    {
        internal enum eMenuOperations
        {
            Insert = 1, PrintVehiclesLicenses, ChangeVehicleStatus, InflateVehicleWheels, RefuelVehicle, ChargeVehicle, PrintAllVehiclesData, Finish
        }

        private readonly GarageManger r_GarageManger = new GarageManger();

        public ConsoleUI()
        {
            Menu(); // Start program.
        }

        public void Menu()
        {
            bool isFinishWork = false;
            while (!isFinishWork)
            {
                bool isCarExist = true;

                Console.Clear();
                Console.WriteLine(OutputMessages.MenuMsg());
                int selectedOperation = InputGetter.GetOperation();
                try
                {
                    switch ((eMenuOperations)selectedOperation)
                    {
                        case eMenuOperations.Insert:
                            bool isCarInserted = insertVehicleToTheGarage();
                            if (!isCarInserted)
                            {
                                Console.WriteLine(OutputMessages.VechileAlreadyExist());
                            }

                            break;
                        case eMenuOperations.PrintVehiclesLicenses:
                            printLicenseNumbers();
                            break;
                        case eMenuOperations.ChangeVehicleStatus:
                            isCarExist = changingVehicleStatus();
                            break;
                        case eMenuOperations.InflateVehicleWheels:
                            isCarExist = inflateWheels();
                            break;
                        case eMenuOperations.RefuelVehicle:
                            isCarExist = carRefueling();
                            break;
                        case eMenuOperations.ChargeVehicle:
                            isCarExist = chargingElectricVehicle();
                            break;
                        case eMenuOperations.PrintAllVehiclesData:
                            printAllVehicleDataInTheGarage();
                            break;
                        case eMenuOperations.Finish:
                            isFinishWork = true;
                            break;
                    }

                    if (isCarExist == false)
                    {
                        Console.WriteLine(OutputMessages.VechileDoesntExist());
                    }
                }
                catch (ValueOutOfRangeException exc)
                {
                    Console.WriteLine(exc.Message);
                }
                catch (ArgumentException exc)
                {
                    Console.WriteLine(exc.Message);
                }

                Console.WriteLine(OutputMessages.PauseForDisplayMsg());
                Console.ReadLine();
            }
        }

        // Return false if the car already exist, And true if the vehicle inserted successfully
        private bool insertVehicleToTheGarage()
        {
            Console.Clear();
            
            eVehicleType desiredType = InputGetter.GetEnumChooise<eVehicleType>();
            string licensePlate = (string)InputGetter.GetInputByTypeOnFly(typeof(string), OutputMessages.AskLicensePlateMsg());
            bool isCarExistAlready = r_GarageManger.IsCarExistInGarage(licensePlate);

            if (isCarExistAlready == false)
            {
                Tuple<Type,string>[] extraParamsToGet = CreateObjectForInsertion.CreateNewVehicle(desiredType); // Get an array with unique parameters that the instance need.
                string ownerName = (string)InputGetter.GetInputByTypeOnFly(typeof(string), OutputMessages.AskOwnerNameMsg());
                string ownerPhone = (string)InputGetter.GetInputByTypeOnFly(typeof(string), OutputMessages.AskOwnerPhoneMsg());
                string model = (string)InputGetter.GetInputByTypeOnFly(typeof(string), OutputMessages.AskModelMsg());
                float energyLeft = (float)InputGetter.GetInputByTypeOnFly(typeof(float), OutputMessages.AskEnergyLeftMsg());
                string wheelManufacturer = (string)InputGetter.GetInputByTypeOnFly(typeof(string), OutputMessages.AskWheelManufacturerMsg());
                int currentAirPressure = (int)InputGetter.GetInputByTypeOnFly(typeof(int), OutputMessages.AskCurrentAirPressureMsg());

                // Get parameters from user on fly according to the array given by the Logic. 
                object[] extraParams = new object[extraParamsToGet.Length];
                for (int i = 0; i < extraParamsToGet.Length; i++)
                {
                    extraParams[i] = InputGetter.GetInputByTypeOnFly(extraParamsToGet[i].Item1, extraParamsToGet[i].Item2);
                }

                r_GarageManger.InsertVehicle(CreateObjectForInsertion.SetVehicleData(ownerName,ownerPhone,licensePlate,model,energyLeft,wheelManufacturer,currentAirPressure,extraParams));
            }
            else
            {
                r_GarageManger.ChangeVehicleStatus(licensePlate, eVehicleStatus.InRepair); // Vehicle already exist in the garage
            }

            return !isCarExistAlready; 
        }

        // Print license according to user desired filter status
        private void printLicenseNumbers()
        { 
            Console.Clear();
            string dataToPrint;
            InputGetter.eYesNo printByFilter = (InputGetter.eYesNo)InputGetter.GetInputByTypeOnFly(typeof(InputGetter.eYesNo),"if you would like to filter by status");

            if (printByFilter == InputGetter.eYesNo.Yes)
            {
                eVehicleStatus selectedStatus = (eVehicleStatus)InputGetter.GetInputByTypeOnFly(typeof(eVehicleStatus), OutputMessages.AskStatusToPrintMsg());
                dataToPrint = r_GarageManger.GetLicenseNumbersByFilter(selectedStatus);
            }
            else
            {
                dataToPrint = r_GarageManger.GetAllLicenseNumbers();
            }

            Console.Clear();
            Console.WriteLine(dataToPrint);
        }

        // Update vehicle status. Return false in case that the vehicle doesn't exist in the garage.
        private bool changingVehicleStatus()
        {
            string licensePlate = (string)InputGetter.GetInputByTypeOnFly(typeof(string), OutputMessages.AskLicenseNumberMsg());
            eVehicleStatus selectedStatus = InputGetter.GetEnumChooise<eVehicleStatus>();

            return r_GarageManger.ChangeVehicleStatus(licensePlate, selectedStatus);
        }

        // Update Wheels air pressure. Return false in case that the vehicle doesn't exist in the garage.
        private bool inflateWheels()
        {
            string licensePlate = (string)InputGetter.GetInputByTypeOnFly(typeof(string),OutputMessages.AskLicenseNumberMsg());
            int airToAdd = (int)InputGetter.GetInputByTypeOnFly(typeof(int), "air pressure to inflate");

            return r_GarageManger.InflateWheels(licensePlate, airToAdd);
        }

        // Update fuel. Return false in case that the vehicle doesn't exist in the garage.
        private bool carRefueling()
        {
            string licensePlate = (string)InputGetter.GetInputByTypeOnFly(typeof(string), OutputMessages.AskLicenseNumberMsg());
            eFuelType selectedFuel = InputGetter.GetEnumChooise<eFuelType>();
            float amount = (float)InputGetter.GetInputByTypeOnFly(typeof(float), OutputMessages.AskAmountOfFuelMsg());

            return r_GarageManger.RefuleCar(licensePlate, (eFuelType)selectedFuel, amount);
        }

        // Update energy. Return false in case that the vehicle doesn't exist in the garage.
        private bool chargingElectricVehicle()
        {
            string licensePlate = (string)InputGetter.GetInputByTypeOnFly(typeof(string), OutputMessages.AskLicenseNumberMsg());
            float amount = (float)InputGetter.GetInputByTypeOnFly(typeof(float), OutputMessages.AskChargingMsg());

            return r_GarageManger.ChargeElectricVehicle(licensePlate, amount);
        }

        // Print all vehicles that exist in the garage.
        private void printAllVehicleDataInTheGarage()
        {
            Console.WriteLine(r_GarageManger.GetAllVehicleDataString());
        }
    }
}
