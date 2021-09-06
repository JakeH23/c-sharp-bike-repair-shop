namespace BikeRepairShopTests
{
    using System.Collections.Generic;
    using BikeRepairShop.Enums;
    using BikeRepairShop.Models;
    using Xunit;

    public class BikeTests
    {
        private const MachineTypes TestType = MachineTypes.Road;
        private static readonly Dictionary<Components, Condition> TestParts = new Dictionary<Components, Condition>();
        private static readonly Bike TestBike = Bike.Create(TestType, TestParts);

        public class TestRideTests
        {
            [Fact]
            public void WhenPartsAreFineOrPristine_ReturnsBeautifulResponse()
            {
                // arrange
                TestBike.Parts = new Dictionary<Components, Condition>
                {
                    { Components.Gears, Condition.Fine },
                    { Components.Breaks, Condition.Pristine },
                    { Components.Frame, Condition.Fine },
                    { Components.Tyres, Condition.Pristine }
                };

                // act
                var res = TestBike.TestRide();

                // assert
                Assert.Equal("The bike rides beautifully!", res);
            }

            [Fact]
            public void WhenSomePartsAreFragile_ReturnsComfyResponse()
            {
                // arrange
                TestBike.Parts = new Dictionary<Components, Condition>
                {
                    { Components.Gears, Condition.Fragile },
                    { Components.Breaks, Condition.Pristine },
                    { Components.Frame, Condition.Fragile },
                    { Components.Tyres, Condition.Fine }
                };

                // act
                var res = TestBike.TestRide();

                // assert
                Assert.Equal("It's a comfy ride!", res);
            }

            [Fact]
            public void WhenSomePartsAreBroken_ReturnsBrokenResponse()
            {
                // arrange
                TestBike.Parts = new Dictionary<Components, Condition>
                {
                    { Components.Gears, Condition.Broken },
                    { Components.Breaks, Condition.Pristine },
                    { Components.Frame, Condition.Fragile },
                    { Components.Tyres, Condition.Fine }
                };

                // act
                var res = TestBike.TestRide();

                // assert
                Assert.Equal("This bike is broken, I can't ride it like this!", res);
            }
        }

        public class RingBellTests
        {
            [Fact]
            public void WhenPartsAreFineOrPristine_ReturnsBeautifulResponse()
            {
                // arrange
                TestBike.Parts = new Dictionary<Components, Condition>
                {
                    { Components.Gears, Condition.Fine },
                    { Components.Breaks, Condition.Pristine },
                    { Components.Frame, Condition.Fine },
                    { Components.Tyres, Condition.Pristine }
                };

                // act
                var res = TestBike.RingBell();

                // assert
                Assert.Equal("Ring!Ring!Ring!", res);
            }

            [Fact]
            public void WhenSomePartsAreFragile_ReturnsComfyResponse()
            {
                // arrange
                TestBike.Parts = new Dictionary<Components, Condition>
                {
                    { Components.Gears, Condition.Fragile },
                    { Components.Breaks, Condition.Pristine },
                    { Components.Frame, Condition.Fragile },
                    { Components.Tyres, Condition.Fine }
                };

                // act
                var res = TestBike.RingBell();

                // assert
                Assert.Equal("Ring! cling..", res);
            }

            [Fact]
            public void WhenSomePartsAreBroken_ReturnsBrokenResponse()
            {
                // arrange
                TestBike.Parts = new Dictionary<Components, Condition>
                {
                    { Components.Gears, Condition.Broken },
                    { Components.Breaks, Condition.Pristine },
                    { Components.Frame, Condition.Fragile },
                    { Components.Tyres, Condition.Fine }
                };

                // act
                var res = TestBike.RingBell();

                // assert
                Assert.Equal("The bell fell off!", res);
            }
        }
    }
}
