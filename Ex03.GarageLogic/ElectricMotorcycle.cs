using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class ElectricMotorcycle : Motorcycle, IElectricEngine
    {
        // **Const**
        private const float k_MaximumEnergyCapacity = (float)1.8;
        private const eFuelType k_FuelType = eFuelType.Electric;

        // **Properties**
        float IElectricEngine.GetRemainingBatteryTime()
        {
            return PercentageOfEnergyRemaining;
        }

        float IElectricEngine.GetMaximumBatteryTime()
        {
            return k_MaximumEnergyCapacity;
        }

        // **Methods**
        internal ElectricMotorcycle()
        {
            m_FuelType = k_FuelType;
            m_MaxEnergyCapacity = k_MaximumEnergyCapacity;
        }

        void IElectricEngine.BatteryCharging(float i_litersToAdd)
        {
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

        internal override float MaximumEnergyCapacity()
        {
            return k_MaximumEnergyCapacity;
        }
    }
}
