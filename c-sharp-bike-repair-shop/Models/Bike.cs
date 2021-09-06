namespace BikeRepairShop.Models
{
    using BikeRepairShop.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Bike
    {
        private Bike(BikeTypes bikeType, Dictionary<Components, Condition> parts, DateTime? lastMaintained = null)
        {
            Type = bikeType;
            Parts = parts;
            DateLastMaintained = lastMaintained;
        }

        public static Bike Create(BikeTypes bikeType, Dictionary<Components, Condition> parts, DateTime? lastMaintained = null)
        {
            return new Bike(bikeType, parts, lastMaintained);
        }

        public DateTime? DateLastMaintained { get; set; }

        public BikeTypes Type { get; set; }

        public Dictionary<Components, Condition> Parts { get; set; }

        public string TestRide()
        {
            var conditions = Parts.Values.ToList();

            if (conditions.Contains(Condition.Broken))
            {
                return "This bike is broken, I can't ride it like this!";
            }

            if (conditions.Contains(Condition.Fragile))
            {
                return "It's a comfy ride!";
            }

            return "The bike rides beautifully!";
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
