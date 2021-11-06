namespace Ex3.GarageUI
{
    using System;
    using GarageLogic;

    public class GarageInsertVehicleUI
    {
        private const string k_LicenseNumberMessage = "License Number";
        private const string k_ModelMessage = "Model";
        private const string k_OwnerNameMessage = "Owner's name";
        private const string k_OwnerPhoneNumberMessage = "Owner's phone number";
        private const string k_WheelsManufacturerMessage = "Wheel's manufacturer";
        private const string k_FloatFormatExceptionMessage = "Invalid amount entered";
        private const string k_InvalidNumberMessage = "Amount entered cannot be larger than the maximum amount or negative, Please try again:";
        private const string k_FuelMessage = "fuel in engine (Liters)";
        private const string k_BatteryEnergyMessage = "hours in battery (Hours)";
        private const string k_WheelsPressureMessage = "pressure in wheels (PSI)";
        private const string k_VolumeOfCargoMessage = "volume in truck's cargo";
        private const string k_MaxCargoValumeMessage = "Max volume in truck's cargo";
        private readonly VehicleGenerator r_VehicleGenerator;
        private readonly VehicleGarageManager r_GarageManager;

        public GarageInsertVehicleUI(VehicleGenerator i_VehicleGenerator, VehicleGarageManager i_GarageManager)
        {
            r_VehicleGenerator = i_VehicleGenerator;
            r_GarageManager = i_GarageManager;
        }

        public void InsertNewVehicle()
        {
            Console.WriteLine(string.Format("Vehicle add menu:{0}", Environment.NewLine));

            string licenseNumber = getNonEmptyStringFromInput(k_LicenseNumberMessage);

            if (r_GarageManager.DoesVehicleExist(licenseNumber))
            {
                Console.WriteLine("Vehicle already in garage, Changing status to \"In Repair\"");
                r_GarageManager.SetVehicleStatus(licenseNumber, eVehicleRepairStatus.InRepair);
            }
            else
            {
                string ownerName = getNonEmptyStringFromInput(k_OwnerNameMessage);

                string ownersPhoneNumber = getNonEmptyStringFromInput(k_OwnerPhoneNumberMessage);

                string vehicleModel = getNonEmptyStringFromInput(k_ModelMessage);

                eVehicleType vehicleType = getVehicleTypeFromInput();

                Vehicle newVehicle = null;

                eEngineType engineType = eEngineType.Fuel;
                string energyNameString = k_FuelMessage;

                switch (vehicleType)
                {
                    case eVehicleType.Car:
                        {
                            engineType = getEngineTypeFromInput();
                            energyNameString = getEnergySourceNameByEngineType(engineType);

                            newVehicle = r_VehicleGenerator.CreateVehicle(vehicleType, engineType, licenseNumber, vehicleModel);

                            eCarColor carColor = getCarColorFromInput();
                            int numberOfDoors = getNumberOfDoorsInCarFromInput();

                            r_VehicleGenerator.SetCarSpecificAttributes(newVehicle as Car, carColor, numberOfDoors);

                            break;
                        }

                    case eVehicleType.Motorcycle:
                        {
                            engineType = getEngineTypeFromInput();
                            energyNameString = getEnergySourceNameByEngineType(engineType);
                            newVehicle = r_VehicleGenerator.CreateVehicle(vehicleType, engineType, licenseNumber, vehicleModel);
                            eLicenseType licenseType = getLicenseTypeFromInput();
                            int engineVolume = getMotorcycleEngineVolume();
                            r_VehicleGenerator.SetMotorcycleSpecificAttributes(newVehicle as Motorcycle, licenseType, engineVolume);

                            break;
                        }

                    case eVehicleType.Truck:
                        {
                            bool isCarryingDangerousMaterials = doesContainsDangerousMaterials();
                            float maxCargoValume = getNonNegativeFloatFromInput(k_MaxCargoValumeMessage);
                            float volumeOfCargo = getNonNegativeFloatFromInput(k_VolumeOfCargoMessage);

                            newVehicle = r_VehicleGenerator.CreateVehicle(vehicleType, engineType, licenseNumber, vehicleModel);

                            r_VehicleGenerator.SetTruckSpecificAttributes(newVehicle as Truck, isCarryingDangerousMaterials, maxCargoValume, volumeOfCargo);

                            break;
                        }

                    default:
                        {
                            break;
                        }
                }

                float currentAmountOfEnergy = getNonNegativeFloatFromInput(energyNameString, newVehicle.GetMaxEngineVolume());

                string wheelsManufacturer = getNonEmptyStringFromInput(k_WheelsManufacturerMessage);

                float currentWheelsAirPressure = getNonNegativeFloatFromInput(k_WheelsPressureMessage, newVehicle.GetMaxWheelsAirPressure());

                try
                {
                    r_VehicleGenerator.SetVehicleAttributes(newVehicle,
                        vehicleType,
                        currentAmountOfEnergy,
                        wheelsManufacturer,
                        currentWheelsAirPressure);

                    r_GarageManager.AddVehicle(ownerName, ownersPhoneNumber, newVehicle);
                    Console.WriteLine(string.Format("New vehicle with license number: {0} was added to the garage", licenseNumber));
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private string getNonEmptyStringFromInput(string i_StringName)
        {
            Console.WriteLine(string.Format("Please enter the {0} (not empty)", i_StringName));
            string stringInput = Console.ReadLine();

            while (string.IsNullOrEmpty(stringInput))
            {
                Console.WriteLine(string.Format("{0} cannot be empty, Please try again:", i_StringName));
                stringInput = Console.ReadLine();
            }

            return stringInput;
        }

        private eVehicleType getVehicleTypeFromInput()
        {
            eVehicleType vehicleType;

            Console.WriteLine(string.Format(@"Please choose one of the options:
1. Car
2. Motorcycle
3. Truck"));

            string vehicleTypeInput = Console.ReadLine();

            while (!Enum.TryParse(vehicleTypeInput, out vehicleType) ||
                   !Enum.IsDefined(typeof(eVehicleType), vehicleType))
            {
                Console.WriteLine("Invalid vehicle type entered, Please try again:");
                vehicleTypeInput = Console.ReadLine();
            }

            return vehicleType;
        }

        private eEngineType getEngineTypeFromInput()
        {
            eEngineType engineType;

            Console.WriteLine(string.Format(@"Please enter the type of engine out of the options:
1. Fuel
2. Electric"));

            string engineTypeInput = Console.ReadLine();

            while (!Enum.TryParse(engineTypeInput, out engineType) ||
                   !Enum.IsDefined(typeof(eEngineType), engineType))
            {
                Console.WriteLine("Invalid engine type entered, Please try again:");
                engineTypeInput = Console.ReadLine();
            }

            return engineType;
        }

        private string getEnergySourceNameByEngineType(eEngineType i_EngineType)
        {
            string energySourceName = string.Empty;

            switch (i_EngineType)
            {
                case eEngineType.Fuel:
                    {
                        energySourceName = k_FuelMessage;
                        break;
                    }

                case eEngineType.Electric:
                    {
                        energySourceName = k_BatteryEnergyMessage;
                        break;
                    }

                default:
                    {
                        break;
                    }
            }

            return energySourceName;
        }

        private eCarColor getCarColorFromInput()
        {
            eCarColor carColor;

            Console.WriteLine(string.Format(@"Please enter the color of the car form the below options:
1. Red
2. Silver
3. White
4. Black"));

            string carColorInput = Console.ReadLine();

            while (!Enum.TryParse(carColorInput, out carColor) ||
                   !Enum.IsDefined(typeof(eCarColor), carColor))
            {
                Console.WriteLine("Invalid car color entered entered, Please try again:");
                carColorInput = Console.ReadLine();
            }

            return carColor;
        }

        private int getNumberOfDoorsInCarFromInput()
        {
            eNumberOfDoors numberOfDoors;

            Console.WriteLine(string.Format(@"Please enter the number of doors in the car form the below options:
2
3
4
5"));

            string numberOfDoorsInput = Console.ReadLine();

            while (!Enum.TryParse(numberOfDoorsInput, out numberOfDoors) ||
                   !Enum.IsDefined(typeof(eNumberOfDoors), numberOfDoors))
            {
                Console.WriteLine("Invalid number of doors entered entered entered, Please try again:");
                numberOfDoorsInput = Console.ReadLine();
            }

            return (int)numberOfDoors;
        }

        private eLicenseType getLicenseTypeFromInput()
        {
            eLicenseType licenseType;

            Console.WriteLine(string.Format(@"Please enter the license of the motorcycle form the below options:
1. A
2. B1
3. AA
4. BB"));

            string licenseTypeInput = Console.ReadLine();

            while (!Enum.TryParse(licenseTypeInput, out licenseType) ||
                   !Enum.IsDefined(typeof(eLicenseType), licenseType))
            {
                Console.WriteLine("Invalid license type entered entered, Please try again:");
                licenseTypeInput = Console.ReadLine();
            }

            return licenseType;
        }

        private int getMotorcycleEngineVolume()
        {
            Console.WriteLine("Please enter the motorcycle's engine volume (No negative):");
            string engineVolumeInput = Console.ReadLine();
            int engineVolume;

            try
            {
                while (!int.TryParse(engineVolumeInput, out engineVolume) || isFloatOrIntInRange(engineVolume, 0, int.MaxValue))
                {
                    if (!int.TryParse(engineVolumeInput, out engineVolume))
                    {
                        throw new FormatException("Invalid engine volume entered");
                    }
                    else
                    {
                        Console.WriteLine("Invalid engine volume entered, Please try again:");
                        engineVolumeInput = Console.ReadLine();
                    }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                engineVolume = getMotorcycleEngineVolume();
            }

            return engineVolume;
        }

        private bool doesContainsDangerousMaterials()
        {
            Console.WriteLine(string.Format(@"Does the truck contains dangerous materials?
1. Yes
2. No"));
            string doesContainsDangerousMaterialsInput = Console.ReadLine();

            while (doesContainsDangerousMaterialsInput != "1" && doesContainsDangerousMaterialsInput != "2")
            {
                Console.WriteLine(string.Format(@"Invalid input, Please pick one of the options below:
1. Yes
2. No"));
                doesContainsDangerousMaterialsInput = Console.ReadLine();
            }

            return doesContainsDangerousMaterialsInput == "1";
        }

        private float getNonNegativeFloatFromInput(string i_NameOfObjectString, float i_MaxValue = 0)
        {
            string maxValueString = string.Empty;

            if (i_MaxValue > 0)
            {
                maxValueString = string.Format(" out of a maximum amount of {0}", i_MaxValue);
            }
            else
            {
                i_MaxValue = float.MaxValue;
            }

            Console.WriteLine(string.Format("Please enter the amount of {0}{1} (Non Negative):", i_NameOfObjectString, maxValueString));
            string floatInput = Console.ReadLine();
            float floatToReturn;

            try
            {
                while (!float.TryParse(floatInput, out floatToReturn) || !isFloatOrIntInRange(floatToReturn, 0, i_MaxValue))
                {
                    if (!float.TryParse(floatInput, out floatToReturn))
                    {
                        throw new FormatException(k_FloatFormatExceptionMessage);
                    }
                    else
                    {
                        Console.WriteLine(k_InvalidNumberMessage);
                        floatInput = Console.ReadLine();
                    }
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                floatToReturn = getNonNegativeFloatFromInput(i_NameOfObjectString, i_MaxValue);
            }

            return floatToReturn;
        }

        private bool isFloatOrIntInRange(float i_Number, float i_MinInRange, float i_MaxInRange)
        {
            return i_Number >= i_MinInRange && i_Number <= i_MaxInRange;
        }
    }
}
