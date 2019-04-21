using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeginGame : MonoBehaviour
{
    private GameObject car;
    private GameObject canvas;
    private bool setAct = false;
    void Start()
    {
        car = GameObject.FindWithTag("Car");
        canvas = GameObject.Find("Panel");

        car.GetComponent<SpriteRenderer>().sprite = Car.sprite;
        canvas.SetActive(setAct);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            setAct = !setAct;
            canvas.SetActive(setAct);
        }
    }

    /// <summary>
    /// Продолжение игры
    /// </summary>
    public void continute()
    {
        
        setAct = !setAct;
        canvas.SetActive(setAct);
    }

    /// <summary>
    /// Выход в главное меню
    /// </summary>
    public void exit()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
