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
    //div by 20 for convert to ticks
    public double CooldownFactor { set => cooldown = (int)(value * baseCooldown / 20); }
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
    [NonSerialized]
    public List<UnitType> targets;
    #endregion

    private int ticks = 0;
    private int curTick = 0;

    protected bool IsReady => ticks - curTick >= cooldown;

    private void Start()
    {
        damage = baseDamage;
        bulletSpeed = baseSpeed;
        cooldown = baseCooldown / 20;
        damageType = Bullet.damageType;

        //Чтобы можно было сделать выстрел при получении оружия
        curTick -= cooldown;
    }

    protected void StartCooldown()
    {
        curTick = ticks;
    }

    public virtual void Shot()
    {
        if (IsReady)
        {
            SpawnBullet();
            SpawnBullet(Quaternion.Euler(0, 180, 0));
            StartCooldown();
        }
    }

    protected virtual void SpawnBullet(Quaternion rotation = new Quaternion())
    {
        var bullet = Instantiate(Bullet, transform.position, transform.rotation * rotation);
        bullet.SetUpStats(damage, bulletSpeed, targets);
        bullet.SetUpTraectory(function);
    }

    private void FixedUpdate()
    {
        ticks++;
    }
}
