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
        Advertisement.Initialize("3791501", false);
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
        if (PlayerPrefs.GetInt("NoAds") != 1)
        {
            if (Advertisement.IsReady() && Advertisement.isInitialized)
            {
                Advertisement.Show("video", new ShowOptions() { resultCallback = HandleAdResultSkipable });
            }
        }
    }

    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                PlayerPrefs.SetFloat("Cash", PlayerPrefs.GetFloat("Cash") + 300f);
                Amplitude.Instance.logEvent("CashForAds");
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Failed:
                break;
        }
    }

    private void HandleAdResultSkipable(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Amplitude.Instance.logEvent("RestartForAds");
                break;
            case ShowResult.Skipped:
                break;
            case ShowResult.Failed:
                break;
        }
    }
}
