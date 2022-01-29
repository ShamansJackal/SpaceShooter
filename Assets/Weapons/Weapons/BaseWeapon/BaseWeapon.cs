using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class BaseWeapon : MonoBehaviour
{
    #region Editor Fields
    public BaseBullet Bullet;
    public DamageType damageType;

    //Default weapon stats stuff
    [SerializeField]
    protected int baseDamage = DefaultStats.DEFAULT_DAMAGE;
    [SerializeField]
    protected int baseSpeed = DefaultStats.DEFAULT_SPEED;
    [SerializeField]
    //in miliseconds
    protected int baseCooldown = DefaultStats.DEFAULT_COOLDOWN;
    #endregion

    #region Scale factors
    public double CooldownFactor { set => cooldown = (int)(value * baseCooldown); }
    public float BulletsSpeedFactor { set => bulletSpeed = (int)(value * baseSpeed); }
    public float BulletsDamageFactor { set => damage = (int)(value * baseDamage); }
    #endregion

    #region Weapon stats
    protected int damage;
    protected int bulletSpeed;
    //Cooldown in tickets
    protected int cooldown;
    protected ParametricFunction function = Traectories.Sinusoid;
    //Set up's by weapen owner при получении
    public List<UnitType> targets;
    #endregion

    protected bool IsReady = true;

    private void Start()
    {
        damage = baseDamage;
        bulletSpeed = baseSpeed;
        cooldown = baseCooldown;
        damageType = Bullet.damageType;
    }

    protected IEnumerator StartCooldown()
    {
        IsReady = false;
        yield return new WaitForSeconds(cooldown*0.001f);
        IsReady = true;
    }

    public virtual void Shot()
    {
        if (IsReady)
        {
            SpawnBullet();
            SpawnBullet(Quaternion.Euler(0, -180, 0));
            //SpawnBullet(Quaternion.Euler(0, 0, -30));
            //SpawnBullet(Quaternion.Euler(0, 0, 30));
            StartCoroutine(StartCooldown());
        }
    }

    protected virtual void SpawnBullet(Quaternion localRotation)
    {
        var bullet = Instantiate(Bullet, transform.position, transform.rotation * localRotation);
        bullet.SetUpStats(damage, bulletSpeed, targets);
        bullet.SetUpTraectory(function);
    }
    
    //TODO возможно должна быть виртуальной бля и вообще это чисто обёртка
    protected void SpawnBullet()
    {
        SpawnBullet(Quaternion.identity);
    }
}
