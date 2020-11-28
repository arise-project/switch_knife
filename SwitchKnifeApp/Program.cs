using SwitchKnifeApp.csv;
using SwitchKnifeApp.csv.opration;
using System;
using System.Linq;

namespace SwitchKnifeApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("map_test_to_class|csproj_find_not_present_file|file_remover|csv_splitter|csv-change|csv-validate-keys|csv-merge");
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
                case "csv_splitter":
                    Console.WriteLine("csv file:");
                    var csvFile1 = Console.ReadLine();
                    Console.WriteLine("output folder:");
                    var outputFolder1 = Console.ReadLine();
                    Console.WriteLine("limit:");
                    var limit1 = int.Parse(Console.ReadLine());
                    new CsvSplitter().Execute(csvFile1, outputFolder1, limit1);
                    break;
                case "csv-change":
                    Console.WriteLine("input folder:");
                    var inputFolder2 = Console.ReadLine();
                    Console.WriteLine("output folder:");
                    var outputFolder2 = Console.ReadLine();
                    Console.WriteLine("headers:");
                    var headers = Console.ReadLine().Split(',').ToList();
                    Console.WriteLine("move from:");
                    var moveFrom = Console.ReadLine().Split(',').Select(int.Parse);
                    Console.WriteLine("move to:");
                    var moveTo = Console.ReadLine().Split(',').Select(int.Parse).ToArray();
                    Console.WriteLine("shrink from:");
                    var shrinkFrom = int.Parse(Console.ReadLine());
                    Console.WriteLine("shrink to:");
                    var shrinkTo = int.Parse(Console.ReadLine());
                    new CsvChange().Execute(
                        inputFolder2, 
                        outputFolder2, 
                        new Change 
                        { 
                            Header = new Header 
                            { 
                                Names = headers
                            },
                            Moves = moveFrom.Select((mt, i) => new Move { FromColumn = mt, ToColumn = moveTo[i] }).ToList(),
                            Shrink = new Shrink { StartColumn = shrinkFrom, EndColumn = shrinkTo }
                        });
                    break;
                case "csv-validate-keys":
                    Console.WriteLine("input folder:");
                    var inputFolder3 = Console.ReadLine();
                    Console.WriteLine("input column:");
                    var inputColumn1 = int.Parse(Console.ReadLine());
                    Console.WriteLine("output folder:");
                    var outputFolder3 = Console.ReadLine();
                    Console.WriteLine("output column:");
                    var outputColumn1 = int.Parse(Console.ReadLine());
                    new CsvValidateKeys().Execute(inputFolder3, inputColumn1, outputFolder3, outputColumn1);
                    break;
                case "csv-merge":
                    Console.WriteLine("input folder:");
                    var inputFolder4 = Console.ReadLine();
                    Console.WriteLine("output file:");
                    var outputFolder4 = Console.ReadLine();
                    new CvsMerge().Execute(inputFolder4, outputFolder4);
                    break;
            }
        }
    }
}
