using System;

namespace SwitchKnifeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("map_test_to_class|csproj_find_not_present_file");
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
                case "csproj_find_not_present_file":
                    Console.WriteLine("folder:");
                    var filesFolder = Console.ReadLine();
                    Console.WriteLine("csproj file:");
                    var csprojFile = Console.ReadLine();
                    var e2 = new CsprojStructure();
                    e2.FindFilesNotPresent(filesFolder, csprojFile);
                    break;
                
            }
        }
    }
}
