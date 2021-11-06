namespace GarageLogic
{
    public class FuelBasedMotorcycle : Motorcycle
    {
        private const float k_EngineVolumeInLiters = 6f;
        private const eFuelType k_FuelType = eFuelType.Octane98;

        public FuelBasedMotorcycle(string i_Model, string i_LicenseNumber)
            : base(i_Model, i_LicenseNumber)
        {
        }

        public override void InitEngine(float i_CurrentAmountOfEnergy)
        {
            CreateAndSetEngine(eEngineType.Fuel, i_CurrentAmountOfEnergy, k_EngineVolumeInLiters);
            SetFuelType(k_FuelType);
        }

        public override float GetMaxEngineVolume()
        {
            return k_EngineVolumeInLiters;
        }
    }
}
