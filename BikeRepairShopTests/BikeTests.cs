namespace BikeRepairShopTests
{
    using BikeRepairShop.Enums;
    using BikeRepairShop.Models;
    using System.Collections.Generic;
    using Xunit;

    public class BikeTests
    {
        private const MachineTypes TestType = MachineTypes.Road;
        private static readonly Dictionary<Components, Condition?> TestParts = new Dictionary<Components, Condition?>();
        private static readonly Bike TestBike = Bike.Create(TestType, TestParts);

        public class BikePropertyTests
        {
            [Theory]
            [InlineData(MachineTypes.Road)]
            [InlineData(MachineTypes.Hybrid)]
            [InlineData(MachineTypes.Mountain)]
            [InlineData(MachineTypes.Folding)]
            public void WhenBikeWithNoElectronics_ReturnsUnalteredPartsDictionary(MachineTypes type)
            {
                // arrange
                var machineType = type;

                var parts = new Dictionary<Components, Condition?>
                {
                    { Components.Gears, Condition.Pristine },
                    { Components.Breaks, Condition.Pristine },
                    { Components.Frame, Condition.Pristine },
                    { Components.Tyres, Condition.Pristine }
                };

                // act
                var res = Bike.Create(machineType, parts);

                // assert
                Assert.Equal(4, res.Parts.Count);
            }

            [Fact]
            public void WhenBikeWithElectronicsButNotSet_ReturnsAlteredPartsDictionaryWithNullValue()
            {
                // arrange
                var machineType = MachineTypes.Cyclocross;

                var parts = new Dictionary<Components, Condition?>
                {
                    { Components.Gears, Condition.Pristine },
                    { Components.Breaks, Condition.Pristine },
                    { Components.Frame, Condition.Pristine },
                    { Components.Tyres, Condition.Pristine }
                };

                // act
                var res = Bike.Create(machineType, parts);

                // assert
                Assert.Equal(5, res.Parts.Count);
                Assert.Null(res.Parts[Components.Electronics]);
            }

            [Fact]
            public void WhenBikeWithElectronicsAndSet_ReturnsAlteredPartsDictionaryWithSetValue()
            {
                // arrange
                var machineType = MachineTypes.Cyclocross;

                var parts = new Dictionary<Components, Condition?>
                {
                    { Components.Gears, Condition.Pristine },
                    { Components.Breaks, Condition.Pristine },
                    { Components.Frame, Condition.Pristine },
                    { Components.Tyres, Condition.Pristine },
                    { Components.Electronics, Condition.Pristine }
                };

                // act
                var res = Bike.Create(machineType, parts);

                // assert
                Assert.Equal(5, res.Parts.Count);
                Assert.Equal(Condition.Pristine, res.Parts[Components.Electronics]);
            }
        }

        public class TestRideTests
        {
            [Fact]
            public void WhenPartsAreFineOrPristine_ReturnsBeautifulResponse()
            {
                // arrange
                TestBike.Parts = new Dictionary<Components, Condition?>
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
                TestBike.Parts = new Dictionary<Components, Condition?>
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
                TestBike.Parts = new Dictionary<Components, Condition?>
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
                TestBike.Parts = new Dictionary<Components, Condition?>
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
                TestBike.Parts = new Dictionary<Components, Condition?>
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
                TestBike.Parts = new Dictionary<Components, Condition?>
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
