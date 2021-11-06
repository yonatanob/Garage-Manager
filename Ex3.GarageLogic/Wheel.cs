namespace GarageLogic
{
    using System;
    using System.Text;

    public class Wheel
    {
        private readonly string r_Manufacturer;
        private readonly float r_MaxAirPressure;
        private float m_CurrentAirPressure;

        public Wheel(string i_Manufacturer, float i_MaxAirPressure)
        {
            r_Manufacturer = i_Manufacturer;
            r_MaxAirPressure = i_MaxAirPressure;
        }

        public string Manufacturer
        {
            get
            {
                return r_Manufacturer;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                if (value <= r_MaxAirPressure && value >= 0)
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, r_MaxAirPressure);
                }
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return r_MaxAirPressure;
            }
        }

        public void InflateWheelWithAir(float i_Amount)
        {
            if (m_CurrentAirPressure + i_Amount <= r_MaxAirPressure && m_CurrentAirPressure + i_Amount >= 0)
            {
                m_CurrentAirPressure += i_Amount;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaxAirPressure);
            }
        }

        public override string ToString()
        {
            StringBuilder wheelInfoStringBuilder = new StringBuilder();
            wheelInfoStringBuilder.Append(string.Format("Wheel manufacturer: {0}{1}", r_Manufacturer, Environment.NewLine));
            wheelInfoStringBuilder.Append(string.Format("Wheel current air pressure (PSI): {0}/{1}{2}", m_CurrentAirPressure, r_MaxAirPressure, Environment.NewLine));
            return wheelInfoStringBuilder.ToString();
        }
    }
}