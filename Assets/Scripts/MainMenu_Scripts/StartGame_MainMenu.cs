using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame_MainMenu : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    /// <summary>
    /// Загрузка сцены с выбором автомобиля
    /// </summary>
    public void newGame()
    {
        SceneManager.LoadScene("CarSelection", LoadSceneMode.Single);
    }
}
