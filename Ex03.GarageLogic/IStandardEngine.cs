using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal interface IStandardEngine
    {
        eFuelType GetFuelType();

        float GetCurrentAmountOfFuel(); ////A method that will be implemented in all vehicles with an regular engine and returns the current amount of fuel in the vehicle

        float GetMaximumAmountOfFuel(); ////A method that will be implemented in all vehicles with an regular engine and returns the maximum amount of fuel the vehicle can contain

        void Refueling(eFuelType i_FuelType, float i_litersToAdd); ////A method that will be implemented in all vehicles with an electric engine and adds the amount of fuel the method received to the amount of fuel currently in the vehicle
    }
}
