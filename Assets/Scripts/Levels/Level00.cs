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
        List<BaseShip> ships = new List<BaseShip> { ShipL2, ShipL1, ShipC0, ShipR1, ShipR2 };

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

        yield return new WaitUntil(() => ships.TrueForAll(x => x.IsDestroyed));

        while (true)
        {
            var Asteroid = Object.Instantiate(ShipByName("BaseShip"), new Vector3(Random.Range(-6.5f, 6.5f), 5.5f), Quaternion.Euler(0, 0, 180));
            //Asteroid.Body.angularVelocity = Random.Range(-60, 60);
            Asteroid.velocity = new Vector2(0, -3f);
            yield return new WaitForSeconds(0.8f);
        }

        //ShipL2.MoveHorizontal(5f, 2f);
        //ShipR2.MoveHorizontal(-5f, 2f);
        //yield return new WaitForSeconds(1.5f);

        ////ShipL1(new Vector2(6, 6.5f), 2f);
        ////ShipR1.MoveToPoint(new Vector2(-6, 6.5f), 2f);
        //yield return new WaitForSeconds(1.5f);
    }
}
