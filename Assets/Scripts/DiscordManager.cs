using UnityEngine;

public class DiscordManager : MonoBehaviour
{
    Discord.Discord discord;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        discord = new Discord.Discord(1344071019379294208, (ulong)Discord.CreateFlags.NoRequireDiscord);
        ChangeActivity();
    }

    void OnDisable()
    {
        discord.Dispose();
    }

    public void ChangeActivity()
    {
        var activityManager = discord.GetActivityManager();
        var activity = new Discord.Activity
        {
            State = "Playing",
            Details = "By Kawok Enjoyers"
        };
        activityManager.UpdateActivity(activity, (res) => { });
    }

    // Update is called once per frame
    void Update()
    {
        discord.RunCallbacks();
    }
}
