using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    const float SPEED_SCALE = 0.001f; 

    public int damage = DefaultStats.DEFAULT_DAMAGE;
    public int speed = DefaultStats.DEFAULT_SPEED;
    public List<UnitType> targets;

    private ParametricFunction traectory = Traectories.Sinusoid;
    private Vector2 prevTraectoryValue = new Vector2(0, 0);
    private Vector2 prevMovementVector = new Vector2(0, 0);

    private Quaternion defaultRotation;
    private Rigidbody2D  body;
    protected int Ticks { get; private set; } = 0;

    public DamageType damageType = DamageType.Balistic;
    public static int count = 0;

    private void Start()
    {
        defaultRotation = transform.rotation;
        body = GetComponent<Rigidbody2D>();

        count++; 
    }

    public void SetUpStats(int damage, int speed, List<UnitType> targets)
    {
        this.damage = damage;
        this.speed = speed;
        this.targets = targets;
    }

    public void SetUpTraectory(ParametricFunction function)
    {
        traectory = function;
    }

    protected virtual Vector3 Move()
    {
        var curTraectoryValue = traectory(Ticks*SPEED_SCALE*speed);
        Vector2 vector = curTraectoryValue - prevTraectoryValue;

        //transform.rotation = defaultRotation * Quaternion.FromToRotation(Vector2.up * vector.magnitude, vector);
        transform.rotation = defaultRotation * Rotate(vector);
        vector = defaultRotation * vector;
        body.velocity = vector;

        prevMovementVector = vector;
        prevTraectoryValue = curTraectoryValue;
        return vector;
    }

    protected virtual Quaternion Rotate(Vector3 vector)
    {
        Quaternion rotationToTrajectory;
        if (vector.magnitude < Mathf.Epsilon)
        {
            rotationToTrajectory = Quaternion.identity;
        }
        else
        {
            Vector3 angFromAxis = new Vector3(0, 0, -Mathf.Asin(vector.x / vector.magnitude)) * Mathf.Rad2Deg;
            rotationToTrajectory = Quaternion.Euler(angFromAxis);
        }
        return rotationToTrajectory;
    }

    protected virtual void FixedUpdate()
    {
        transform.position += Move();
        Ticks++;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogWarning("collissd bullet");
    }

    public virtual void OnEnemyCollison()
    {

    }
}
