using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShip: MonoBehaviour, IDamageble
{
    [SerializeField]
    protected UnitType type;

    protected int _healt;
    protected int _shield;
    protected int _maxShield;
    protected int _maxHealt;
    public int Health
    {
        get => _healt;
        set {
            if (value >= _maxHealt)
                _healt = _maxHealt;
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
            if (_shield == 0)
                Health -= value;
            else if (value <= 0)
                _shield = ShieldDown();
            else if (value >= _maxShield)
                _shield = _maxShield;
            else
                _shield = value;
        }
    }

    public virtual void Start()
    {
        Health = _maxHealt;
        Shield = _maxShield;
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
        Destroy(this);
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
