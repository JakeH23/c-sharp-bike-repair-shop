namespace BikeRepairShop.Models
{
    using BikeRepairShop.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class Machine
    {
        protected abstract string GenericMachineName { get; }

        protected abstract string GenericMachineMovementMethod { get; }

        public DateTime? DateLastMaintained { get; set; }

        public MachineTypes MachineType { get; set; }

        public Dictionary<Components, Condition> Parts { get; set; }

        public string TestRide()
        {
            var conditions = Parts.Values.ToList();

            if (conditions.Contains(Condition.Broken))
            {
                return $"This {GenericMachineName} is broken, I can't {GenericMachineMovementMethod} it like this!";
            }

            if (conditions.Contains(Condition.Fragile))
            {
                return $"It's a comfy {GenericMachineMovementMethod}!";
            }

            return $"The {GenericMachineName} {GenericMachineMovementMethod}s beautifully!";
        }

        protected Machine(MachineTypes type, Dictionary<Components, Condition> parts, DateTime? lastMaintained = null)
        {
            MachineType = type;
            Parts = parts;
            DateLastMaintained = lastMaintained;
        }
    }
}