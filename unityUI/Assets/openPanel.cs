using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class openPanel : MonoBehaviour {
    public GameObject panel;
    public GameObject movePosition;

    public Vector3 orignalPostion;

    public AudioSource audioSource;

    public AudioClip clip;


   



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

    public void MuteGameAudio () {
        audioSource.PlayOneShot (clip, 1F);
    }

    




}