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
            Console.WriteLine("map_test_to_class|csproj_find_not_present_file|file_remover|csv_splitter|csv-change|csv-validate-keys|csv-merge|char-case|lsh-serach|resx-key|en-words|resx-patch|resx-format|resx-dots|resx-excepts|restore-except|csv-add-col|resx-repeat-seq|css-selectors");
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
                    var outputFile4 = Console.ReadLine();
                    new CvsMerge().Execute(inputFolder4, outputFile4);
                    break;
                case "char-case":
                    Console.WriteLine("input file:");
                    var inputFile5 = Console.ReadLine();
                    Console.WriteLine("output file:");
                    var outputFile5 = Console.ReadLine();
                    new CharCase().Execute(inputFile5, outputFile5);
                    break;
                case "lsh-serach":
                    Console.WriteLine("file 1:");
                    var file6_1 = Console.ReadLine();
                    Console.WriteLine("file 2:");
                    var file6_2 = Console.ReadLine();
                    new LSHFileCompare().Execute(file6_1, file6_2);
                    break;
                case "resx-key":
                    Console.WriteLine("input folder:");
                    var inputFolder7 = Console.ReadLine();
                    Console.WriteLine("search file:");
                    var file7 = Console.ReadLine();
                    new FindResxKey().Execute(inputFolder7, file7);
                    break;
                case "en-words":
                    Console.WriteLine("input folder:");
                    var inputFolder8 = Console.ReadLine();
                    Console.WriteLine("output file:");
                    var file8 = Console.ReadLine();
                    new EnglishWords().Execute(inputFolder8, file8);
                    break;
                case "resx-patch":
                    Console.WriteLine("resx file:");
                    var resxFile9 = Console.ReadLine();
                    Console.WriteLine("path file:");
                    var pathFile9 = Console.ReadLine();
                    new PatchResx().Execute(resxFile9, pathFile9);
                    break;
                case "resx-format":
                    Console.WriteLine("input file:");
                    var inputFile = Console.ReadLine();
                    Console.WriteLine("compare folder:");
                    var compareFolder = Console.ReadLine();
                    Console.WriteLine("output file:");
                    var outputFile = Console.ReadLine();
                    new ResxStringFormat().Execute(inputFile, compareFolder, outputFile);
                    break;
                case "resx-dots":
                    Console.WriteLine("original file:");
                    var originalFile = Console.ReadLine();
                    Console.WriteLine("locale file:");
                    var localeFile = Console.ReadLine();
                    new ResxDots().Execute(originalFile, localeFile);
                    break;
                case "resx-excepts":
                    Console.WriteLine("original file:");
                    var originalFile1 = Console.ReadLine();
                    Console.WriteLine("locale file:");
                    var localeFile1 = Console.ReadLine();
                    Console.WriteLine("except file:");
                    var exceptFile1 = Console.ReadLine();
                    new ResxExcepts().Execute(originalFile1, localeFile1, exceptFile1);
                    break;
                case "restore-except":
                    Console.WriteLine("file:");
                    var file12 = Console.ReadLine();
                    Console.WriteLine("except file:");
                    var exceptFile2 = Console.ReadLine();
                    new RestoreExcepts().Execute(file12, exceptFile2);
                    break;
                case "csv-add-col":
                    Console.WriteLine("csv file:");
                    var csvFile = Console.ReadLine();
                    Console.WriteLine("column file:");
                    var columnFile = Console.ReadLine();
                    new CsvAddCol().Execute(csvFile, columnFile);
                    break;
                case "resx-repeat-seq":
                    Console.WriteLine("resource file:");
                    var resourceFile1 = Console.ReadLine();
                    Console.WriteLine("sequence length:");
                    var seqLength = int.Parse(Console.ReadLine());
                    Console.WriteLine("output file:");
                    var outputFile1 = Console.ReadLine();
                    new ResxRepeatedChars().Execute(resourceFile1, seqLength, outputFile1);
                    break;
                case "css-selectors":
                    Console.WriteLine("html file:");
                    var htmlFile1 = Console.ReadLine();
                    Console.WriteLine("options:");
                    var options1 = Console.ReadLine();
                    new CssSelectors().Execute(htmlFile1, options1);
                    break;
            }
        }
    }
}
