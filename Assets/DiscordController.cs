using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Discord;

public class DiscordController : MonoBehaviour
{
    DateTime epochStart;
    int currentEpochTime;

    public Discord.ActivityManager activityManager;

    public Discord.Discord discord;

    public Discord.Activity activity;

    // Use this for initialization
    void Start()
    {
        UpdateEpoch();
        GetActivity();
        UpdateActivity();
        SaveActivity();
    }

    // Update is called once per frame
    void Update()
    {
        discord.RunCallbacks();
    }

    void UpdateEpoch()
    {
        epochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        currentEpochTime = (int)(DateTime.UtcNow - epochStart).TotalSeconds;
    }

    void GetActivity()
    {
        discord = new Discord.Discord(671023511539810305, (System.UInt64)Discord.CreateFlags.Default);
        activityManager = discord.GetActivityManager();
    }

    void UpdateActivity()
    {
        activity = new Discord.Activity
        {
            State = "Online QA",
            Details = "Private Bata",

            Timestamps =
             {
                Start = currentEpochTime,
                //End = 1580210732,
             },

            Assets =
            {
                LargeImage = "gta",
                LargeText = "",
                SmallImage = "vi",
                SmallText = "",
            },

            Party =
            {
                Id = "foo partyID",

                Size =
                {
                    CurrentSize = 13,
                    MaxSize = 116,
                },
            },

            Secrets =
            {
                //Match = "foo matchSecret",
                Join = "memes",
                //Spectate = "foo spectateSecret",
            },

            Instance = true,
        };
    }

    void SaveActivity()
    {
        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.LogError("Everything is fine!");
            }
        });
    }

    void CleanActivity()
    {
        activityManager.ClearActivity((result) =>
        {
            if (result == Discord.Result.Ok)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("Failed");
            }
        });
    }
}
