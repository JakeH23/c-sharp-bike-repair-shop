namespace BikeRepairShop.Models
{
    using BikeRepairShop.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Bike : Machine
    {
        protected override string GenericMachineName => "bike";

        protected override string GenericMachineMovementMethod => "ride";

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

        public Bike(MachineTypes type, Dictionary<Components, Condition> parts, DateTime? lastMaintained = null) : base(type, parts, lastMaintained)
        {
            MachineType = type;
            Parts = parts;
            DateLastMaintained = lastMaintained;
        }

        public static Bike Create(MachineTypes machineType, Dictionary<Components, Condition> parts, DateTime? lastMaintained = null)
        {
            return new Bike(machineType, parts, lastMaintained);
        }
    }
}
