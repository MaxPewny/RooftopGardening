using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Unity.Notifications.Android;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text DebugText;

    private void Awake()
    {
        try
        {
            Notification.Instance.OpenAndroidNotificationChannel();
            Notification.Instance.SendAndroidNotification(DateTime.Now, "Rooftop Gardening", "Willkomen zu deinem Persönlichen Garten");
        }
        catch (Exception e)
        {
            DebugText.text = e.Message;
        }
        
    }

    public void ClickStart()
    {
        SceneManager.LoadScene("Garden_FM");
    }
}