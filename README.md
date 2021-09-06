# c-sharp-bike-repair-shop

You finally decide to take the plunge and follow your dreams - and set up a bike repair shop! Everything is going great, but you feel like you can optimise things a bit better... with C#. And TDD.

## `Bike` Class

Before you get stared with the class itself, set up a few of `enums`, described below. You can read more about enums here: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/enum

- `Components` consists of:
  - gears
  - breaks
  - frame
  - tyres

- `Condition` consists of:
  - pristine
  - fine
  - fragile
  - broken

- `BikeTypes` consists of:
  - road
  - mountain
  - hybrid

Now lets get started on the bike class itself!

### Properties
- public DateTime DateLastMaintained
- public BikeType Type - can be any of the `BikeType` enum
- public Dictionary Parts - a Dictionary is a collection of key value pairs, so you will need to declare two types when creating one - the key here should be one of the `Components` enum, and the value should be one of the `Condition` enum. Remember how to make a primitive value nullable? And for more details on Dictionaries, the docs are here: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.dictionary-2?view=net-5.0

### Methods
- `TestRide` - should have a quick check of the condition of each part of the bike and if:
  - all are `fine` or `pristine` should return "The bike rides beautifully!"
  - any are `broken` should return "This bike is broken, I can't ride it like this!"
  - any are `fragile`, but none are `broken` should return "It's a comfy ride!"

- `RingBell`
  - all are `fine` or `pristine` should return "Ring!Ring!Ring!"
  - any are `broken` should return "The bell fell off!"
  - any are `fragile`, but none are `broken` should return "Ring! cling.."

### Additional tasks for Bike class
Please work on these after you have finished all other main sections

- add a `folding` and `cyclocross` types to the `BikeType` enum. 
- add electronics to the `Components` enum.
- make the value of the `Parts` Dictionary nullable to accommodate that not all bikes will have an electronics component.

At this point, all your current tests may need updating so that when you create a road bike, for example, the `electronics` part is null. Only `cyclocross` bikes should have a non-null value for `electronics`.

## `ServicePerson` class

A ServicePerson will check over a bike, figure out if anything is wrong, fix it up if it is broken or service it to make it pristine again.

### Properties

- public Bike CurrentJob - the bike which is currently being serviced. To set a bike as the current job, there shouldn't be another bike being checked over. 

### Methods

Here we will create a few public methods, which will be exposed externally to service and repair the bikes, as well as a few private ones which will be used only within the class. They will be called by the public methods.

- private ones:
  - `OrderSpareParts` - Set the thread to sleep to simulate ordering new parts - you can order all `broken` parts at once. Change all `broken` parts to `fragile`. 
  - `FixUpParts` - Make sure to set the thread to sleep to simulate the job taking some time - the more broken parts there are, the longer the job should take. Change all `fragile` parts to `fine`.
  - `Oil` - set the thread to sleep for a few milliseconds. Change the `breaks` and `gears` part from `fine` to `pristine`. 
  - `Clean` - set the thread to sleep for a few milliseconds to simulate cleaning the bike. Change the `frame` part from `fine` to `pristine`. 
  - `PumpWheels` - set the thread to sleep for a few milliseconds. Change the `tyres` part from `fine` to `pristine`. 
  - `ServiceBike` - invokes the `Oil`, `Clean` and `PumpWheels` methods. Make sure to oil the bike before cleaning it, so we don't make it dirty again. `PumpWheels` can happen at any point. 


- public
  - `CheckUp`:
    - if any parts are `broken` triggers the `OrderSpareParts` method 
    - if any parts are `fragile` triggers the `FixUpParts` method
    - if the parts are either `fine` or `pristine` triggers the `ServiceBike` method
    - once all maintenance work is done, triggers the `CompleteCheckUp` method
    - ring the bike's bell to feel that sweet, sweet freshly services bike satisfaction!


### Advanced tasks
Please work on these after you have finished all other main sections

- The `Bike` and `ServicePerson` classes are pretty tightly coupled - how will you make them more loosely coupled? This change will also allow us to provide a machine which isn't a bike to be serviced - so long as it conforms to a set of criteria - has parts with specific conditions for us to check over and maintain. How will this change impact the test suite? 
- Use mocks to help you with the above refactor.
