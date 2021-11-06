namespace GarageLogic
{
    using System;
    using System.Text;

    public enum eEngineType
    {
        Fuel = 1,
        Electric = 2,
    }

    public class Engine
    {
        private readonly float r_MaxAmountOfEnergy;
        private readonly eEngineType r_EngineType;
        private float m_CurrentAmountOfEnergy;
        private eFuelType m_FuelType;

        public Engine(eEngineType i_EngineType, float i_MaxAmountOfEnergy)
        {
            r_EngineType = i_EngineType;
            r_MaxAmountOfEnergy = i_MaxAmountOfEnergy;
        }

        public float CurrentAmountOfEnergy
        {
            get
            {
                return m_CurrentAmountOfEnergy;
            }

            set
            {
                if (value >= 0 && value <= r_MaxAmountOfEnergy)
                {
                    m_CurrentAmountOfEnergy = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(0, r_MaxAmountOfEnergy);
                }
            }
        }

        public float MaxAmountOfEnergy
        {
            get
            {
                return r_MaxAmountOfEnergy;
            }
        }

        public eEngineType EngineType
        {
            get
            {
                return r_EngineType;
            }
        }

        public eFuelType FuelType
        {
            get
            {
                if (r_EngineType == eEngineType.Fuel)
                {
                    return m_FuelType;
                }
                else
                {
                    throw new ArgumentException("An electric engine has no fuel type");
                }
            }

            set
            {
                if (r_EngineType == eEngineType.Fuel)
                {
                    m_FuelType = value;
                }
                else
                {
                    throw new ArgumentException("Can't set a fuel to an electric based engine");
                }
            }
        }

        public void Refuel(eFuelType i_FuelType, float i_Liters)
        {
            if (r_EngineType != eEngineType.Fuel)
            {
                throw new ArgumentException("Can't fuel an electric based engine");
            }
            else if (m_FuelType != i_FuelType)
            {
                throw new ArgumentException(string.Format("Incompatible fuel types, expected : {0}, entered : {1}", m_FuelType, i_FuelType));
            }
            else
            {
                reEnergize(i_Liters);
            }
        }

        public void RechargeBattery(float i_Hours)
        {
            if (r_EngineType == eEngineType.Electric)
            {
                reEnergize(i_Hours);
            }
            else
            {
                throw new ArgumentException("Can't charge a fuel based engine");
            }
        }

        public override string ToString()
        {
            StringBuilder engineInfoStringBuilder = new StringBuilder();
            engineInfoStringBuilder.Append(string.Format("Engine type: {0} based{1}", r_EngineType, Environment.NewLine));

            switch (r_EngineType)
            {
                case eEngineType.Fuel:
                    {
                        engineInfoStringBuilder.Append(string.Format("Fuel Type: {0}{1}", m_FuelType, Environment.NewLine));
                        engineInfoStringBuilder.Append(string.Format("Max Fuel (Liters): {0}{1}", r_MaxAmountOfEnergy, Environment.NewLine));
                        break;
                    }

                case eEngineType.Electric:
                    {
                        engineInfoStringBuilder.Append(string.Format("Max Battery (Hours): {0}{1}", m_CurrentAmountOfEnergy, Environment.NewLine));
                        break;
                    }
            }

            return engineInfoStringBuilder.ToString();
        }

        private void reEnergize(float i_UnitAmount)
        {
            if (m_CurrentAmountOfEnergy + i_UnitAmount <= r_MaxAmountOfEnergy && i_UnitAmount >= 0)
            {
                m_CurrentAmountOfEnergy += i_UnitAmount;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaxAmountOfEnergy);
            }
        }
    }
}