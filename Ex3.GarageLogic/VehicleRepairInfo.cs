namespace GarageLogic
{
    public enum eVehicleRepairStatus
    {
        InRepair = 1,
        Repaired = 2,
        PaidFor = 3
    }

    public class VehicleRepairInfo
    {
        private readonly string r_OwnerName;
        private readonly string r_OwnerPhoneNumber;
        private eVehicleRepairStatus m_RepairStatus;

        public VehicleRepairInfo(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            r_OwnerName = i_OwnerName;
            r_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_RepairStatus = eVehicleRepairStatus.InRepair;
        }

        public string OwnerName
        {
            get
            {
                return r_OwnerName;
            }
        }

        public string OwnerPhoneNumber
        {
            get
            {
                return r_OwnerPhoneNumber;
            }
        }

        public eVehicleRepairStatus RepairStatus
        {
            get
            {
                return m_RepairStatus;
            }

            set
            {
                m_RepairStatus = value;
            }
        }
    }
}
