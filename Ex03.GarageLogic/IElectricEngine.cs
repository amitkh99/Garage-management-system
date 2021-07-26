using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal interface IElectricEngine
    {
        float GetRemainingBatteryTime(); ////A method that will be implemented in all vehicles with an electric engine and returns the remaining battery time

        float GetMaximumBatteryTime(); ////A method that will be implemented in all vehicles with an electric engine and returns the maximum battery charge

        void BatteryCharging(float i_litersToAdd); ////A method that will be implemented in all vehicles with an electric engine and charges the battery in the amount of time the method has received
    }
}
