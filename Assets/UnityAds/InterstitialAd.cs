using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidAdUnitId = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitId = "Interstitial_iOS";
    string _adUnitId;
    [SerializeField] public GameObject loadingObject;
    void Awake()
    {
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;
        LoadAd();
    }


    public IEnumerator loadAd()
    {
        yield return new WaitForSeconds(5);
        LoadAd();
        ShowAd();
        StartCoroutine(loadAd());
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string _adUnitId) 
    { 
        LoadAd();
    }
    public void OnUnityAdsShowClick(string _adUnitId) { }

    public void next()
    {
        int currentLevel = PlayerPrefs.GetInt("level", 1);
        if (currentLevel <= 25 && currentLevel % 4 == 0)
        {
            loadingObject.SetActive(true);
            ShowAd();
            StartCoroutine(loadAd());
        }
        else if(currentLevel > 25 && currentLevel <= 50 && currentLevel % 3 == 0)
        {
            loadingObject.SetActive(true);
            ShowAd();
            StartCoroutine(loadAd());
        }
        else if (currentLevel > 50 && currentLevel <= 100 && currentLevel % 2 == 0)
        {
            loadingObject.SetActive(true);
            ShowAd();
            StartCoroutine(loadAd());
        }
        else if (currentLevel > 100)
        {
            loadingObject.SetActive(true);
            ShowAd();
            StartCoroutine(loadAd());
        }
        else
        {
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level", 1) + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState) {

        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level", 1) + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}