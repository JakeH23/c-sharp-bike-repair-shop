namespace BikeRepairShop.Models
{
    using BikeRepairShop.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Bike : IMachine
    {
        public string GenericMachineName => "Bike";

        public string GenericMachineMovementMethod => "Ride";

        public MachineTypes MachineType { get; }

        public DateTime? DateLastMaintained { get; }

        public Dictionary<Components, Condition> Parts { get; set; }

        private Bike(MachineTypes type, Dictionary<Components, Condition> parts, DateTime? lastMaintained = null)
        {
            MachineType = type;
            Parts = parts;
            DateLastMaintained = lastMaintained;
        }

        public static Bike Create(MachineTypes type, Dictionary<Components, Condition> parts, DateTime? lastMaintained = null)
        {
            return new Bike(type, parts, lastMaintained);
        }

        public string RingBell()
        {
            var conditions = Parts.Values.ToList();

            if (conditions.Contains(Condition.Broken))
            {
                return "The bell fell off!";
            }

            if (conditions.Contains(Condition.Fragile))
            {
                return "Ring! cling..";
            }

            return "Ring!Ring!Ring!";
        }
    }
}
