namespace GarageLogic
{
    using System;
    using System.Text;

    public enum eLicenseType
    {
        A = 1,
        B1 = 2,
        AA = 3,
        BB = 4
    }

    public abstract class Motorcycle : Vehicle
    {
        private const int k_NumberOfWheels = 2;
        private const float k_MaxWheelAirPressure = 30f;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;

        protected Motorcycle(string i_Model, string i_LicenseNumber)
    : base(i_Model, i_LicenseNumber)
        {
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }

            set
            {
                m_LicenseType = value;
            }
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolume;
            }

            set
            {
                m_EngineVolume = value;
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
            infoStringBuilder.Append(string.Format("License Type : {0}{1}", m_LicenseType, Environment.NewLine));
            infoStringBuilder.Append(string.Format("Engine Volume : {0}{1}", m_EngineVolume, Environment.NewLine));
            return infoStringBuilder.ToString();
        }
    }
}
