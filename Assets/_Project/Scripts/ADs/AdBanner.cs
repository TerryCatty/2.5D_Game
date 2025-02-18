using UnityEngine;
using System;
using YandexMobileAds;
using YandexMobileAds.Base;

public class AdBanner : MonoBehaviour
{

    private Banner banner;

    private void Start()
    {
        ShowBanner();
    }

    public void ShowBanner()
    {
        MobileAds.SetAgeRestrictedUser(true);

        string adUnitId = "demo-banner-yandex";

        if (this.banner != null)
        {
            this.banner.Destroy();
        }

        BannerAdSize bannerSize = BannerAdSize.StickySize(GetScreenWidthDp());

        this.banner = new Banner(adUnitId, bannerSize, AdPosition.BottomCenter);

        try
        {
            this.banner.OnAdLoaded += this.HandleAdLoaded;
        }
        catch
        {
            Debug.Log("Failed to load banner");
        }
    }

    private int GetScreenWidthDp()
    {
        int screenWidth = (int)Screen.safeArea.width;
        return ScreenUtils.ConvertPixelsToDp(screenWidth);
    }

    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder().Build();
    }

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        this.banner.Show();
    }

}
