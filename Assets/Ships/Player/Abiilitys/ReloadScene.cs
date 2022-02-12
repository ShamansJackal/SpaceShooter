using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : IAbility
{
    private float cooldown = 0.5f;
    private float usageTime = 0f;

    public void Cast()
    {
        if (Time.time - usageTime <= cooldown) return;

        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        usageTime = Time.time;
    }

    public float Cooldown() => cooldown;

    public float GetCooldownPercent()
    {
        return Mathf.Clamp01((Time.time - usageTime) / cooldown);
    }
}