using System;
using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

public class Partical : MonoBehaviour
{
    private static Random rnd = new Random(223);
    Animator animator;
    protected Guid guid = new Guid();

    private void Start()
    {
        animator = GetComponent<Animator>();
        Vector2 vector = new Vector2(rnd.NextFloat(), rnd.NextFloat()).normalized;
        GetComponent<Rigidbody>().AddForce(vector, ForceMode.Impulse);
    }

    private void OnDestroy()
    {
        Die();
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
