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
    public Rigidbody2D body;
    public Animator animator;

    private ParametricFunction traectory = Traectories.Sinusoid;
    private Vector2 prevTraectoryValue = new Vector2(0, 0);
    private Vector2 prevMovementVector = new Vector2(0, 0);

    private Quaternion defaultRotation;
    private byte stoper = 1;
    protected int Ticks { get; private set; } = 0;

    public DamageType damageType = DamageType.Balistic;
    public static int count = 0;

    private void Start()
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

        //transform.rotation = defaultRotation * Quaternion.FromToRotation(Vector2.up * vector.magnitude, vector * stoper);
        //transform.rotation = defaultRotation * Rotate(vector);
        transform.rotation = Rotate(vector);
        vector = defaultRotation * vector;
        body.velocity = vector * 10 * stoper;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ship" &&
            collision.gameObject.TryGetComponent(out BaseShip ship) && targets.Contains(ship.type))
            OnEnemyCollison(ship);
    }

    public virtual void OnEnemyCollison(BaseShip ship)
    {
        ship.TakeDamage(damage, damageType);
        stoper = 0;
        animator.SetTrigger("Explose");
        Destroy(gameObject, 1f);
    }
}
