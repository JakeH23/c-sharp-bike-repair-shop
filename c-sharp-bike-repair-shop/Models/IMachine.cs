using System;
using System.Collections.Generic;
using System.Linq;
using BikeRepairShop.Enums;

namespace BikeRepairShop.Models
{
    public interface IMachine
    {
        string GenericMachineName { get; }

        string GenericMachineMovementMethod { get; }

        DateTime? DateLastMaintained { get; }

        MachineTypes MachineType { get; }

        Dictionary<Components, Condition> Parts { get; set; }

        string TestRide()
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
    }
}