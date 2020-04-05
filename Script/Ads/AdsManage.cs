using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class AdsManage : MonoBehaviour
{
    public static AdsManage manage;
    private void Awake()
    {
        manage = this;
        Advertisement.Initialize("3530524");
    }

    public void ShowAd()
    {
        if (Advertisement.IsReady() && Advertisement.isInitialized)
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleAdResult });
        }
    }
    public void ShowAdDefault()
    {
        if (Advertisement.IsReady() && Advertisement.isInitialized)
        {
            Advertisement.Show("video", new ShowOptions() { resultCallback = HandleAdResult });
        }
    }

    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") + 100f);
                break;
            case ShowResult.Skipped:
                print("Skipped");
                break;
            case ShowResult.Failed:
                print("Internet?");
                break;
        }
    }
}
