using Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == gameObject.layer)
        {
            if (collision.gameObject.TryGetComponent(out IPoolebObject poolebObject))
                poolebObject.ReturnToPoll();
            else
                Destroy(collision.gameObject);
        }
    }

}
