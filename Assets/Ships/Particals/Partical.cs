using System;
using UnityEngine;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

public class Partical : MonoBehaviour
{
    private static Random rnd = new Random(228);
    public Animator animator;
    public Rigidbody2D body;
    protected Guid guid = new Guid();

    private void Start()
    {
        animator = animator != null ? animator : GetComponent<Animator>();
        body = body != null ? body : GetComponent<Rigidbody2D>();

        Vector2 vector = new Vector2(rnd.NextFloat(), rnd.NextFloat()).normalized;
        Vector3 rotation = new Vector3(0, 0, rnd.NextFloat()) * 360;

        transform.rotation = Quaternion.Euler(rotation);
        body.velocity = vector;
        body.angularVelocity = rnd.NextFloat() * 10;
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
