using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TitleController : MonoBehaviour
{
    private string firstSceneName = "map_station";
    AudioSource audio;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void NewGameCallback(string playerName)
    {
        PlayerContoroller.player_name = playerName;
	audio = GetComponent<AudioSource>();
	audio.PlayOneShot(audio.clip);
        GameObject.Find("GameManager").GetComponent<GameManager>().SceneChange(firstSceneName,true);
    }

    public void NewGame()
    {
	audio = GetComponent<AudioSource>();
	audio.PlayOneShot(audio.clip);
        AlertController.alertController.ShowAlertByInput("ニューゲーム", "名前を入力してください。", "ゆうしゃ" ,NewGameCallback);
    }

    public void Continue()
    {
	audio = GetComponent<AudioSource>();
	audio.PlayOneShot(audio.clip);
        GameObject.Find("GameManager").GetComponent<GameManager>().Load();
    }
}
