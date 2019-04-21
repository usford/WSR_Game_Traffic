using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back_CarSelection : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// Возвращение в главное меню
    /// </summary>
    public void back()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
