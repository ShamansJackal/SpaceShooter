using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : BaseBullet
{
    protected override void Start()
    {
        StartCoroutine(LifeTimer());
    }

    IEnumerator LifeTimer()
    {
        yield return new WaitForSeconds(0.01f);
        yield return new WaitForSeconds(speed*0.001f);
        animator.SetTrigger("FadeOut");
        damage >>= 1;
        yield return new WaitForSeconds(0.25f);
        damage >>= 1;
        yield return new WaitForSeconds(0.25f);
        damage = 0;
        Destroy(gameObject, 0.5f);
    }

    protected override void FixedUpdate(){}

    protected override void OnCollisionEnter2D(Collision2D collision){}

    protected void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ship") && collision.gameObject.TryGetComponent(out BaseShip ship) && targets.Contains(ship.type))
            OnEnemyCollison(ship);
    }

    public override void OnEnemyCollison(BaseShip ship)
    {
        ship.TakeDamage(damage, damageType);
    }
}
