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
            transform.position += Vector3.up * 0.01f;
        if (Input.GetKey(KeyCode.S))
            transform.position += Vector3.down * 0.01f;
        if (Input.GetKey(KeyCode.D))
            transform.rotation *= Quaternion.AngleAxis(0.1f, Vector3.forward);
        if (Input.GetKey(KeyCode.A))
            transform.rotation *= Quaternion.AngleAxis(0.1f, Vector3.back);

    }
}
