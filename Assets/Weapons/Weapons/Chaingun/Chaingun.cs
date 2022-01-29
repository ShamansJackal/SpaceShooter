using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class Chaingun : BaseWeapon
{
    private Random rnd = new Random(228);
    // Start is called before the first frame update
    protected override void Start()
    {
        function = Traectories.Direct;
        base.Start();
    }

    // Update is called once per frame
    public override void Shot()
    {
        if (IsReady)
        {
            SpawnBullet(Quaternion.Euler(0, 0, rnd.NextInt(-5, 5)));
            StartCooldown();
        }
    }
}
