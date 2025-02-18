using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;
using System;
public class RewarderAd : MonoBehaviour
{

    private RewardedAdLoader rewardedAdLoader;
    private RewardedAd rewardedAd;

    public Action action;

    public void Awake()
    {
        this.rewardedAdLoader = new RewardedAdLoader();
        this.rewardedAdLoader.OnAdLoaded += this.HandleAdLoaded;
        this.rewardedAdLoader.OnAdFailedToLoad += this.HandleAdFailedToLoad;
    }

    public void RequestRewardedAd()
    {
        //Sets COPPA restriction for user age under 13
        MobileAds.SetAgeRestrictedUser(true);

        if (this.rewardedAd != null)
        {
            this.rewardedAd.Destroy();
        }

        // Replace demo Unit ID 'demo-rewarded-yandex' with actual Ad Unit ID
        string adUnitId = "R-M-14181657-2";

        this.rewardedAdLoader.LoadAd(this.CreateAdRequest(adUnitId));
    }

    public void HandleAdLoaded(object sender, RewardedAdLoadedEventArgs args)
    {
        this.rewardedAd = args.RewardedAd;
    }

    public void ShowRewardedAd()
    {
        if (this.rewardedAd == null)
        {
            return;
        }

        this.rewardedAd.OnAdClicked += this.HandleAdClicked;
        this.rewardedAd.OnAdShown += this.HandleAdShown;
        this.rewardedAd.OnAdFailedToShow += this.HandleAdFailedToShow;
        this.rewardedAd.OnAdImpression += this.HandleImpression;
        this.rewardedAd.OnAdDismissed += this.HandleAdDismissed;
        this.rewardedAd.OnRewarded += this.HandleRewarded;
        this.rewardedAd.OnRewarded += this.HandleRewarded;

        this.rewardedAd.Show();
    }

    private AdRequestConfiguration CreateAdRequest(string adUnitId)
    {
        return new AdRequestConfiguration.Builder(adUnitId).Build();
    }
    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
    }

    public void HandleAdClicked(object sender, EventArgs args)
    {
    }

    public void HandleAdShown(object sender, EventArgs args)
    {
    }

    public void HandleAdDismissed(object sender, EventArgs args)
    {
        this.rewardedAd.Destroy();
        this.rewardedAd = null;
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
    }

    public void HandleRewarded(object sender, Reward args)
    {
        action?.Invoke();
        action = null;
    }

    public void HandleAdFailedToShow(object sender, AdFailureEventArgs args)
    {
    }

}
