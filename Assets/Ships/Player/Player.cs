using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public BaseWeapon weapon;

    private void Start()
    {
        weapon = Instantiate(weapon, transform);
        weapon.targets = new List<UnitType>() { UnitType.Enemy };
    }

    private void OnMouseDown()
    {
        weapon.Shot();
    }

    public void Update()
    {

        if (Input.GetKey(KeyCode.Space))
            weapon.Shot();
        if (Input.GetKey(KeyCode.W))
            transform.position += Vector3.up * 0.02f;
        if (Input.GetKey(KeyCode.S))
            transform.position += Vector3.down * 0.02f;
        if (Input.GetKey(KeyCode.D))
            transform.rotation *= Quaternion.AngleAxis(0.4f, Vector3.forward);
        else if (Input.GetKey(KeyCode.A))
            transform.rotation *= Quaternion.AngleAxis(0.4f, Vector3.back);
        else transform.position += Vector3.right * (0.02f * Input.GetAxis("Horizontal"));
        //ProffilngBullets();

    }

    private static void ProffilngBullets()
    {
        if (1.0f / Time.deltaTime > 120)
            Debug.Log($"{BaseBullet.count} time:{1.0f / Time.deltaTime}");
        if (1.0f / Time.deltaTime < 120)
            Debug.LogWarning($"{BaseBullet.count} time:{1.0f / Time.deltaTime}");
        if (1.0f / Time.deltaTime < 60)
            Debug.LogError($"{BaseBullet.count} time:{1.0f / Time.deltaTime}");
    }
}
