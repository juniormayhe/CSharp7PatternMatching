using System;

namespace CSharp7PatternMatching
{
    class Program
    {
        static void Main(string[] args)
        {
            object[] shapes = {
                new Circle(20),
                new Rectangle(200,20),
                new Rectangle(40,40),
                new Triangle(50, 100),
                null};

            Console.WriteLine(@"
   _____  _  _     ______ ___    _____      _   _                                    _       _     _             
  / ____|| || |_  |____  / _ \  |  __ \    | | | |                                  | |     | |   (_)            
 | |   |_  __  _|     / / | | | | |__) |_ _| |_| |_ ___ _ __ _ __    _ __ ___   __ _| |_ ___| |__  _ _ __   __ _ 
 | |    _| || |_     / /| | | | |  ___/ _` | __| __/ _ \ '__| '_ \  | '_ ` _ \ / _` | __/ __| '_ \| | '_ \ / _` |
 | |___|_  __  _|   / / | |_| | | |  | (_| | |_| ||  __/ |  | | | | | | | | | | (_| | || (__| | | | | | | | (_| |
  \_____||_||_|    /_(_) \___/  |_|   \__,_|\__|\__\___|_|  |_| |_| |_| |_| |_|\__,_|\__\___|_| |_|_|_| |_|\__, |
                                                                                                            __/ |
                                                                                                           |___/ 
");
            Console.WriteLine("C# 7.0 Pattern matching\n");
            Console.WriteLine("We will loop an array of shapes, try to cast each object as a specific Class.");
            Console.WriteLine("If cast is successful, we'll assign each object \"o\" to its suitable class instance\n");
            foreach (object o in shapes) { 
                switch (o)
                {
                    case Circle circle:
                        Console.WriteLine($"Type pattern: circle.Radius {circle.Radius}");
                        break;
                    case Triangle triangle:
                        Console.WriteLine($"Type pattern: triangle.Height {triangle.Height} x triangle.Base {triangle.Base} Triangle");
                        break;
                    case Rectangle rectangle when rectangle.Height == rectangle.Width:
                        Console.WriteLine($"Type pattern: rectangle.Width {rectangle.Width} x rectangle.Height {rectangle.Height} Square!");
                        break;
                    case Rectangle rectangle:
                        Console.WriteLine($"Type pattern: rectangle.Width {rectangle.Width} x rectangle.Height {rectangle.Height} Rectangle");
                        break;
                    case null:
                        Console.WriteLine("Constant pattern: null is a constant pattern as int and other value types");
                        break;
                    default:
                        Console.WriteLine("type unknown");
                        break;
                }//switch

            }//foreach


            //set o1 into someobject within an if expression
            object o1 = new Foo { MyProperty = 1 };
            if (o1 is var someObject)
                Console.WriteLine($"Var pattern: o1 is not null and a var pattern was set with underlying type {someObject?.GetType()?.Name}. Member value: {(someObject as Foo)?.MyProperty}");

            Console.WriteLine($"NO var pattern: o1 is not null and a var pattern was set with underlying type {o1?.GetType()?.Name}. Member value: {(o1 as Foo)?.MyProperty}");

            //set o2 into someobject within an if expression
            object o2 = null;
            if (o2 is var someObject2)
                Console.WriteLine($"Var pattern: o2 is null and a var pattern cannot be set. There is no underlying type: {someObject2?.GetType()?.Name}");
            
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


        class Foo
        {
            public int MyProperty { get; set; }
        }
    }
}
