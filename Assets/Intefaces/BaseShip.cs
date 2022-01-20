using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShip: MonoBehaviour, IDamageble
{
    public UnitType type;

    public List<Sprite> sprites;
    public Partical partical;

    protected int _healt;
    protected int _shield;

    public int maxShield;
    public int maxHealth;
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
                Health -= value;
            else if (value <= 0)
                _shield = ShieldDown();
            else
                _shield = value;
        }
    }

    public virtual void Start()
    {
        Health = maxHealth;
        Shield = maxShield;
    }

    public virtual int TakeDamage(int Damage, DamageType damageType)
    {
        if (Shield > 0)
        {
            var realDamage = (int)(Damage * DefaultStats.DamagesScale[0, (int)damageType]);
            Shield -= realDamage;
            return realDamage;
        }
        else
        {
            var realDamage = (int)(Damage * DefaultStats.DamagesScale[1, (int)damageType]);
            Health -= realDamage;
            return realDamage;
        }
    }

    protected int Die()
    {
        for(int i=0;i<3; i++)
        {
            var part = Instantiate(partical, transform.position, transform.rotation);
            part.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }

        Destroy(gameObject);
        return 0;
    }
    
    protected virtual int ShieldDown()
    {
        return 0;
    }

    private void OnDestroy()
    {
        
    }
}
