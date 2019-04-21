using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamera : MonoBehaviour
{
    public GameObject car;

    void Update()
    {
        transform.position = new Vector3(0.03f, car.transform.position.y + 1f, -10f);
    }
}
