namespace GarageLogic
{
    using System;
    using System.Text;

    public abstract class Truck : Vehicle
    {
        private const int k_NumberOfWheels = 16;
        private const float k_MaxWheelAirPressure = 26f;
        private bool m_ContainsDangerousMaterials;
        private float m_VolumeOfCargo;
        private float m_MaxCargoValume;

        protected Truck(string i_Model, string i_LicenseNumber)
            : base(i_Model, i_LicenseNumber)
        {
        }

        public bool ContainsDangerousMaterials
        {
            get
            {
                return m_ContainsDangerousMaterials;
            }

            set
            {
                m_ContainsDangerousMaterials = true;
            }
        }

        public float VolumeOfCargo
        {
            get
            {
                return m_VolumeOfCargo;
            }

            set
            {
                if (value <= m_MaxCargoValume && value >= 0)
                {
                    m_VolumeOfCargo = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, m_MaxCargoValume);
                }
            }
        }

        public float MaxCargoValume
        {
            get
            {
                return m_MaxCargoValume;
            }

            set
            {
                if (value >= 0)
                {
                    m_MaxCargoValume = value;
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
            infoStringBuilder.Append(string.Format("Contain Dangerous Materials : {0}{1}", m_ContainsDangerousMaterials, Environment.NewLine));
            infoStringBuilder.Append(string.Format("Volume of Cargo : {0}/{1}{2} in KG", m_VolumeOfCargo, m_MaxCargoValume, Environment.NewLine));
            return infoStringBuilder.ToString();
        }
    }
}
