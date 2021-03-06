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
        string path = @"D:\level.txt";
        var lines = File.ReadAllLines(path);
        Dictionary<string, BaseShip> ships = new Dictionary<string, BaseShip>();
        Quaternion rot = Quaternion.Euler(0, 0, 180);

        foreach (var line in lines)
        {
            Match match = Regex.Match(line, @"\((?<var_name>[^,]+?)(?:,(?<ship_name>[^,]+?))?(?:,(?<x_pos>-?[\d.]+?),(?<y_pos>-?[\d.,]+?))?\)");

            string varName = match.Groups["var_name"].Value.Trim();
            string shipName = match.Groups["ship_name"].Value.Trim();
            string xStr = match.Groups["x_pos"].Value.Trim();
            string yStr = match.Groups["y_pos"].Value.Trim();

            if (string.IsNullOrWhiteSpace(line) || Regex.IsMatch(line, "//")) continue;
            else if(Regex.IsMatch(line, @"spawn\(.+?,.+?,.+?,.+?\)"))
            {
                float x = float.Parse(xStr, CultureInfo.InvariantCulture);
                float y = float.Parse(yStr, CultureInfo.InvariantCulture);

                ships[varName] = Object.Instantiate(ShipByName(shipName), new Vector3(x,y),rot);
            }
            else if(Regex.IsMatch(line, @"move\(.+,.+,.+\)"))
            {
                float x = float.Parse(xStr, CultureInfo.InvariantCulture);
                float y = float.Parse(yStr, CultureInfo.InvariantCulture);

                ships[varName].velocity = new Vector3(x,y);
            }
            else if(Regex.IsMatch(line, @"localMove\(.+,.+,.+\)"))
            {
                float x = float.Parse(xStr, CultureInfo.InvariantCulture);
                float y = float.Parse(yStr, CultureInfo.InvariantCulture);

                ships[varName].SetLocalVelocity(new Vector2(x, y));
            }
            else if(Regex.IsMatch(line, @"stop\(.+\)")){
                ships[varName].StopMoving();
            }
            else if(Regex.IsMatch(line, @"shot\(.+\)"))
            {
                ships[varName].Shot();
            }
            else if(Regex.IsMatch(line, @"wait\(.+\)"))
            {
                yield return new WaitForSeconds(float.Parse(varName, CultureInfo.InvariantCulture));
            }
            else if(Regex.IsMatch(line, @"rotation\(.+,.+\)"))
            {
                ships[varName].transform.rotation = Quaternion.Euler(0,0,float.Parse(shipName, CultureInfo.InvariantCulture));
            }
            else if(Regex.IsMatch(line, @"stopRotation\(.+\)"))
            {
                ships[varName].StopRotation();
            }
            else if(Regex.IsMatch(line, @"rotate\(.+,.+\)"))
            {
                ships[varName].angularVelocity = float.Parse(shipName, CultureInfo.InvariantCulture); ;
            }
        }
    }
}

