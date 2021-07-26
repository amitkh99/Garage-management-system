using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Wheel 
    {
        ////**Fields**
        private string m_Manufacturer;
        private float m_CurrentAirPressure;
        private float m_MaximumAirPressure;

        //// **Properties**
        internal float MaximumAirPressure
        {
            get
            {
                return m_MaximumAirPressure;
            }
        }

        internal float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        internal string Manufacturer
        {
            get
            {
                return m_Manufacturer;
            }

            set
            {
                m_Manufacturer = value;
            }
        }

        internal Wheel(float i_MaximumAirPressure)
        {
            m_MaximumAirPressure = i_MaximumAirPressure;
        }

        //// **Methods**
        internal void Inflation(float i_AirPressureToAdd)
        {
            if (i_AirPressureToAdd <= GetMissingAirPressure()) 
            {
                m_CurrentAirPressure += i_AirPressureToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException("To much air pressure.", GetMissingAirPressure(),0);
            }
        }

        internal float GetMissingAirPressure() ////Returns the maximum amount of air that can be inflated on the wheel
        {
            return MaximumAirPressure - CurrentAirPressure;
        }
    }
}
