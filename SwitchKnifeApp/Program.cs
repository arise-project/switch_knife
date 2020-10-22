using System;

namespace SwitchKnifeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("map_test_to_class");
            var choice = Console.ReadLine();

            switch(choice)
            {
                case "map_test_to_class":
                    Console.WriteLine("tests folder:");
                    var testsFolder = Console.ReadLine();
                    Console.WriteLine("classes folder:");
                    var classesFolder = Console.ReadLine();
                    var e1 = new MapTestToClass();
                    e1.SetArguments(testsFolder, classesFolder);
                    e1.Execute();
                break;
            }
        }
    }
}
