using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    public float GetCooldownPercent();

    public void Cast();

    public float Cooldown();
}
