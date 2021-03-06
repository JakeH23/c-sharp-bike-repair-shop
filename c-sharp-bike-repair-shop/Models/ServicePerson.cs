namespace BikeRepairShop.Models
{
    using BikeRepairShop.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ServicePerson
    {
        private ServicePerson(Machine machine)
        {
            this.CurrentJob = machine;
        }

        public static ServicePerson Create(Machine machine)
        {
            return new ServicePerson(machine);
        }

        public Machine CurrentJob { get; }

        public async Task CheckUp()
        {
            var tasks = new List<Task>();
            if (CurrentJob.Parts.ContainsValue(Condition.Broken))
            {
                tasks.Add(OrderSpareParts());
            }

            if (CurrentJob.Parts.ContainsValue(Condition.Fragile))
            {
                tasks.Add(FixUpParts());
            }

            tasks.Add(ServiceMachine());
            await Task.WhenAll(tasks);

            CompleteCheckUp();
        }

        private Task OrderSpareParts()
        {
            var brokenParts = CurrentJob.Parts.Where(x => x.Value == Condition.Broken).Select(x => x.Key).ToList();

            foreach (var part in brokenParts)
            {
                CurrentJob.Parts[part] = Condition.Fragile;
            }

            return Task.Run(() => Task.Delay(5000));
        }

        private Task FixUpParts()
        {
            var fragileParts = CurrentJob.Parts.Where(x => x.Value == Condition.Fragile).Select(x => x.Key).ToList();
            var timeOfJob = fragileParts.Sum(part => 1000);

            foreach (var part in fragileParts)
            {
                CurrentJob.Parts[part] = Condition.Fine;
            }

            return Task.Run(() => Task.Delay(timeOfJob));
        }

        private bool HasElectronics()
        {
            var electronics = CurrentJob.Parts.FirstOrDefault(x => x.Key == Components.Electronics).Value;
            return electronics != null && CurrentJob.MachineType == MachineTypes.Cyclocross;
        }

        private async Task ServiceMachine()
        {
            PumpWheels();
            if (HasElectronics())
            {
                FixElectronics();
            }
            await Oil();
            Clean();
        }

        private Task Oil()
        {
            CurrentJob.Parts[Components.Breaks] = Condition.Pristine;
            CurrentJob.Parts[Components.Gears] = Condition.Pristine;
            return Task.Run(() => Task.Delay(500));
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

        private void FixElectronics()
        {
            Task.Delay(500);
            CurrentJob.Parts[Components.Electronics] = Condition.Pristine;
        }

        private void CompleteCheckUp()
        {
            switch (CurrentJob)
            {
                case Bike bike:
                    Console.WriteLine(bike.RingBell());
                    break;
                case Car car:
                    Console.WriteLine(car.TootHorn());
                    break;
            }
        }
    }
}
