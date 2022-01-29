using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelHelper;

public class Level00: MonoBehaviour, ILevel
{
    public IEnumerator StartLevel()
    {
        var ShipL2 = Instantiate(ShipByName("BaseShip"), new Vector2(6f, 5.5f), Quaternion.Euler(0, 0, 180));
        var ShipR2 = Instantiate(ShipByName("BaseShip"), new Vector2(-6, 5.5f), Quaternion.Euler(0, 0, 180));
        var ShipL1 = Instantiate(ShipByName("BaseShip"), new Vector2(3f, 5.5f), Quaternion.Euler(0, 0, 180));
        var ShipR1 = Instantiate(ShipByName("BaseShip"), new Vector2(-3, 5.5f), Quaternion.Euler(0, 0, 180));
        var ShipC0 = Instantiate(ShipByName("BaseShip"), new Vector2(0f, 5.5f), Quaternion.Euler(0, 0, 180));


        //StartCoroutine(ShipL1.MoveVertical(-5, 2f));
        //StartCoroutine(ShipR2.MoveVertical(-5, 2f));
        //StartCoroutine(ShipR1.MoveVertical(-5, 2f));
        //StartCoroutine(ShipC0.MoveVertical(-5, 2f));
        //yield return StartCoroutine(ShipL2.MoveVertical(-5, 2f));
        //yield return new WaitUntil(2.4f);

        ShipC0.Body.velocity = new Vector2(0, -2.75f);
        ShipL2.Body.velocity = new Vector2(0, -2.75f);
        ShipR2.Body.velocity = new Vector2(0, -2.75f);
        ShipL1.Body.velocity = new Vector2(0, -2.75f);
        ShipR1.Body.velocity = new Vector2(0, -2.75f);
        yield return new WaitForSeconds(2f);
        ShipC0.Body.velocity *= 0;
        ShipL2.Body.velocity *= 0;
        ShipR2.Body.velocity *= 0;
        ShipL1.Body.velocity *= 0;
        ShipR1.Body.velocity *= 0;

        ShipL2.Shot();
        ShipR2.Shot();
        yield return new WaitForSeconds(0.5f);

        ShipL1.Shot();
        ShipR1.Shot();
        yield return new WaitForSeconds(0.5f);

        ShipC0.Shot();
        yield return new WaitForSeconds(0.5f);

       // StartCoroutine(ShipC0.MoveVertical(6.2f, 2f));
        yield return new WaitForSeconds(1.5f);
        Debug.Log("wait");
        yield return new WaitForSeconds(0.5f);
        Debug.Log("fin");

        //ShipL2.MoveHorizontal(5f, 2f);
        //ShipR2.MoveHorizontal(-5f, 2f);
        //yield return new WaitForSeconds(1.5f);

        ////ShipL1(new Vector2(6, 6.5f), 2f);
        ////ShipR1.MoveToPoint(new Vector2(-6, 6.5f), 2f);
        //yield return new WaitForSeconds(1.5f);
    }
}
