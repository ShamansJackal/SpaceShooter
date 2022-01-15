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
    private Quaternion defaultRotation;
    protected int Ticks { get; private set; } = 0;

    public DamageType damageType = DamageType.Balistic;

    private void Start()
    {
        defaultRotation = transform.rotation;
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
        vector = defaultRotation * vector;

        prevTraectoryValue = curTraectoryValue;
        return vector;
    }

    private void FixedUpdate()
    {
        Vector3 delta = Move();
        transform.rotation *= Quaternion.FromToRotation(Vector3.zero, delta); ;
        transform.position += delta;
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
