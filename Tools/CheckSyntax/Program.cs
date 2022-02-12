using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace CheckSyntax
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;

            if (args.Length > 0) path = @"..\level.txt";
            else path = args[0];

            var lines = File.ReadAllLines(path);
            Dictionary<string, int> ships = new Dictionary<string, int>();

            for(int i = 0; i<lines.Length; i++)
            {
                string line = lines[i];

                Match match = Regex.Match(line, @"\((?<var_name>[^,]+?)(?:,(?<ship_name>[^,]+?))?(?:,(?<x_pos>-?[\d.]+?),(?<y_pos>-?[\d.,]+?))?\)");

                string varName = match.Groups["var_name"].Value.Trim();
                string shipName = match.Groups["ship_name"].Value.Trim();
                string xStr = match.Groups["x_pos"].Value.Trim();
                string yStr = match.Groups["y_pos"].Value.Trim();

                try
                {
                    if (string.IsNullOrWhiteSpace(line) || Regex.IsMatch(line, "//")) continue;
                    else if (Regex.IsMatch(line, @"spawn\(.+?,.+?,.+?,.+?\)"))
                    {
                        float x = float.Parse(xStr, CultureInfo.InvariantCulture);
                        float y = float.Parse(yStr, CultureInfo.InvariantCulture);

                        ships[varName] = 0;
                    }
                    else if (Regex.IsMatch(line, @"move\(.+,.+,.+\)"))
                    {
                        float x = float.Parse(xStr, CultureInfo.InvariantCulture);
                        float y = float.Parse(yStr, CultureInfo.InvariantCulture);

                        if (!ships.ContainsKey(varName)) throw new KeyNotFoundException();
                    }
                    else if (Regex.IsMatch(line, @"localMove\(.+,.+,.+\)"))
                    {
                        float x = float.Parse(xStr, CultureInfo.InvariantCulture);
                        float y = float.Parse(yStr, CultureInfo.InvariantCulture);

                        if (!ships.ContainsKey(varName)) throw new KeyNotFoundException();
                    }
                    else if (Regex.IsMatch(line, @"stop\(.+\)"))
                    {
                        if (!ships.ContainsKey(varName)) throw new KeyNotFoundException();
                    }
                    else if (Regex.IsMatch(line, @"shot\(.+\)"))
                    {
                        if (!ships.ContainsKey(varName)) throw new KeyNotFoundException();
                    }
                    else if (Regex.IsMatch(line, @"wait\(.+\)"))
                    {
                        var x = float.Parse(varName, CultureInfo.InvariantCulture);
                    }
                    else if (Regex.IsMatch(line, @"rotation\(.+,.+\)"))
                    {
                        var x = float.Parse(shipName, CultureInfo.InvariantCulture);
                        if (!ships.ContainsKey(varName)) throw new KeyNotFoundException();
                    }
                    else if (Regex.IsMatch(line, @"stopRotation\(.+\)"))
                    {
                        if (!ships.ContainsKey(varName)) throw new KeyNotFoundException();
                    }
                    else if (Regex.IsMatch(line, @"rotate\(.+,.+\)"))
                    {
                        var y = float.Parse(shipName, CultureInfo.InvariantCulture);
                        if (!ships.ContainsKey(varName)) throw new KeyNotFoundException();
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                catch(KeyNotFoundException e)
                {
                    Console.WriteLine($"Line {i}: No such переменой, line");
                }
                catch
                {
                    Console.WriteLine($"Line {i}: Syntax error, statement ignored");
                }
            }
        }
    }
}
