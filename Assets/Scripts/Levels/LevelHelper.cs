using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHelper : MonoBehaviour
{
    public List<BaseShip> Mobs;
    public ILevel level;

    private void Start()
    {
        StartCoroutine(level.StartLevel());
    }

    public static

}

