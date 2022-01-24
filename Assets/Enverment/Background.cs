using System.Collections;
using UnityEditor;
using UnityEngine;

public sealed class Background : MonoBehaviour
{
    static Vector3 StartPos = new Vector3(0f, 10.75f);
    static Vector3 SpawnPos = new Vector3(0f, 0f);
    static readonly float speed = 1f;
    static readonly float time = Mathf.Abs(StartPos.y - SpawnPos.y) / speed;

    public Rigidbody2D body;
    public GameObject parent;
    public bool SpawnChilds = true;

    void Start()
    {
        body.velocity = new Vector2(0, -speed);
        if (SpawnChilds) StartCoroutine(SpawnNext());
        else Destroy(gameObject, time);
        name = "Backgroud";
    }

    IEnumerator SpawnNext()
    {
        yield return new WaitForSeconds(time);
        Instantiate(gameObject, StartPos, Quaternion.identity, parent.transform);
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
