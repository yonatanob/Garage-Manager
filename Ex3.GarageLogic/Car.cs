namespace GarageLogic
{
    using System;
    using System.Text;

    public enum eCarColor
    {
        Red = 1,
        Silver = 2,
        White = 3,
        Black = 4
    }

    public enum eNumberOfDoors
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5
    }

    public abstract class Car : Vehicle
    {
        private const int k_NumberOfWheels = 4;
        private const float k_MaxWheelAirPressure = 32f;
        private const int k_MinNumberOfDoors = 2;
        private const int k_MaxNumberOfDoors = 5;
        private eCarColor m_CarColor;
        private int m_NumberOfDoors;

        protected Car(string i_Model, string i_LicenseNumber)
    : base(i_Model, i_LicenseNumber)
        {
        }

        public eCarColor CarColor
        {
            get
            {
                return m_CarColor;
            }

            set
            {
                m_CarColor = value;
            }
        }

        public int NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }

            set
            {
                if (value >= k_MinNumberOfDoors && value <= k_MaxNumberOfDoors)
                {
                    m_NumberOfDoors = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(k_MinNumberOfDoors, k_MaxNumberOfDoors);
                }
            }
        }

        public override void InitWheels(string i_Manufacturer, float i_CurrentAirPressure)
        {
            CreateAndSetWheels(i_Manufacturer, i_CurrentAirPressure, k_MaxWheelAirPressure, k_NumberOfWheels);
        }

        public override float GetMaxWheelsAirPressure()
        {
            return k_MaxWheelAirPressure;
        }

        public override string ToString()
        {
            StringBuilder infoStringBuilder = new StringBuilder(base.ToString());
            infoStringBuilder.Append(string.Format("Car Color : {0}{1}", m_CarColor, Environment.NewLine));
            infoStringBuilder.Append(string.Format("Number Of Doors : {0}{1}", m_NumberOfDoors, Environment.NewLine));
            return infoStringBuilder.ToString();
        }
    }
}
