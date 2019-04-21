using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame_CarSelection : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Загрузка самой игры
    /// </summary>
    public void newGame()
    {
        SceneManager.LoadScene("Start", LoadSceneMode.Single);
    }
}
