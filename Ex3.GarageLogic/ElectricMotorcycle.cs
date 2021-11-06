namespace GarageLogic
{
    public class ElectricMotorcycle : Motorcycle
    {
        private const float k_MaxBatteryLife = 1.8f;

        public ElectricMotorcycle(string i_Model, string i_LicenseNumber)
            : base(i_Model, i_LicenseNumber)
        {
        }

        public override void InitEngine(float i_CurrentAmountOfEnergy)
        {
            CreateAndSetEngine(eEngineType.Electric, i_CurrentAmountOfEnergy, k_MaxBatteryLife);
        }

        public override float GetMaxEngineVolume()
        {
            return k_MaxBatteryLife;
        }
    }
}
