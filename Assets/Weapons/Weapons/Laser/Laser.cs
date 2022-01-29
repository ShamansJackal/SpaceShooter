using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : BaseWeapon
{
    public override void Shot()
    {
        if (IsReady)
        {
            SpawnBullet();
            StartCoroutine(StartCooldown());
        }
    }

    protected override void SpawnBullet(Quaternion localRotation)
    {
        float ToGlobaCord = transform.localScale.magnitude / transform.lossyScale.magnitude;

        Vector3 pos = new Vector3(
            Bullet.transform.localScale.y * ToGlobaCord / 2 * Mathf.Sin(transform.localRotation.z),
            Bullet.transform.localScale.y * ToGlobaCord / 2 * Mathf.Cos(transform.localRotation.z)
        );

        var bullet = Instantiate(Bullet, gameObject.transform);
        bullet.transform.localScale *= ToGlobaCord;
        bullet.transform.localPosition = pos;
        bullet.SetUpStats(damage, bulletSpeed, targets);        
    }
}
