namespace BikeRepairShopTests
{
    using BikeRepairShop.Enums;
    using BikeRepairShop.Models;
    using System.Collections.Generic;
    using Xunit;

    public class CarTests
    {
        private const MachineTypes TestType = MachineTypes.Hatchback;
        private static readonly Dictionary<Components, Condition?> TestParts = new Dictionary<Components, Condition?>();
        private static readonly Car TestCar = Car.Create(TestType, TestParts);

        public class TestRideTests
        {
            [Fact]
            public void WhenPartsAreFineOrPristine_ReturnsBeautifulResponse()
            {
                // arrange
                TestCar.Parts = new Dictionary<Components, Condition?>
                {
                    { Components.Gears, Condition.Fine },
                    { Components.Breaks, Condition.Pristine },
                    { Components.Frame, Condition.Fine },
                    { Components.Tyres, Condition.Pristine }
                };

                // act
                var res = TestCar.TestRide();

                // assert
                Assert.Equal("The car drives beautifully!", res);
            }

            [Fact]
            public void WhenSomePartsAreFragile_ReturnsComfyResponse()
            {
                // arrange
                TestCar.Parts = new Dictionary<Components, Condition?>
                {
                    { Components.Gears, Condition.Fragile },
                    { Components.Breaks, Condition.Pristine },
                    { Components.Frame, Condition.Fragile },
                    { Components.Tyres, Condition.Fine }
                };

                // act
                var res = TestCar.TestRide();

                // assert
                Assert.Equal("It's a comfy drive!", res);
            }

            [Fact]
            public void WhenSomePartsAreBroken_ReturnsBrokenResponse()
            {
                // arrange
                TestCar.Parts = new Dictionary<Components, Condition?>
                {
                    { Components.Gears, Condition.Broken },
                    { Components.Breaks, Condition.Pristine },
                    { Components.Frame, Condition.Fragile },
                    { Components.Tyres, Condition.Fine }
                };

                // act
                var res = TestCar.TestRide();

                // assert
                Assert.Equal("This car is broken, I can't drive it like this!", res);
            }
        }

        public class RingBellTests
        {
            [Fact]
            public void WhenPartsAreFineOrPristine_ReturnsBeautifulResponse()
            {
                // arrange
                TestCar.Parts = new Dictionary<Components, Condition?>
                {
                    { Components.Gears, Condition.Fine },
                    { Components.Breaks, Condition.Pristine },
                    { Components.Frame, Condition.Fine },
                    { Components.Tyres, Condition.Pristine }
                };

                // act
                var res = TestCar.TootHorn();

                // assert
                Assert.Equal("Honk!Honk!Honk!", res);
            }

            [Fact]
            public void WhenSomePartsAreFragile_ReturnsComfyResponse()
            {
                // arrange
                TestCar.Parts = new Dictionary<Components, Condition?>
                {
                    { Components.Gears, Condition.Fragile },
                    { Components.Breaks, Condition.Pristine },
                    { Components.Frame, Condition.Fragile },
                    { Components.Tyres, Condition.Fine }
                };

                // act
                var res = TestCar.TootHorn();

                // assert
                Assert.Equal("SQUEEEEEKKKKKKKKKKKK.", res);
            }

            [Fact]
            public void WhenSomePartsAreBroken_ReturnsBrokenResponse()
            {
                // arrange
                TestCar.Parts = new Dictionary<Components, Condition?>
                {
                    { Components.Gears, Condition.Broken },
                    { Components.Breaks, Condition.Pristine },
                    { Components.Frame, Condition.Fragile },
                    { Components.Tyres, Condition.Fine }
                };

                // act
                var res = TestCar.TootHorn();

                // assert
                Assert.Equal("Wheres the horn at?!", res);
            }
        }
    }
}
