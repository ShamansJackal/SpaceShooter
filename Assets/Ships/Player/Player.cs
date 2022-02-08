﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Dictionary<string, IAbility> Abilitys = new Dictionary<string, IAbility>();

    public BaseShip Ship;

    public List<BaseWeapon> weapons1;
    public List<BaseWeapon> weapons2;
    private bool _aoeWeapon;

    private float SpeedScale = 0.08f;
    private bool ControleAllowed = true;

    public void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Ship.Weapons = weapons1;
        _aoeWeapon = false;

        Abilitys["SwapWeapons"] = new ChangeWeaponSet(this, weapons1, weapons2);
    }

    private void OnMouseDown()
    {
        Ship.Shot();
    }

    public void FixedUpdate()
    {
        var SpeedScale = Ship.Speed;
        if (Input.GetKey(KeyCode.Space)) Ship.Shot();

        if (!ControleAllowed) return;
        if (Input.GetKey(KeyCode.Escape)) Application.Quit();

        if (Input.GetKey(KeyCode.D)) transform.position += Vector3.right * SpeedScale;
        if (Input.GetKey(KeyCode.A)) transform.position += Vector3.left * SpeedScale;
        if (Input.GetKey(KeyCode.S)) transform.position += Vector3.down * SpeedScale;
        if (Input.GetKey(KeyCode.W)) transform.position += Vector3.up * SpeedScale;

        if (Input.GetKey(KeyCode.LeftArrow)) transform.rotation *= Quaternion.AngleAxis(3f, Vector3.forward);
        if (Input.GetKey(KeyCode.RightArrow)) transform.rotation *= Quaternion.AngleAxis(3f, Vector3.back);

        //ProffilngBullets();
    }

    private static void ProffilngBullets()
    {
        if (1.0f / Time.deltaTime > 60)
            Debug.Log($"{BaseBullet.count} time:{1.0f / Time.deltaTime}");
        if (1.0f / Time.deltaTime < 40)
            Debug.LogWarning($"{BaseBullet.count} time:{1.0f / Time.deltaTime}");
        if (1.0f / Time.deltaTime < 24)
            Debug.LogError($"{BaseBullet.count} time:{1.0f / Time.deltaTime}");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ship") && collision.gameObject.TryGetComponent(out BaseShip ship))
        {
            PushBack(collision.gameObject);
        }
            //OnEnemyCollison(ship);
    }

    private void PushBack(GameObject gameObject)
    {
        Vector2 velocity = transform.position - gameObject.transform.position;
        StartCoroutine(MoveBack(velocity.normalized*20));
        StartCoroutine(Invicteble());
    }

    IEnumerator MoveBack(Vector2 velocity)
    {
        Ship.Body.velocity = velocity;
        ControleAllowed = false;
        yield return new WaitForSeconds(0.1f);
        Ship.Body.velocity = Vector2.zero;
        ControleAllowed = true;
    }

    IEnumerator Invicteble()
    {
        Ship.BaseCollider.enabled = false;
        Ship.ShieldCollider.enabled = false;
        Ship.animator.SetBool("Invicteble", true);
        yield return new WaitForSeconds(0.5f);

        if(Ship.IsShieldActive) Ship.ShieldCollider.enabled = true;
        else Ship.BaseCollider.enabled = true;
        Ship.animator.SetBool("Invicteble", false);
    }
}
