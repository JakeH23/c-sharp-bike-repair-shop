namespace BikeRepairShop.Models
{
    using System;
    using System.Linq;
    using System.Threading;
    using BikeRepairShop.Enums;

    public class ServicePerson
    {
        private ServicePerson(IMachine bike)
        {
            this.CurrentJob = bike;
        }

        public static ServicePerson Create(IMachine bike)
        {
            return new ServicePerson(bike);
        }

        public IMachine CurrentJob { get; }

        public void CheckUp()
        {
            Thread brokenThread = null;
            if (CurrentJob.Parts.ContainsValue(Condition.Broken))
            {
                brokenThread = new Thread(OrderSpareParts);
            }

            Thread fragileThread = null;
            if (CurrentJob.Parts.ContainsValue(Condition.Fragile))
            {
                fragileThread = new Thread(FixUpParts);
            }

            var serviceThread = new Thread(ServiceBike);
            var checkUpCompleteThread = new Thread(CompleteCheckUp);

            brokenThread?.Start();
            fragileThread?.Start();
            serviceThread.Start();

            brokenThread?.Join();
            fragileThread?.Join();
            serviceThread.Join();
            checkUpCompleteThread.Start();
        }

        private void OrderSpareParts()
        {
            Thread.Sleep(5000);
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

            Thread.Sleep(timeOfJob);

            foreach (var part in fragileParts)
            {
                CurrentJob.Parts[part] = Condition.Fine;
            }
        }

        private void ServiceBike()
        {
            var oilThread = new Thread(Oil);
            var cleanThread = new Thread(Clean);
            var pumpWheelsThread = new Thread(PumpWheels);

            pumpWheelsThread.Start();
            oilThread.Start();
            oilThread.Join();
            cleanThread.Start();
        }

        private void Oil()
        {
            Thread.Sleep(500);
            CurrentJob.Parts[Components.Breaks] = Condition.Pristine;
            CurrentJob.Parts[Components.Gears] = Condition.Pristine;
        }

        private void Clean()
        {
            Thread.Sleep(500);
            CurrentJob.Parts[Components.Frame] = Condition.Pristine;
        }

        private void PumpWheels()
        {
            Thread.Sleep(500);
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
