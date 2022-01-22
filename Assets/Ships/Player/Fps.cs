using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fps : MonoBehaviour
{
    public Text text;
    public void Start()
    {
        StartCoroutine(fps());
    }

    public void Update()
    {
        var fps = 1 / Time.deltaTime;
        if (fps > 120f) text.color = Color.white;
        else if (fps > 60f) text.color = Color.yellow;
        else text.color = Color.red;
        text.text = $"Bullets: {BaseBullet.count}; fps: {fps:F2}";
    }
    IEnumerator fps()
    {
        while (true)
        {
            var fps = 1 / Time.deltaTime;
            if (fps > 40f) text.color = Color.white;
            else if (fps > 24f) text.color = Color.yellow;
            else text.color = Color.red;
            text.text = $"Bullets: {BaseBullet.count}; fps: {fps:F2}";
            yield return new WaitForSeconds(0.1f);
        }
    }
}
