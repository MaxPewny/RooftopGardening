using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string NextSceneLeftName;
    public string NextSceneRightName;

    public GameObject LeftButton;
    public GameObject RightButton;

    private void Start()
    {
        if (NextSceneLeftName == "")
        {
            LeftButton.SetActive(false);
        }
        if (NextSceneRightName == "")
        {
            RightButton.SetActive(false);
        }
    }

    public void ChangeSceneLeft() 
    {
        SceneManager.LoadScene(NextSceneLeftName);
    }

    public void ChangeSceneRight()
    {
        SceneManager.LoadScene(NextSceneRightName);
    }
}
