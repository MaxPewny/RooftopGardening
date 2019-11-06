using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class Notification : MonoBehaviour
{
#if UNITY_ANDROID
    AndroidNotificationChannel defaultNotificationChannel = new AndroidNotificationChannel()
    {
        Id = "channel_0",
        Name = "Default Channel",
        Importance = Importance.High,
        Description = "Generic notifications",
    };
#endif

    private void Start()
    {
#if UNITY_ANDROID
        OpenAndroidNotificationChannel();
#endif
    }

#if UNITY_ANDROID
    void OpenAndroidNotificationChannel()
    {
        AndroidNotificationCenter.RegisterNotificationChannel(defaultNotificationChannel);
    }

    public void SendAndroidNotification(DateTime FireTime, string Title, string Text )
    {
        AndroidNotification notification = new AndroidNotification();
        notification.Title = Title;
        notification.Text = Text;
        notification.FireTime = FireTime;

        AndroidNotificationCenter.SendNotification(notification, "channel_0");
    }
#endif
}