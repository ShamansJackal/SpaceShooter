using UnityEngine;

public static class  ShipExtension
{
    public static void StopMoving(this BaseShip ship) => ship.velocity = Vector2.zero;
    public static void SetLocalVelocity(this BaseShip ship, Vector2 velocity) => ship.velocity = ship.transform.rotation * velocity;
    public static void DefaultRotation(this BaseShip ship) => ship.transform.rotation = Quaternion.Inverse(Quaternion.identity);
    public static void StopRotation(this BaseShip ship) => ship.angularVelocity = 0f;
}
