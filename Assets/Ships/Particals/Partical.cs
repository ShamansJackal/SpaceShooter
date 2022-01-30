using UnityEngine;

public class Partical : MonoBehaviour
{
    //private static Random rnd = new Random(228);
    public Animator animator;
    public Rigidbody2D body;

    private void Start()
    {
        animator = animator != null ? animator : GetComponent<Animator>();
        body = body != null ? body : GetComponent<Rigidbody2D>();

        Vector2 vector = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Vector3 rotation = new Vector3(0, 0, Random.value) * 360;

        transform.rotation = Quaternion.Euler(rotation);
        body.velocity = vector;
        body.angularVelocity = Random.value * 10;
    }

    private void OnDestroy()
    {
        Die();
    }
    public virtual void Die()
    {
        //Destroy(gameObject);
    }
}
