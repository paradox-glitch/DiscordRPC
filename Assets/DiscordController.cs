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
            Details = "Private Beta",

            Timestamps =
             {
                Start = currentEpochTime,
                //End = 1580210732,
             },

            Assets =
            {
                LargeImage = "gtasmall",
                LargeText = "GTA VI",
                SmallImage = "online",
                SmallText = "Online",
            },

            Party =
            {
                Id = "foo partyID",

                Size =
                {
                    CurrentSize = 15,
                    MaxSize = 256,
                },
            },

            Secrets =
            {
                //Match = "meme",
                Join = "meme",
                //Spectate = "meme",
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
