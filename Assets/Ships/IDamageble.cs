using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageble
{
    public int TakeDamage(int Damage, DamageType damageType);
}
