using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class DefaultStats
{
    public static int DEFAULT_SPEED = 30;
    public static int DEFAULT_DAMAGE = 10;
    public static int DEFAULT_COOLDOWN = 500;

    public static float[,] DamagesScale = new float[,] { {1.0f, 1.2f, 1.1f}, //SHield damage
                                                         {1.2f, 1.0f, 1.1f} }; //Armor damage
}


