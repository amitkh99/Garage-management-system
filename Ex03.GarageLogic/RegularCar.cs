using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class RegularCar : Car, IStandardEngine
    {
        // **Const**
        private const float k_MaximumEnergyCapacity = (float)45;
        private const eFuelType k_FuelType = eFuelType.Octan95;

        // **Properties**
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

        internal override float MaximumEnergyCapacity()
        {
            return k_MaximumEnergyCapacity;
        }

        // **Methods**
        internal RegularCar()
        {
            m_FuelType = k_FuelType;
            m_MaxEnergyCapacity = k_MaximumEnergyCapacity;
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
