using System.Threading.Tasks;

namespace BikeRepairShopTests
{
    using BikeRepairShop.Enums;
    using BikeRepairShop.Models;
    using System.Collections.Generic;
    using Xunit;

    public class ServicePersonTests
    {
        private const MachineTypes TestType = MachineTypes.Road;
        private static readonly Dictionary<Components, Condition?> TestParts = new Dictionary<Components, Condition?>();
        private static readonly Bike TestBike = Bike.Create(TestType, TestParts);

        public class CheckUpTests
        {
            [Fact]
            public async Task WhenPartsAreInVaryingConditions_ReturnsAllPartsConditionsAsPristine()
            {
                // arrange
                TestBike.Parts = new Dictionary<Components, Condition?>
                {
                    { Components.Gears, Condition.Broken },
                    { Components.Breaks, Condition.Fragile },
                    { Components.Frame, Condition.Fine },
                    { Components.Tyres, Condition.Pristine }
                };

                // act
                var servicePerson = ServicePerson.Create(TestBike);
                await servicePerson.CheckUp();

                // assert
                Assert.Equal(Condition.Pristine, servicePerson.CurrentJob.Parts[Components.Gears]);
                Assert.Equal(Condition.Pristine, servicePerson.CurrentJob.Parts[Components.Tyres]);
                Assert.Equal(Condition.Pristine, servicePerson.CurrentJob.Parts[Components.Frame]);
                Assert.Equal(Condition.Pristine, servicePerson.CurrentJob.Parts[Components.Breaks]);
                Assert.False(servicePerson.CurrentJob.Parts.ContainsKey(Components.Electronics));
            }
        }
    }
}
