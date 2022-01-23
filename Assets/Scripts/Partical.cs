using System;
using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

public class Partical : MonoBehaviour
{
    private static Random rnd = new Random(223);
    public Animator animator;
    public Rigidbody2D body;
    protected Guid guid = new Guid();

    private void Start()
    {
        animator = animator != null ? animator : GetComponent<Animator>();
        body = body != null ? body : GetComponent<Rigidbody2D>();
        Vector2 vector = new Vector2(rnd.NextFloat(), rnd.NextFloat()).normalized;
        body.velocity = vector;
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
