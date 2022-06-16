using System;

namespace MyApp
{
    public record Engine(int Power);
    public record Motorcycle(Engine Engine);
    public record SmartCar(bool IsLowPower);

    public class Car
    {
        public Engine Engine { get; }
        public Car(Engine engine)
        {
            Engine = engine ?? throw new ArgumentNullException(nameof(engine));
        }
    }


    public class Program
    {
        static void Main(string[] args)
        {
            //// Task#1
            object obj = new Car(new Engine(105));
            var isCarWith105HP = obj is Car c && c.Engine is Engine e && (e.Power is int p && p == 105);
            Console.WriteLine(isCarWith105HP);


            //// Task#2
            if (obj is Car res)
            {
                Console.WriteLine(res.Engine.Power);
            }


            //// Task#3
            object obj1 = new Car(new Engine(90));
            object obj2 = new Motorcycle(new Engine(80));
            static bool IsLowPowerVehicle3(object vehicle) =>
                vehicle switch
                {
                    Car and { Engine.Power: < 100 } s => true,
                    Motorcycle and { Engine.Power: < 100 } s => true,
                    null => throw new ArgumentNullException(),
                    _ => false
                };
            Console.WriteLine(IsLowPowerVehicle3(obj1));
            Console.WriteLine(IsLowPowerVehicle3(obj2));


            //// Task#4
            object obj3 = new Car(new Engine(900));
            object obj4 = new Motorcycle(new Engine(800));
            static bool IsLowPowerVehicle4(object vehicle, bool isElectro) =>
                (vehicle,isElectro) switch
                {
                    (_, true) => true,
                    (Car { Engine.Power: < 100 }, _) => true,
                    (Motorcycle { Engine.Power: < 100 }, _) => true,
                    (null,_) => throw new ArgumentNullException(),
                    _ => false
                };
            Console.WriteLine(IsLowPowerVehicle4(obj3, true));
            Console.WriteLine(IsLowPowerVehicle4(obj4, false));


            //// Task#5-6
            object obj5 = new Car(new Engine(900));
            object obj6 = new Motorcycle(new Engine(800));
            object obj7 = new SmartCar(true);
            static bool IsLowPowerVehicle56(object vehicle, bool? isElectro) =>
                (vehicle, isElectro) switch
                {
                    (SmartCar car, _)  => car.IsLowPower,
                    (_, true) => true,
                    (Car and { Engine.Power: < 100 }, _) => true,
                    (Motorcycle { Engine.Power: < 100 }, _) => true,
                    (null, _) => throw new ArgumentNullException(),
                    (not(Car or Motorcycle or SmartCar),_) => throw new Exception(),
                    _ => false
                };
            Console.WriteLine(IsLowPowerVehicle56(obj5, true));
            Console.WriteLine(IsLowPowerVehicle56(obj6, false));
            Console.WriteLine(IsLowPowerVehicle56(obj7, null));
        }
    }
}