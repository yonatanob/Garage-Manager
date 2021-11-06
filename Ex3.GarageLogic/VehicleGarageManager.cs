namespace GarageLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class VehicleGarageManager
    {
        private readonly Dictionary<string, Vehicle> r_GarageVehiclesDictionary;

        public VehicleGarageManager()
        {
            r_GarageVehiclesDictionary = new Dictionary<string, Vehicle>();
        }

        public bool DoesVehicleExist(string i_LicenseNumber)
        {
            if (i_LicenseNumber != null)
            {
                return !string.IsNullOrEmpty(i_LicenseNumber) &&
                       r_GarageVehiclesDictionary.ContainsKey(i_LicenseNumber);
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void AddVehicle(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_Vehicle)
        {
            if (i_Vehicle != null)
            {
                VehicleRepairInfo vehicleInfo = new VehicleRepairInfo(i_OwnerName, i_OwnerPhoneNumber);
                i_Vehicle.RepairInfo = vehicleInfo;
                r_GarageVehiclesDictionary.Add(i_Vehicle.LicenseNumber, i_Vehicle);
            }
            else
            {
                throw new ArgumentNullException("Vehicle provided is null");
            }
        }

        public void SetVehicleStatus(string i_LicenseNumber, eVehicleRepairStatus i_Status)
        {
            r_GarageVehiclesDictionary[i_LicenseNumber].RepairInfo.RepairStatus = i_Status;
        }

        public string GetVehicleInfoString(string i_LicenseNumber)
        {
            return r_GarageVehiclesDictionary[i_LicenseNumber].ToString();
        }

        public void Refuel(string i_LicenseNumber, eFuelType i_FuelType, float i_Liters)
        {
            r_GarageVehiclesDictionary[i_LicenseNumber].Refuel(i_FuelType, i_Liters);
        }

        public void RechargeBattery(string i_LicenseNumber, float i_Hours)
        {
            r_GarageVehiclesDictionary[i_LicenseNumber].RechargeBattery(i_Hours);
        }

        public void InflateWheelsWithAirToMax(string i_LicenseNumber)
        {
            r_GarageVehiclesDictionary[i_LicenseNumber].InflateWheelsWithAirToMax();
        }

        public string GetVehiclesLicensesByRepairStatus(eVehicleRepairStatus i_VehicleRepairStatus)
        {
            StringBuilder vehiclesLicensesStringBuilder = new StringBuilder(string.Format("Vehicles with repair status : {0}{1}", i_VehicleRepairStatus, Environment.NewLine));

            List<string> vehiclesWithRepairStatus = r_GarageVehiclesDictionary.Values.
                Where(vehicle => vehicle.RepairInfo.RepairStatus == i_VehicleRepairStatus).
                Select(vehicle => vehicle.LicenseNumber).
                ToList();

            vehiclesLicensesStringBuilder.Append(string.Format("{0}{1}", string.Join(Environment.NewLine, vehiclesWithRepairStatus), Environment.NewLine));
            return vehiclesLicensesStringBuilder.ToString();
        }

        public float GetEnergyPercentInVehicle(string i_LicenseNumber)
        {
            return r_GarageVehiclesDictionary[i_LicenseNumber].PercentOfEnergyLeftInEngine;
        }

        public float GetMaximumAmountOfEnergyInVehicle(string i_LicenseNumber)
        {
            return r_GarageVehiclesDictionary[i_LicenseNumber].GetMaxEngineVolume();
        }
    }
}
