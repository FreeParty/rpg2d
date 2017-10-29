using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SymbolEncountContoller : MonoBehaviour
{

    public int monster_num;

    // Use this for initialization
    void Start()
    {
        switch(EnemyController.monster_name) //戦闘終了後に呼び出したい処理を書く。逃げれない敵を指定すれば倒した後の処理となる。Endingへの遷移、倒した後の敵の命乞い等役立ててください。
        {
            case "THEラスボス": LogController.logController.printTextByFileName("test.txt").then(Callback1);
                    break;
        }
        if(GetComponent<Messeage>() == null)
        {
            Encount();
        }
    }

    void Callback1()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().SceneChange("ending", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Encount()
    {
        GameObject.Find("BGM Source").GetComponent<BGMcontroller>().EncountSound();
        GameObject.Find("GameManager").GetComponent<GameManager>().SceneChange("battle?mn=" + monster_num, true);
    }
}