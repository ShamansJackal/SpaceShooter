using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using UnityEngine;
using static LevelHelper;
using System.Globalization;

public class LevelFromFile : ILevel
{
    public IEnumerator StartLevel()
    {
        string path = @"C:\Users\water\Desktop\level.txt";
        var lines = File.ReadAllLines(path);
        Dictionary<string, BaseShip> ship = new Dictionary<string, BaseShip>();
        Quaternion rot = Quaternion.Euler(0, 0, 180);

        foreach (var line in lines)
        {
            Match match = Regex.Match(line, @"\((?<var_name>[^,]+?)(?:,(?<ship_name>[^,]+?))?(?:,(?<x_pos>-?[\d.]+?),(?<y_pos>-?[\d.,]+?))?\)"); 
            if(Regex.IsMatch(line, @"spawn\(.+?,.+?,.+?,.+?\)"))
            {
                float x = float.Parse(match.Groups["x_pos"].Value, CultureInfo.InvariantCulture);
                float y = float.Parse(match.Groups["y_pos"].Value, CultureInfo.InvariantCulture);
                ship[match.Groups["var_name"].Value] = Object.Instantiate(ShipByName(match.Groups["ship_name"].Value), new Vector3(x,y),rot);
            }
            else if(Regex.IsMatch(line, @"move\(.+,.+,.+\)"))
            {
                float x = float.Parse(match.Groups["x_pos"].Value, CultureInfo.InvariantCulture);
                float y = float.Parse(match.Groups["y_pos"].Value, CultureInfo.InvariantCulture);
                ship[match.Groups["var_name"].Value].velocity = new Vector3(x,y);
            }
            else if(Regex.IsMatch(line, @"stop\(.+\)")){
                ship[match.Groups["var_name"].Value].StopMoving();
            }
            else if(Regex.IsMatch(line, @"shot\(.+\)"))
            {
                ship[match.Groups["var_name"].Value].Shot();
            }
            else if(Regex.IsMatch(line, @"wait\(.+\)"))
            {
                yield return new WaitForSeconds(float.Parse(match.Groups["var_name"].Value, CultureInfo.InvariantCulture));
            }
        }
    }
}

