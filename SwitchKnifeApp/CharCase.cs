using System.Collections.Generic;
using System.IO;

namespace SwitchKnifeApp
{
    public class CharCase
    {
        public void Execute(string inputFile, string outputFile)
        {
            var input = File.ReadAllLines(inputFile);

            var result = new List<string>();
            foreach (var line in input)
            {
                bool isWord = false;
                int start = -1;
                int end = -1;
                int index = 0;
                string resultLine = line;
                foreach (var c in line)
                {   
                    if ((c >= 'A' && c <= 'Z'))
                    {
                        if (start >= 0)
                        {
                            isWord = true;
                        }

                        if (!isWord)
                        {
                            start = index;
                        }
                        end = index;
                    }
                    else
                    {
                        if(isWord)
                        {
                            resultLine = resultLine.Replace(resultLine.Substring(start, end - start + 1), resultLine.Substring(start, 1) + resultLine.Substring(start + 1, end - start + 1).ToLower());
                        }
                        isWord = false;
                        start = -1;
                        end = -1;
                    }
                    index++;
                }
                result.Add(resultLine);
            }

            File.WriteAllLines(outputFile, result);
        }
    }
}
