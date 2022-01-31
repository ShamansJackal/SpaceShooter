using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    // Start is called before the first frame update
    public int OrderInLayer;
    public TextMesh text;
    public Rigidbody2D body;
    public MeshRenderer mesh;
    void Start()
    {
        mesh.sortingOrder = OrderInLayer;
        body.AddForce(new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0.2f, 0.5f)) *7, ForceMode2D.Impulse);
        Destroy(gameObject, 1.5f);
    }
}
