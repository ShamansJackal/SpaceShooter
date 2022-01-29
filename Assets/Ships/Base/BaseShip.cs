using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShip: MonoBehaviour, IDamageble
{
    protected const float timestep = 0.02f;
    protected bool IsDistored = false;
    #region Fields
    [Header("Main")]
    public UnitType type;
    public List<UnitType> Targets;
    public float Speed;
    public BaseWeapon Weapon => Weapons[0];
    public List<BaseWeapon> Weapons;

    public Rigidbody2D Body;

    public List<Sprite> ParticalSprites;
    public Partical partical;

    [Header("Colliders")]
    public CircleCollider2D ShieldCollider;
    public Collider2D BaseCollider;

    [Header("Health")]
    public int maxShield;
    public int maxHealth;
    public Shield ShieldObj;
    #endregion

    protected int _healt;
    protected int _shield;

    public int Health
    {
        get => _healt;
        set {
            if (value >= maxHealth)
                _healt = maxHealth;
            else if (value <= 0)
                _healt = Die();
            else
                _healt = value;
        }
    }

    public int Shield
    {
        get => _shield;
        set {
            if (value >= maxShield)
                _shield = maxShield;
            else if (_shield == 0)
                Health += value;
            else if (value <= 0)
                _shield = ShieldDown();
            else
                _shield = value;
        }
    }

    public bool IsShieldActive => _shield > 0;

    public virtual void Start()
    {
        for (int i = 0; i < Weapons.Count; i++)
        {
            Weapons[i] = Instantiate(Weapons[i], transform);
            Weapons[i].targets = Targets;
        }

        ShieldObj = Instantiate(ShieldObj, transform);
        ShieldUp();

        Health = maxHealth;
        Shield = maxShield;
        tag = "Ship";
    }

    public virtual int TakeDamage(int Damage, DamageType damageType)
    {
        int realDamage;
        if (IsShieldActive)
            realDamage = (int)(Damage * DefaultStats.DamagesScale[0, (int)damageType]);
        else
            realDamage = (int)(Damage * DefaultStats.DamagesScale[1, (int)damageType]);

        Shield -= realDamage;
        return realDamage;
    }

    protected int Die()
    {
        for(int i=0;i<3; i++)
        {
            var part = Instantiate(partical, transform.position, transform.rotation);
            part.GetComponent<SpriteRenderer>().sprite = ParticalSprites[0];
        }

        Destroy(gameObject);
        return 0;
    }
    
    protected virtual int ShieldDown()
    {
        ShieldObj.ShieldDown();
        BaseCollider.enabled = true;
        ShieldCollider.enabled = false;

        return 0;
    }

    protected virtual int ShieldUp()
    {
        BaseCollider.enabled = false;
        ShieldCollider.enabled = true;

        return default;
    }

    public virtual void Shot()
    {
        foreach (var weapon in Weapons)
            weapon.Shot();
    }

    private void OnDestroy()
    {
        Weapons = new List<BaseWeapon>();
        IsDistored = true;
        //StopAllCoroutines();
    }
}
