using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MaxValue;
        private float m_MinValue;

        public ValueOutOfRangeException(string i_Message, float i_MaxValue, float i_MinValue) : base(i_Message)
        {
            m_MaxValue = i_MaxValue;
            m_MinValue = i_MinValue;
        }

        public override string Message
        {
            get
            {
                return base.Message + string.Format("Range is: {0} to {1}", MinValue, MaxValue);
            }
        }

        public float MaxValue { get => m_MaxValue; }

        public float MinValue { get => m_MinValue; }
    }
}
