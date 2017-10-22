using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TitleController : MonoBehaviour
{
    public string firstSceneName = "main";
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
        GameObject.Find("GameManager").GetComponent<GameManager>().SceneChange(firstSceneName);
    }

    public void NewGame()
    {
        AlertController.alertController.ShowAlertByInput("ニューゲーム", "名前を入力してください。", NewGameCallback);
    }

    public void Continue()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().Load();
    }
}
