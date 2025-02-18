using System;
using UnityEngine;
using YandexMobileAds;
using YandexMobileAds.Base;

public class AdsInterstitial : MonoBehaviour
{

    private InterstitialAdLoader interstitialAdLoader;
    private Interstitial interstitial;
    public void Awake()
    {
        this.interstitialAdLoader = new InterstitialAdLoader();
        this.interstitialAdLoader.OnAdLoaded += this.HandleAdLoaded;
        this.interstitialAdLoader.OnAdFailedToLoad += this.HandleAdFailedToLoad;
    }

    public void RequestInterstitial()
    {
        //Sets COPPA restriction for user age under 13
        MobileAds.SetAgeRestrictedUser(true);

        // Replace demo Unit ID 'demo-interstitial-yandex' with actual Ad Unit ID
        string adUnitId = "R-M-14181657-1";

        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        this.interstitialAdLoader.LoadAd(this.CreateAdRequest(adUnitId));
    }
    public void ShowInterstitial()
    {
        if (this.interstitial == null)
        {
            return;
        }

        this.interstitial.OnAdClicked += this.HandleAdClicked;
        this.interstitial.OnAdShown += this.HandleAdShown;
        this.interstitial.OnAdFailedToShow += this.HandleAdFailedToShow;
        this.interstitial.OnAdImpression += this.HandleImpression;
        this.interstitial.OnAdDismissed += this.HandleAdDismissed;

        this.interstitial.Show();
    }

    private AdRequestConfiguration CreateAdRequest(string adUnitId)
    {
        return new AdRequestConfiguration.Builder(adUnitId).Build();
    }

    public void HandleAdLoaded(object sender, InterstitialAdLoadedEventArgs args)
    {
        this.interstitial = args.Interstitial;
    }
    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {

    }
    public void HandleAdClicked(object sender, EventArgs args)
    {
    }

    public void HandleAdShown(object sender, EventArgs args)
    {
        RequestInterstitial();
    }
    public void HandleAdDismissed(object sender, EventArgs args)
    {
        this.interstitial.Destroy();
        this.interstitial = null;
    }

    public void HandleImpression(object sender, ImpressionData impressionData)
    {
        var data = impressionData == null ? "null" : impressionData.rawData;
    }

    public void HandleAdFailedToShow(object sender, AdFailureEventArgs args)
    {
    }
}
