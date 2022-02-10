using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;


//Представляет производную параметрической функции
public delegate Vector2 ParametricFunction(float t);

public static class Traectories
{
    public static ParametricFunction Direct;
    public static ParametricFunction Sinusoid;
    public static ParametricFunction Spiral;

    static Traectories()
    {
        Direct = (float t) =>
        {
            float x = 0;
            float y = t*2;
            return new Vector2(x, y);
        };

        Sinusoid = (float t) =>
        {
            float x = (float)(Math.Sin(t)*5);
            float y = t*3f;
            return new Vector2(x, y);
        };

        Spiral = (float t) =>
        {
            float x = (float)(Math.Cos(t*0.2) * (4*Math.Sqrt(t*0.9)));
            float y = (float)(Math.Sin(t*0.2) * (4*Math.Sqrt(t*0.9)));
            return new Vector2(x, y);
        };

    }
}
