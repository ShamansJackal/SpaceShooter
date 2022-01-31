using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    const float SPEED_SCALE = 0.001f; 

    public int damage = DefaultStats.DefaultDamage;
    public int speed = DefaultStats.DefaultSpeed;
    public List<UnitType> targets;
    public Rigidbody2D body;
    public Animator animator;
    public TrailRenderer trail;

    private ParametricFunction traectory = Traectories.Sinusoid;
    private Vector2 prevTraectoryValue = new Vector2(0, 0);
    private Vector2 prevMovementVector = new Vector2(0, 0);

    private Quaternion defaultRotation;
    private byte stoper = 1;
    protected int Ticks { get; private set; } = 0;

    public DamageType damageType = DamageType.Balistic;
    public static int count = 0;

    protected virtual void Start()
    {
        defaultRotation = transform.rotation;
        body = body != null ? body : GetComponent<Rigidbody2D>();
        animator = animator != null ? animator : GetComponent<Animator>();

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

        transform.rotation = defaultRotation * Quaternion.FromToRotation(Vector2.up * vector.magnitude, vector * stoper);
        //transform.rotation = defaultRotation * Rotate(vector);
        //transform.rotation = Rotate(vector);
        vector = defaultRotation * vector;
        body.velocity = 10 * stoper * vector;

        prevMovementVector = vector;
        prevTraectoryValue = curTraectoryValue;
        return vector;
    }

    protected virtual float VectorToAngle(Vector3 vector)
    {
        if (vector.magnitude < Mathf.Epsilon) return 0f;
        return vector.y > 0 ? -Mathf.Asin(vector.x / vector.magnitude) * Mathf.Rad2Deg : 180f + Mathf.Asin(vector.x / vector.magnitude)*Mathf.Rad2Deg;
    }

    protected virtual Quaternion Rotate(Vector3 vector)
    {
        return Quaternion.Euler(0, 0, defaultRotation.eulerAngles.z + VectorToAngle(vector) * stoper);
    }

    protected virtual void FixedUpdate()
    {
        Move();
        Ticks++;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ship"))
        {
            var ship = collision.gameObject.GetComponent<BaseShip>();
            if (targets.Contains(ship.type)) OnEnemyCollison(ship);
        }

    }

    public virtual void OnEnemyCollison(BaseShip ship)
    {
        trail.enabled = false;
        stoper = 0;
        transform.parent = ship.transform;
        animator.SetTrigger("Explose");
        ship.TakeDamage(damage, damageType);

        Destroy(gameObject, 0.7f);
    }
}
