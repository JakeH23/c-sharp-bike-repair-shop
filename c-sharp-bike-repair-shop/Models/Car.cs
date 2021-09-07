namespace BikeRepairShop.Models
{
    using BikeRepairShop.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Car : Machine
    {
        protected override string GenericMachineName => "car";

        protected override string GenericMachineMovementMethod => "drive";

        public string TootHorn()
        {
            var conditions = Parts.Values.ToList();

            if (conditions.Contains(Condition.Broken))
            {
                return "Wheres the horn at?!";
            }

            if (conditions.Contains(Condition.Fragile))
            {
                return "SQUEEEEEKKKKKKKKKKKK.";
            }

            return "Honk!Honk!Honk!";
        }

        private Car(MachineTypes type, Dictionary<Components, Condition?> parts, DateTime? lastMaintained = null) : base(type, parts, lastMaintained)
        {
            MachineType = type;
            Parts = parts;
            DateLastMaintained = lastMaintained;
        }

        public static Car Create(MachineTypes machineType, Dictionary<Components, Condition?> parts, DateTime? lastMaintained = null)
        {
            return new Car(machineType, parts, lastMaintained);
        }
    }
}
