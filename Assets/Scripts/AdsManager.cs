using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener,IUnityAdsInitializationListener
{
    [SerializeField] Button _showAdButton;
    [SerializeField] TextMeshProUGUI _buttonText;
    [SerializeField] int AmountOfReward;
    private string _adUnitId = "5306263";
    private string _rewardedVideo = "Rewarded_Android";
    void Awake()
    {
    

        // Disable the button until the ad is ready to show:
        _showAdButton.interactable = false;
        Advertisement.Initialize(_adUnitId, true,this);

    }

    // Call this public method when you want to get an ad ready to show.
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_rewardedVideo, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_rewardedVideo))
        {
            // Configure the button to call the ShowAd() method when clicked:
            _showAdButton.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            _showAdButton.interactable = true;
        }
    }

    // Implement a method to execute when the user clicks the button:
    public void ShowAd()
    {
        // Disable the button:
        _showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_rewardedVideo, this);
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_rewardedVideo) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");

            GlobalVariables.instance.AmountOfMoney += AmountOfReward;
            IEnumerator timer = WaitForAd();
            StartCoroutine(timer);
        }
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        _showAdButton.onClick.RemoveAllListeners();
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Initialized succesfully");
        LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Error intializing Ad Unit : {error.ToString()} - {message}");
    }
    public IEnumerator WaitForAd()
    {
        for(int i =10; i>=0; i--)
        {
            _buttonText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        _buttonText.text = "ad";
        LoadAd();
    }
}
