using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BaseWeapon weapon;
    private float SpeedScale = 0.08f;

    private void Start()
    {
        weapon = Instantiate(weapon, transform);
        weapon.targets = new List<UnitType>() { UnitType.Enemy };
    }

    private void OnMouseDown()
    {
        weapon.Shot();
    }

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space)) weapon.Shot();
        if (Input.GetKey(KeyCode.Escape)) Application.Quit();

        if (Input.GetKey(KeyCode.D)) transform.position += Vector3.right * SpeedScale;
        if (Input.GetKey(KeyCode.A)) transform.position += Vector3.left * SpeedScale;
        if (Input.GetKey(KeyCode.S)) transform.position += Vector3.down * SpeedScale;
        if (Input.GetKey(KeyCode.W)) transform.position += Vector3.up * SpeedScale;

        if (Input.GetKey(KeyCode.LeftArrow)) transform.rotation *= Quaternion.AngleAxis(1f, Vector3.forward);
        if (Input.GetKey(KeyCode.RightArrow)) transform.rotation *= Quaternion.AngleAxis(1f, Vector3.back);

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
}
