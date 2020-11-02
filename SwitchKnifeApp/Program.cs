using System;

namespace SwitchKnifeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("map_test_to_class|csproj_find_not_present_file|file_remover");
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
                case "unused_resource":
                    Console.WriteLine("project folder:");
                    var projectFolder = Console.ReadLine();
                    Console.WriteLine("resource folder:");
                    var resourceFolder = Console.ReadLine();
                    var e3 = new ResourceToProject();
                    e3.SetArguments(projectFolder, resourceFolder);
                    e3.Execute();
                    break;
                case "file_remover":
                    Console.WriteLine("resource folder:");
                    var resourceFolder1 = Console.ReadLine();
                    Console.WriteLine("remove file:");
                    var removeFile = Console.ReadLine();
                    var e4 = new FileReomover();
                    e4.SetArguments(removeFile, resourceFolder1);
                    e4.Execute();
                    break;
            }
        }
    }
}
