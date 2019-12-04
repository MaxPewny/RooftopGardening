﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using core;
#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif

public class Notification : Singleton<Notification>
{
    protected Notification() { }
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
        OpenAndroidNotificationChannel();
    }

    void OpenAndroidNotificationChannel()
    {
#if UNITY_ANDROID
        AndroidNotificationCenter.RegisterNotificationChannel(defaultNotificationChannel);
#endif
    }

    public void SendAndroidNotification(DateTime FireTime, string Title, string Text )
    {
#if UNITY_ANDROID
        AndroidNotification notification = new AndroidNotification();
        notification.Title = Title;
        notification.Text = Text;
        notification.FireTime = FireTime;

        AndroidNotificationCenter.SendNotification(notification, "channel_0");
#endif
    }
}