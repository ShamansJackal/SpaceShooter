using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class Spawner : MonoBehaviour
{
    public BaseShip ship;
    private Random rnd = new Random(228);
    void Start()
    {
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        while (true)
        {
            ship = Instantiate(ship, new Vector3(rnd.NextFloat(-7, 7), rnd.NextFloat(-4, 4)), Quaternion.identity);
            yield return new WaitForSeconds(3f);
        }
    }
}
