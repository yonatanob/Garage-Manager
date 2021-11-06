namespace GarageLogic
{
    public class FuelBasedTruck : Truck
    {
        private const eFuelType k_FuelType = eFuelType.Soler;
        private const float k_EngineVolumeInLiters = 120f;

        public FuelBasedTruck(string i_Model, string i_LicenseNumber)
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
