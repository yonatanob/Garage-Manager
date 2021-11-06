namespace Ex3.GarageUI
{
    using System;
    using System.Collections.Generic;
    using GarageLogic;

    public class GarageChangeVehicleUI
    {
        private enum eVehicleChangeOptions
        {
            ChangeStatus = 1,
            InflateTires = 2,
            Refuel = 3,
            Recharge = 4,
            MainMenu = 5
        }

        private const string k_FuelMessage = "fuel (Liters)";
        private const string k_BatteryEnergyMessage = "hours";
        private readonly VehicleGarageManager r_GarageManager;
        private readonly GarageInfoVehicleUI r_InfoVehicleUi;

        public GarageChangeVehicleUI(VehicleGarageManager i_GarageManager, GarageInfoVehicleUI i_InfoVehicleUi)
        {
            r_GarageManager = i_GarageManager;
            r_InfoVehicleUi = i_InfoVehicleUi;
        }

        public void ChangeVehicleMenu()
        {
            bool inChangeMenu = true;
            string licenseNumber;

            while (inChangeMenu)
            {
                Console.WriteLine(string.Format("Change menu:{0}", Environment.NewLine));

                eVehicleChangeOptions changeOptions = getEngineChangeOption();

                switch (changeOptions)
                {
                    case eVehicleChangeOptions.ChangeStatus:
                        {
                            try
                            {
                                licenseNumber = r_InfoVehicleUi.GetVehicleLicenseNumberFromInput();
                                eVehicleRepairStatus repairStatus = r_InfoVehicleUi.GetVehicleRepairStatusFromInput();
                                r_GarageManager.SetVehicleStatus(licenseNumber, repairStatus);
                            }
                            catch (ArgumentNullException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (KeyNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;
                        }

                    case eVehicleChangeOptions.InflateTires:
                        {
                            try
                            {
                                licenseNumber = r_InfoVehicleUi.GetVehicleLicenseNumberFromInput();
                                r_GarageManager.InflateWheelsWithAirToMax(licenseNumber);
                            }
                            catch (ArgumentNullException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (ValueOutOfRangeException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (KeyNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;
                        }

                    case eVehicleChangeOptions.Refuel:
                        {
                            try
                            {
                                licenseNumber = r_InfoVehicleUi.GetVehicleLicenseNumberFromInput();
                                eFuelType fuelType = getFuelTypeFromInput();

                                float currentFillPercent = r_GarageManager.GetEnergyPercentInVehicle(licenseNumber);
                                float maxFillAmount = r_GarageManager.GetMaximumAmountOfEnergyInVehicle(licenseNumber);

                                float amountOfEnergyToFill = getAmountEnergyToAddFromInput(k_FuelMessage, currentFillPercent, maxFillAmount);
                                r_GarageManager.Refuel(licenseNumber, fuelType, amountOfEnergyToFill);
                            }
                            catch (ArgumentNullException ex)
                            {
                                Console.WriteLine(ex.Message);
                                throw;
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (ValueOutOfRangeException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (KeyNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;
                        }

                    case eVehicleChangeOptions.Recharge:
                        {
                            try
                            {
                                licenseNumber = r_InfoVehicleUi.GetVehicleLicenseNumberFromInput();
                                float currentFillPercent = r_GarageManager.GetEnergyPercentInVehicle(licenseNumber);
                                float maxFillAmount = r_GarageManager.GetMaximumAmountOfEnergyInVehicle(licenseNumber);

                                float amountOfEnergyToFill = getAmountEnergyToAddFromInput(k_BatteryEnergyMessage, currentFillPercent, maxFillAmount);
                                r_GarageManager.RechargeBattery(licenseNumber, amountOfEnergyToFill / 60f);
                            }
                            catch (ArgumentNullException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (KeyNotFoundException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (ValueOutOfRangeException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }

                            break;
                        }

                    case eVehicleChangeOptions.MainMenu:
                        {
                            inChangeMenu = false;
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }
            }
        }

        private eVehicleChangeOptions getEngineChangeOption()
        {
            eVehicleChangeOptions changeOption;

            Console.WriteLine(string.Format(@"Please choose one of the below options:
1. Change a vehicle's status
2. Inflate a vehicle's tires
3. Refuel a vehicle's engine
4. Recharge a vehicle's electric engine
5. Return to main menu"));

            string optionInput = Console.ReadLine();

            while (!Enum.TryParse(optionInput, out changeOption) ||
                   !Enum.IsDefined(typeof(eVehicleChangeOptions), changeOption))
            {
                Console.WriteLine(string.Format(@"Invalid option entered, Please try again:
1. Change a vehicle's status
2. Inflate a vehicle's tires
3. Refuel a vehicle's engine
4. Recharge a vehicle's electric engine
5. Return to main menu"));

                optionInput = Console.ReadLine();
            }

            return changeOption;
        }

        private eFuelType getFuelTypeFromInput()
        {
            eFuelType fuelType;

            Console.WriteLine(string.Format(@"Please choose one of the below fuel types:
1. Soler
2. Octane95
3. Octane96
4. Octane98"));

            string fuelTypeInput = Console.ReadLine();

            while (!Enum.TryParse(fuelTypeInput, out fuelType) ||
                   !Enum.IsDefined(typeof(eFuelType), fuelType))
            {
                Console.WriteLine(string.Format(@"Invalid option fuel type, Please try again:
1. Soler
2. Octane95
3. Octane96
4. Octane98"));

                fuelTypeInput = Console.ReadLine();
            }

            return fuelType;
        }

        private float getAmountEnergyToAddFromInput(string i_ObjectToAddName, float i_CurrentFillPercent, float i_MaxAmount)
        {
            Console.WriteLine(string.Format("Current {0} fill percent: {1}%, Max {0} amount: {2}", i_ObjectToAddName, i_CurrentFillPercent, i_MaxAmount));

            if (i_ObjectToAddName == k_BatteryEnergyMessage)
            {
                i_ObjectToAddName = "minutes";
            }

            Console.WriteLine(string.Format("Please enter the amount of {0} to add (Non negative):", i_ObjectToAddName));

            string amountInput = Console.ReadLine();
            float amountToAdd;

            try
            {
                if (!float.TryParse(amountInput, out amountToAdd))
                {
                    throw new FormatException(string.Format("Invalid amount of {0} entered, Please try again:", i_ObjectToAddName));
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
                amountToAdd = getAmountEnergyToAddFromInput(i_ObjectToAddName, i_CurrentFillPercent, i_MaxAmount);
            }

            return amountToAdd;
        }
    }
}