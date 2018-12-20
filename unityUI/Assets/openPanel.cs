using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using GameSparks.Api;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using Facebook.Unity;
using Facebook;

public class openPanel : MonoBehaviour {
    public GameObject panel;
    public GameObject movePosition;
    public Vector3 orignalPostion;
    public AudioSource audioSource;
    public AudioClip clip;

    public string fbAuthToken = "";



    // Start is called before the first frame update
    void Start () {
        orignalPostion = panel.transform.position;
    }

    // Update is called once per frame
    void Update () {
       

    }

    public void onColseMenu () {
        // panel.transform.position = orignalPostion;
        panel.transform.DOMoveX(orignalPostion.x, 0.5f).SetEase(Ease.InOutQuint);
    }

    public void onOpenMenu () {
        
        panel.transform.DOMoveX(movePosition.transform.position.x, 0.5f).SetEase(Ease.InOutQuint);
        //panel.transform.position = movePosition.transform.position;
    }

    public void MuteGameAudio (bool onOff) {
        
        if (onOff == true){
            audioSource.PlayOneShot(clip, 1F);
        }
        else{
            audioSource.Stop();
        }


    }



    void Awake()
    {
        //load authtoken from playerprefs and see if it's been filled in
        Load();
        if (fbAuthToken != "")
        {
            GameSparksFBLogBackIn();
        }
    }

    public void CallFBInit()
    {
        //initialize facebook
        FB.Init(this.OnInitComplete, this.OnHideUnity);
    }

    private void OnInitComplete()
    {
        Debug.Log("FB.Init completed: Is user logged in? " + FB.IsLoggedIn);
        CallFBLogin();
    }

    private void CallFBLogin()
    {
        FB.LogInWithReadPermissions(new List<string>() { "public_profile", "email" }, GameSparksFBLogin);
    }

    private void GameSparksFBLogin(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            fbAuthToken = AccessToken.CurrentAccessToken.TokenString;
            Save();
            new FacebookConnectRequest()
                .SetAccessToken(AccessToken.CurrentAccessToken.TokenString)
                .SetSwitchIfPossible(true)
                .SetSyncDisplayName(true)
                .Send((response) => {
                    if (response.HasErrors)
                    {
                        Debug.Log("Something failed when connecting with Facebook - " + result.Error);
                    }
                    else {
                        Debug.Log("Gamesparks Facebook login successful");
                    }
                });
        }
    }

    private void GameSparksFBLogBackIn()
    {
        new FacebookConnectRequest()
            .SetAccessToken(fbAuthToken)
            .Send((response) => {
                if (response.HasErrors)
                {
                    Debug.Log("Something failed when connecting with Facebook on log back in");
                }
                else {
                    Debug.Log("Gamesparks Facebook login successful on log back in");
                }
            });
    }

    private void OnHideUnity(bool isGameShown)
    {

    }

    void Save()
    {
        PlayerPrefs.SetString("fbAuthToken", fbAuthToken);
    }

    void Load()
    {
        fbAuthToken = PlayerPrefs.GetString("fbAuthToken");
    }


}