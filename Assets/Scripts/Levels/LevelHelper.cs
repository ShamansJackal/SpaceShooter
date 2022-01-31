using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelHelper : MonoBehaviour
{
    public static List<BaseShip> Mobs;
    public List<BaseShip> MobsSSS;
    public static BaseShip ShipByName(string name) => Mobs.Single(x => x.name == name);
    public ILevel level;

    private void Start()
    {
        Mobs = MobsSSS;
        level = new Level99();
        StartCoroutine(level.StartLevel());
    }

}

