using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanisterDestroy : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Car"))
        {
            Destroy(this.gameObject);
        }
    }
}
