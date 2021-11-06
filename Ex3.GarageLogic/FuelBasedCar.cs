namespace GarageLogic
{
    public class FuelBasedCar : Car
    {
        private const eFuelType k_FuelType = eFuelType.Octane95;
        private const float k_EngineVolumeInLiters = 45f;

        public FuelBasedCar(string i_Model, string i_LicenseNumber)
    : base(i_Model, i_LicenseNumber)
        {
        }

        public eFuelType FuelType
        {
            get
            {
                return k_FuelType;
            }
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
