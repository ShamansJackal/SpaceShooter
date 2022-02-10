using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponSet : IAbility
{
    private float cooldown = 0.5f;
    private float usageTime = 0f;

    private readonly BaseWeapon[] weapons1;
    private readonly BaseWeapon[] weapons2;
    private readonly Player player;

    private bool weaponId = false;
    public ChangeWeaponSet(Player player, BaseWeapon[] weapons1, BaseWeapon[] weapons2)
    {
        this.weapons1 = weapons1;
        this.weapons2 = weapons2;
        this.player = player;
    }

    public void Cast()
    {
        if (Time.time - usageTime <= cooldown) return;

        if (weaponId) player.Ship.Weapons = weapons1;
        else player.Ship.Weapons = weapons2;

        weaponId = !weaponId;
        usageTime = Time.time;
    }

    public float GetCooldownPercent()
    {
        return Mathf.Clamp01((Time.time - usageTime) / cooldown);
    }

    public float Cooldown() => cooldown;
}
