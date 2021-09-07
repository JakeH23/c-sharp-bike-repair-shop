namespace BikeRepairShop.Models
{
    using BikeRepairShop.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ServicePerson
    {
        private ServicePerson(Machine bike)
        {
            this.CurrentJob = bike;
        }

        public static ServicePerson Create(Machine bike)
        {
            return new ServicePerson(bike);
        }

        public Machine CurrentJob { get; }

        public void CheckUp()
        {
            var tasks = new List<Task>();
            if (CurrentJob.Parts.ContainsValue(Condition.Broken))
            {
                tasks.Add(Task.Run(OrderSpareParts));
            }

            if (CurrentJob.Parts.ContainsValue(Condition.Fragile))
            {
                tasks.Add(Task.Run(FixUpParts));
            }

            tasks.Add(Task.Run(ServiceBike));

            Task.WaitAll(tasks.ToArray());

            CompleteCheckUp();
        }

        private void OrderSpareParts()
        {
            Task.Delay(5000);
            var brokenParts = CurrentJob.Parts.Where(x => x.Value == Condition.Broken).Select(x => x.Key);

            foreach (var part in brokenParts)
            {
                CurrentJob.Parts[part] = Condition.Fragile;
            }
        }

        private void FixUpParts()
        {
            var fragileParts = CurrentJob.Parts.Where(x => x.Value == Condition.Fragile).Select(x => x.Key);
            var timeOfJob = fragileParts.Sum(part => 1000);

            Task.Delay(timeOfJob);

            foreach (var part in fragileParts)
            {
                CurrentJob.Parts[part] = Condition.Fine;
            }
        }

        private void ServiceBike()
        {
            Task.Run(PumpWheels);
            var oilTask = Task.Run(Oil);
            var awaiter = oilTask.GetAwaiter();
            awaiter.OnCompleted(Clean);
        }

        private void Oil()
        {
            Task.Delay(500);
            CurrentJob.Parts[Components.Breaks] = Condition.Pristine;
            CurrentJob.Parts[Components.Gears] = Condition.Pristine;
        }

        private void Clean()
        {
            Task.Delay(500);
            CurrentJob.Parts[Components.Frame] = Condition.Pristine;
        }

        private void PumpWheels()
        {
            Task.Delay(500);
            CurrentJob.Parts[Components.Tyres] = Condition.Pristine;
        }

        private void CompleteCheckUp()
        {
            if (CurrentJob is Bike job)
            {
                Console.WriteLine(job.RingBell());
            }
        }
    }
}
