using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour
{
    public AbilityButtonScritebleObj ability;
    public Image spriteRenderer;

    private KeyCode hotKeyCode;
    private string abilityName;

    private void Start()
    {
        spriteRenderer.sprite = ability.Icon;
        hotKeyCode = ability.HotKeyCode;
        abilityName = ability.AbilityName;
    }

    private void Update()
    {
        if (Input.GetKeyDown(hotKeyCode)) Player.instance.Abilitys[abilityName].Cast();

        spriteRenderer.color = new Color(1, 0, Player.instance.Abilitys[abilityName].GetCooldownPercent(), Player.instance.Abilitys[abilityName].GetCooldownPercent());
    }
}
