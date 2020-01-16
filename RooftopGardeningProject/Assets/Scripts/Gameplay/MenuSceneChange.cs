using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneChange : MonoBehaviour
{
    public void SwitchScene() 
    {
        if (SceneManager.GetActiveScene().name == "ManagementMenu")
        {
            SceneManager.LoadScene(GameplayController.Instance.CurrentSceneName);
        }
        else
        {
            SceneManager.LoadScene("ManagementMenu");
        }
    }
}
