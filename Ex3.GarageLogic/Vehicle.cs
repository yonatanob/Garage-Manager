namespace GarageLogic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class Vehicle
    {
        private readonly string r_Model;
        private readonly string r_LicenseNumber;
        private readonly List<Wheel> r_Wheels;
        private VehicleRepairInfo m_RepairInfo;
        private eVehicleType m_VehicleType;
        private float m_PercentOfEnergyLeftInEngine;
        private Engine m_Engine;

        public abstract void InitEngine(float i_CurrentAmountOfEnergy);

        public abstract float GetMaxWheelsAirPressure();

        public abstract float GetMaxEngineVolume();

        public VehicleRepairInfo RepairInfo
        {
            get
            {
                return m_RepairInfo;
            }

            set
            {
                m_RepairInfo = value;
            }
        }

        public eVehicleType VehicleType
        {
            get
            {
                return m_VehicleType;
            }

            set
            {
                m_VehicleType = value;
            }
        }

        public string Model
        {
            get
            {
                return r_Model;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return r_LicenseNumber;
            }
        }

        public float PercentOfEnergyLeftInEngine
        {
            get
            {
                return m_PercentOfEnergyLeftInEngine;
            }
        }

        public float GetCurrentAirPressureInWheels()
        {
            if (r_Wheels.Count > 0 && r_Wheels[0] != null)
            {
                return r_Wheels[0].CurrentAirPressure;
            }
            else
            {
                throw new NullReferenceException("Vehicle has no wheels");
            }
        }

        public void InflateWheelsWithAirToMax()
        {
            if (r_Wheels.Count > 0)
            {
                foreach (Wheel wheel in r_Wheels)
                {
                    if (wheel != null)
                    {
                        wheel.InflateWheelWithAir(wheel.MaxAirPressure - wheel.CurrentAirPressure);
                    }
                    else
                    {
                        throw new NullReferenceException("Vehicle missing wheels");
                    }
                }
            }
            else
            {
                throw new NullReferenceException("Vehicle has no wheels");
            }
        }

        public abstract void InitWheels(string i_Manufacturer, float i_CurrentAirPressure);

        public void Refuel(eFuelType i_FuelType, float i_Liters)
        {
            m_Engine.Refuel(i_FuelType, i_Liters);
            updateEnergyLeftPercent();
        }

        public void RechargeBattery(float i_Hours)
        {
            m_Engine.RechargeBattery(i_Hours);
            updateEnergyLeftPercent();
        }

        public override string ToString()
        {
            StringBuilder infoStringBuilder = new StringBuilder();
            infoStringBuilder.Append(string.Format("License Number : {0}{1}", LicenseNumber, Environment.NewLine));
            infoStringBuilder.Append(string.Format("Model : {0}{1}", Model, Environment.NewLine));
            infoStringBuilder.Append(string.Format("Owner Name : {0}{1}", RepairInfo.OwnerName, Environment.NewLine));
            infoStringBuilder.Append(string.Format("Owner Phone Number : {0}{1}", RepairInfo.OwnerPhoneNumber, Environment.NewLine));
            infoStringBuilder.Append(string.Format("Vehicle Type: {0}{1}", m_VehicleType, Environment.NewLine));
            infoStringBuilder.Append(string.Format("Status : {0}{1}", RepairInfo.RepairStatus, Environment.NewLine));
            infoStringBuilder.Append(getWheelsInfoString());
            infoStringBuilder.Append(m_Engine.ToString());
            infoStringBuilder.Append(string.Format("Engine {0} Percentage: {1}%{2}", m_Engine.EngineType, m_PercentOfEnergyLeftInEngine, Environment.NewLine));
            
            return infoStringBuilder.ToString();

        }

        protected Vehicle(string i_Model, string i_LicenseNumber)
        {
            r_Model = i_Model;
            r_LicenseNumber = i_LicenseNumber;
            r_Wheels = new List<Wheel>();
        }

        protected void CreateAndSetWheels(string i_Manufacturer, float i_CurrentAirPressure, float i_MaxAirPressure, int i_NumberOfWheels)
        {
            for (int i = 0; i < i_NumberOfWheels; i++)
            {
                r_Wheels.Add(new Wheel(i_Manufacturer, i_MaxAirPressure)
                {
                    CurrentAirPressure = i_CurrentAirPressure
                });
            }
        }

        protected void CreateAndSetEngine(eEngineType i_EngineType, float i_CurrentAmountOfEnergy, float i_MaxAmountOfEnergy)
        {
            m_Engine = new Engine(i_EngineType, i_MaxAmountOfEnergy)
            {
                CurrentAmountOfEnergy = i_CurrentAmountOfEnergy
            };

            updateEnergyLeftPercent();
        }

        protected void SetFuelType(eFuelType i_FuelType)
        {
            if (m_Engine != null)
            {
                m_Engine.FuelType = i_FuelType;
            }
            else
            {
                throw new NullReferenceException("Vehicle has no engine so a fuel type cannot be set");

            }
        }
        private void updateEnergyLeftPercent()
        {
            m_PercentOfEnergyLeftInEngine = (m_Engine.CurrentAmountOfEnergy / m_Engine.MaxAmountOfEnergy) * 100;
        }

        private string getWheelsInfoString()
        {
            if (r_Wheels.Count > 0 && r_Wheels[0] != null)
            {
                return r_Wheels[0].ToString();
            }
            else
            {
                throw new NullReferenceException("Vehicle is missing wheels");
            }
        }
    }
}