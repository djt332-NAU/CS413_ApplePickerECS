using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    public void StartGameOOP()
    {
        SceneManager.LoadScene("_Scene_AP_OOP");
    }
    
    public void StartGameECS()
    {
        SceneManager.LoadScene("_Scene_AP_ECS");
    }
}
