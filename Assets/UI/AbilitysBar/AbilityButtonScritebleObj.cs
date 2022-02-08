using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buttons", fileName = "New Ability button")]
public class AbilityButtonScritebleObj : ScriptableObject
{
    public Sprite Icon;
    public string AbilityName;
    public KeyCode HotKeyCode;
}
