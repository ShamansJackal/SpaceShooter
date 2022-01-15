using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Threading.Tasks;


//Представляет производную параметрической функции
public delegate Vector2 ParametricFunction(float t);

public static class Traectories
{
    private readonly static float SPEED_SCALE = 0.001f;

    public static ParametricFunction Direct;
    public static ParametricFunction Sinusoid;
    public static ParametricFunction Spiral;

    static Traectories()
    {
        Direct = (float t) =>
        {
            float x = 0;
            float y = t;
            return new Vector2(x, y);
        };

        Sinusoid = (float t) =>
        {
            float x = (float)(Math.Sin(t));
            float y = t;
            return new Vector2(x, y);
        };

        Spiral = (float t) =>
        {
            float x = (float)(Math.Cos(t * 0.1) * (t * 0.1));
            float y = (float)(Math.Sin(t * 0.1) * (t * 0.1));
            return new Vector2(x, y);
        };

    }
}
