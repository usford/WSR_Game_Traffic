using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveAI : MonoBehaviour
{
    public Transform selfTransform;
    public float maxSpeedAI = 100;
    private float speed = 0;
    private Vector3 _force;
    public Sprite carBlue;
    public Sprite carRed;
    public Sprite truck;
    public bool oncomingTraffic;

    void Start()
    {
        speed = maxSpeedAI / 300;
        int random = Random.Range(0, 3);
        Vector2 box = gameObject.GetComponent<BoxCollider2D>().size;
        box.y = 1.4f;
        gameObject.GetComponent<BoxCollider2D>().size = box;
        switch(random)
        {
            case 0:
                GetComponent<SpriteRenderer>().sprite = carBlue;
                break;

            case 1:
                GetComponent<SpriteRenderer>().sprite = carRed;
                break;

            case 2:
                GetComponent<SpriteRenderer>().sprite = truck;
                maxSpeedAI = 60;
                speed = maxSpeedAI / 300;
                box.y = 2.16f;
                gameObject.GetComponent<BoxCollider2D>().size = box;
                break;
        }
        
    }

    void Update()
    {
        if (oncomingTraffic)
        {
            selfTransform.position = new Vector3(selfTransform.position.x, selfTransform.position.y - speed, selfTransform.position.z);
        }
        else
        {
            selfTransform.position = new Vector3(selfTransform.position.x, selfTransform.position.y + speed, selfTransform.position.z);
        }
        

        //if (speed < maxSpeedAI)
        //{
        //    speed += 0.5f;
        //    _force += (selfTransform.up * Time.deltaTime) * (speed / 200);
        //}
        //else if (speed >= maxSpeedAI)
        //{
        //    speed = maxSpeedAI;
        //}
    }
}
