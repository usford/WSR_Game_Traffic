using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedCar : MonoBehaviour
{
    private GameObject img;
    private GameObject panel;
    public GameObject txtModel;
    public GameObject txtMaxSpeed;

    void Start()
    {
        img = GameObject.Find("Image");
        img.GetComponent<Image>().enabled = false;

        panel = GameObject.Find("Panel_Characterictic");
        panel.SetActive(false);
    }

    void Update()
    {
        
    }

    public void clicked(GameObject car)
    {


        Car_General script = car.GetComponent<Car_General>();

        Car.model = script.model;
        Car.sprite = script.sprite;
        Car.maxSpeed = script.maxSpeed;

        img.GetComponent<Image>().sprite = script.sprite;
        img.GetComponent<Image>().enabled = true;

        panel.SetActive(true);

        txtModel.GetComponent<Text>().text = script.model;
        txtMaxSpeed.GetComponent<Text>().text = script.maxSpeed.ToString() + " км/ч";
    }
}
