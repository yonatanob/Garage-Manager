namespace Ex3.GarageUI
{
    using System;
    using GarageLogic;

    public class GarageHandler
    {
        private readonly VehicleGenerator r_VehicleGenerator;
        private readonly VehicleGarageManager r_GarageManager;
        private readonly GarageInsertVehicleUI r_GarageInsertUI;
        private readonly GarageChangeVehicleUI r_GarageChangeVehicleUI;
        private readonly GarageInfoVehicleUI r_GarageInfoVehicleUi;

        private enum eGarageOptions
        {
            InsertVehicle = 1,
            ChangeVehicle = 2,
            GetInfo = 3
        }

        public GarageHandler()
        {
            r_VehicleGenerator = new VehicleGenerator();
            r_GarageManager = new VehicleGarageManager();

            r_GarageInsertUI = new GarageInsertVehicleUI(r_VehicleGenerator, r_GarageManager);
            r_GarageInfoVehicleUi = new GarageInfoVehicleUI(r_GarageManager);
            r_GarageChangeVehicleUI = new GarageChangeVehicleUI(r_GarageManager, r_GarageInfoVehicleUi);
        }

        public void StartGarage()
        {
            Console.WriteLine(string.Format("Welcome to Eden & Yehonatan's garage!!!{0}", Environment.NewLine));
            garageMenu();
        }

        private void garageMenu()
        {
            while (true)
            {
                Console.WriteLine(string.Format("Main menu:{0}", Environment.NewLine));

                eGarageOptions garageOption = getValidOptionFromInput();

                switch (garageOption)
                {
                    case eGarageOptions.InsertVehicle:
                    {
                        r_GarageInsertUI.InsertNewVehicle();
                        break;
                    }

                    case eGarageOptions.ChangeVehicle:
                    {
                        r_GarageChangeVehicleUI.ChangeVehicleMenu();
                        break;
                    }

                    case eGarageOptions.GetInfo:
                    {
                        r_GarageInfoVehicleUi.VehicleInfoMenu();
                        break;
                    }

                    default:
                    {
                        break;
                    }
                }
            }
        }

        private eGarageOptions getValidOptionFromInput()
        {
            eGarageOptions option;

            Console.WriteLine(string.Format(@"Please choose one of the following options:
1. Enter a new vehicle
2. Change a vehicle attribute
3. Get vehicle information"));

            string optionInput = Console.ReadLine();

            while (!Enum.TryParse(optionInput, out option) ||
                   !Enum.IsDefined(typeof(eGarageOptions), option))
            {
                Console.WriteLine(string.Format(@"Invalid option entered, Please try again:
1. Enter a new vehicle
2. Change a vehicle attribute
3. Get vehicle information"));
                optionInput = Console.ReadLine();
            }

            return option;
        }
    }
}
