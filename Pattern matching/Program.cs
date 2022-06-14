using System;

namespace MyApp
{
    public record Engine(int Power);

    public class Car
    {
        public Engine Engine { get; }

        public Car(Engine engine)
        {
            Engine = engine ?? throw new ArgumentNullException(nameof(engine));
        }
    }

    //#3
    public record Engine3(int Power);
    public record Car3(Engine3 Engine);
    public record Motorcycle3(Engine3 Engine);

    //#4
    public record Engine4(int Power);
    public record Car4(Engine4 Engine);
    public record Motorcycle4(Engine4 Engine);

    //#5-6
    public record Engine5(int Power);
    public record Car5(Engine5 Engine);
    public record Motorcycle5(Engine5 Engine);
    public record SmartCar(bool IsLowPower);


    public class Program
    {
        static void Main(string[] args)
        {
            // #1
            object obj = new Car(new Engine(105));
            var isCarWith105HP = obj is Car c && c.Engine is Engine e && (e.Power is int p && p == 105);
            Console.WriteLine(isCarWith105HP);

            //#2
            if (obj is Car res)
            {
                Console.WriteLine(res.Engine.Power);
            }

            //#3
            object obj1 = new Car3(new Engine3(90));
            object obj2 = new Motorcycle3(new Engine3(80));
            static bool IsLowPowerVehicle(object vehicle) =>
             vehicle switch
             {
                 Car3 and { Engine.Power: < 100 } s => true,
                 null => throw new ArgumentNullException(),
                 _ => false
             };

            Console.WriteLine(IsLowPowerVehicle(obj1));
            Console.WriteLine(IsLowPowerVehicle(obj2));

            //#4
            object obj14 = new Car4(new Engine4(900));
            object obj24 = new Motorcycle4(new Engine4(800));
            static bool IsLowPowerVehicle4(object vehicle, bool isElectro) =>
             (vehicle,isElectro) switch
             {
                 (_, true) => true,
                 (Car4 { Engine.Power: < 100 }, _) => true,
                 (null,_) => throw new ArgumentNullException(),
                 _ => false
             };
            Console.WriteLine(IsLowPowerVehicle4(obj14, true));
            Console.WriteLine(IsLowPowerVehicle4(obj24, false));


            //#5-6
            object obj15 = new Car5(new Engine5(900));
            object obj25 = new Motorcycle5(new Engine5(800));
            object obj35 = new SmartCar(true);
            static bool IsLowPowerVehicle5(object vehicle, bool? isElectro) =>
             (vehicle, isElectro) switch
             {
                 ( SmartCar car,_ )  => car.IsLowPower,
                 (_, true) => true,
                 (Car5 and { Engine.Power: < 100 }, _) => true,
                 (null, _) => throw new ArgumentNullException(),
                 (not(Car5 or Motorcycle5 or SmartCar),_) => throw new Exception(),
                 _ => false
             };
            Console.WriteLine(IsLowPowerVehicle5(obj15, true));
            Console.WriteLine(IsLowPowerVehicle5(obj25, false));
            Console.WriteLine(IsLowPowerVehicle5(obj35, null));


        }
    }
}