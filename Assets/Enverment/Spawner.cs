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
            var shiiiit =Instantiate(ship, new Vector3(rnd.NextFloat(-7, 7), 5.2f), Quaternion.Euler(0,0,180));
            shiiiit.Body.velocity = new Vector2(0, -0.5f);
            yield return new WaitForSeconds(3f);
        }
    }
}
