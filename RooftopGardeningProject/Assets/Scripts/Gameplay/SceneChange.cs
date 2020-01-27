using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public Level UnlockLevel;

    public string NextSceneLeftName;
    public string NextSceneRightName;
    public string NextSceneUpName;
    public string NextSceneDownName;

    public GameObject LeftButton;
    public GameObject RightButton;
    public GameObject UpButton;
    public GameObject DownButton;
    public GameObject LockScreen;

    private void Start()
    {
        LockScene();

        if (NextSceneLeftName == "")
        {
            LeftButton.SetActive(false);
        }
        if (NextSceneRightName == "")
        {
            RightButton.SetActive(false);
        }
        if (NextSceneUpName == "")
        {
            UpButton.SetActive(false);
        }
        if (NextSceneDownName == "")
        {
            DownButton.SetActive(false);
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
    public void ChangeSceneUp()
    {
        SceneManager.LoadScene(NextSceneUpName);
    }
    public void ChangeSceneDown()
    {
        SceneManager.LoadScene(NextSceneDownName);
    }

    public void LockScene() 
    {
        if (UnlockLevel <= GameplayController.Instance.PlayerLevel)
        {
            LockScreen.SetActive(false);
        }
        else 
        {
            LockScreen.SetActive(true);
        }
    }
}
