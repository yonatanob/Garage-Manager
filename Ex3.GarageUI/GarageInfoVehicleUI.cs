namespace Ex3.GarageUI
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using GarageLogic;

    public class GarageInfoVehicleUI
    {
        private enum eVehicleInfoOptions
        {
            GetVehicleInfo = 1,
            GetAllVehiclesLicensesWithRepairStatus = 2,
            GetAllVehiclesLicenses = 3,
            MainMenu = 4
        }

        private readonly VehicleGarageManager r_GarageManager;

        public GarageInfoVehicleUI(VehicleGarageManager i_GarageManager)
        {
            r_GarageManager = i_GarageManager;
        }

        public void VehicleInfoMenu()
        {
            Console.WriteLine(string.Format("Info menu:{0}", Environment.NewLine));

            eVehicleInfoOptions infoOption = getVehicleInfoOptionFromInput();

            switch (infoOption)
            {
                case eVehicleInfoOptions.GetVehicleInfo:
                    {
                        try
                        {
                            string licenseNumber = GetVehicleLicenseNumberFromInput();
                            Console.WriteLine(r_GarageManager.GetVehicleInfoString(licenseNumber).ToString());
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

                case eVehicleInfoOptions.GetAllVehiclesLicensesWithRepairStatus:
                    {
                        eVehicleRepairStatus repairStatus = GetVehicleRepairStatusFromInput();

                        string vehiclesWithRepairStatusString = r_GarageManager.GetVehiclesLicensesByRepairStatus(repairStatus);

                        Console.WriteLine(vehiclesWithRepairStatusString);

                        break;
                    }

                case eVehicleInfoOptions.GetAllVehiclesLicenses:
                    {
                        StringBuilder allVehiclesLicensesStringBuilder = new StringBuilder();

                        foreach (eVehicleRepairStatus repairStatus in Enum.GetValues(typeof(eVehicleRepairStatus)))
                        {
                            string vehiclesWithRepairStatusString = r_GarageManager.GetVehiclesLicensesByRepairStatus(repairStatus);
                            allVehiclesLicensesStringBuilder.Append(vehiclesWithRepairStatusString);
                        }

                        Console.WriteLine(allVehiclesLicensesStringBuilder.ToString());
                        break;
                    }

                case eVehicleInfoOptions.MainMenu:
                    {
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }

        public eVehicleRepairStatus GetVehicleRepairStatusFromInput()
        {
            eVehicleRepairStatus repairStatus;
            Console.WriteLine(string.Format(@"Please choose one of the vehicle statuses:
1. In Repair
2. Repaired
3. Paid For"));

            string repairStatusInput = Console.ReadLine();

            while (!Enum.TryParse(repairStatusInput, out repairStatus) ||
                   !Enum.IsDefined(typeof(eVehicleRepairStatus), repairStatus))
            {
                Console.WriteLine(string.Format(@"Invalid status entered, Please try again:
1. In Repair
2. Repaired
3. Paid For"));

                repairStatusInput = Console.ReadLine();
            }

            return repairStatus;
        }

        public string GetVehicleLicenseNumberFromInput()
        {
            Console.WriteLine("Please enter the vehicle's license number for identification");

            string vehicleLicenseNumberInput = Console.ReadLine();

            while (!r_GarageManager.DoesVehicleExist(vehicleLicenseNumberInput))
            {
                Console.WriteLine("The license number entered does not belong to a vehicle currently in the garage, Please try again:");
                vehicleLicenseNumberInput = Console.ReadLine();
            }

            return vehicleLicenseNumberInput;
        }

        private eVehicleInfoOptions getVehicleInfoOptionFromInput()
        {
            eVehicleInfoOptions vehicleInfoOption;

            Console.WriteLine(string.Format(@"Please enter one the options below:
1. Get information about a vehicle
2. Get all vehicle licenses with a repair status in the garage
3. Get all vehicle licenses in the garage
4. Return to main menu"));

            string optionInput = Console.ReadLine();

            while (!Enum.TryParse(optionInput, out vehicleInfoOption) ||
                   !Enum.IsDefined(typeof(eVehicleInfoOptions), vehicleInfoOption))
            {
                Console.WriteLine(string.Format(@"Invalid option entered, Please try again:
1. Get information about a vehicle
2. Get all vehicle licenses with a repair status in the garage
3. Get all vehicle licenses in the garage
4. Return to main menu"));

                optionInput = Console.ReadLine();
            }

            return vehicleInfoOption;
        }
    }
}
