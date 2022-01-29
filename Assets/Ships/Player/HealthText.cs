using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthText : MonoBehaviour
{
    BaseShip player;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().Ship;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        text.text = $"Health: {player.Health} Shield: {player.Shield}";
    }
}
