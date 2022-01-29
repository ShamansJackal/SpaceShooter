using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LevelHelper;

public class Level00: ILevel
{
    public IEnumerator StartLevel()
    {
        var ShipL2 = Object.Instantiate(ShipByName("BaseShip"), new Vector2(6f, 5.5f), Quaternion.Euler(0, 0, 180));
        var ShipR2 = Object.Instantiate(ShipByName("BaseShip"), new Vector2(-6, 5.5f), Quaternion.Euler(0, 0, 180));
        var ShipL1 = Object.Instantiate(ShipByName("BaseShip"), new Vector2(3f, 5.5f), Quaternion.Euler(0, 0, 180));
        var ShipR1 = Object.Instantiate(ShipByName("BaseShip"), new Vector2(-3, 5.5f), Quaternion.Euler(0, 0, 180));
        var ShipC0 = Object.Instantiate(ShipByName("BaseShip"), new Vector2(0f, 5.5f), Quaternion.Euler(0, 0, 180));

        ShipC0.velocity = new Vector2(0, -2.75f);
        ShipL2.velocity = new Vector2(0, -2.75f);
        ShipR2.velocity = new Vector2(0, -2.75f);
        ShipL1.velocity = new Vector2(0, -2.75f);
        ShipR1.velocity = new Vector2(0, -2.75f);

        yield return new WaitForSeconds(2f);

        ShipC0.StopMoving();
        ShipL2.StopMoving();
        ShipR2.StopMoving();
        ShipL1.StopMoving();
        ShipR1.StopMoving();

        ShipL2.Shot();
        ShipR2.Shot();
        yield return new WaitForSeconds(0.5f);

        ShipL1.Shot();
        ShipR1.Shot();
        yield return new WaitForSeconds(0.5f);

        ShipC0.Shot();
        yield return new WaitForSeconds(0.5f);

        yield return new WaitUntil(() => ShipC0.IsDestroyed);
        Debug.Log("fin");

        //ShipL2.MoveHorizontal(5f, 2f);
        //ShipR2.MoveHorizontal(-5f, 2f);
        //yield return new WaitForSeconds(1.5f);

        ////ShipL1(new Vector2(6, 6.5f), 2f);
        ////ShipR1.MoveToPoint(new Vector2(-6, 6.5f), 2f);
        //yield return new WaitForSeconds(1.5f);
    }
}
