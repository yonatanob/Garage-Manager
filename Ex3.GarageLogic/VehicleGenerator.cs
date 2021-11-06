namespace GarageLogic
{
    public class VehicleGenerator
    {
        public Vehicle CreateVehicle(eVehicleType i_VehicleType, eEngineType i_EngineType, string i_LicenseNumber, string i_Model)
        {
            Vehicle newVehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.Car:
                    {
                        switch (i_EngineType)
                        {
                            case eEngineType.Fuel:
                                {
                                    newVehicle = new FuelBasedCar(i_Model, i_LicenseNumber);
                                    break;
                                }

                            case eEngineType.Electric:
                                {
                                    newVehicle = new ElectricCar(i_Model, i_LicenseNumber);
                                    break;
                                }

                            default:
                                {
                                    break;
                                }
                        }

                        break;
                    }

                case eVehicleType.Motorcycle:
                    {
                        newVehicle = new ElectricCar(i_Model, i_LicenseNumber);
                        switch (i_EngineType)
                        {
                            case eEngineType.Fuel:
                                {
                                    newVehicle = new FuelBasedMotorcycle(i_Model, i_LicenseNumber);
                                    break;
                                }

                            case eEngineType.Electric:
                                {
                                    newVehicle = new ElectricMotorcycle(i_Model, i_LicenseNumber);
                                    break;
                                }

                            default:
                                {
                                    break;
                                }
                        }

                        break;
                    }

                case eVehicleType.Truck:
                    {
                        switch (i_EngineType)
                        {
                            case eEngineType.Fuel:
                                {
                                    newVehicle = new FuelBasedTruck(i_Model, i_LicenseNumber);
                                    break;
                                }

                            default:
                                {
                                    break;
                                }
                        }

                        break;
                    }

                default:
                    {
                        break;
                    }
            }

            return newVehicle;
        }

        public void SetCarSpecificAttributes(Car i_Car, eCarColor i_CarColor, int i_NumberOfDoors)
        {
            i_Car.CarColor = i_CarColor;
            i_Car.NumberOfDoors = i_NumberOfDoors;
        }

        public void SetMotorcycleSpecificAttributes(Motorcycle i_Motorcycle, eLicenseType i_LicenseType, int i_EngineVolume)
        {
            i_Motorcycle.LicenseType = i_LicenseType;
            i_Motorcycle.EngineVolume = i_EngineVolume;
        }

        public void SetTruckSpecificAttributes(Truck i_Truck, bool i_ContainsDangerousMaterials, float i_MaxCargoValume, float i_VolumeOfCargo)
        {
            i_Truck.ContainsDangerousMaterials = i_ContainsDangerousMaterials;
            i_Truck.MaxCargoValume = i_MaxCargoValume;
            i_Truck.VolumeOfCargo = i_VolumeOfCargo;
        }

        public void SetVehicleAttributes(Vehicle i_Vehicle, eVehicleType i_VehicleType, float i_CurrentAmountOfEnergy, string i_WheelsManufacturer, float i_CurrentAmountOfAirInWheels)
        {
            i_Vehicle.VehicleType = i_VehicleType;
            i_Vehicle.InitEngine(i_CurrentAmountOfEnergy);
            i_Vehicle.InitWheels(i_WheelsManufacturer, i_CurrentAmountOfAirInWheels);
        }
    }
}
