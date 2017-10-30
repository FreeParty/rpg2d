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
            case "ぬし": LogController.logController.printTextByFileName("/west/boss2.txt").then(Destroyer);
                    break;
            case "上級でんだいせい": LogController.logController.printTextByFileName("/d22/boss2.txt").then(Destroyer);
                    break;
            case "がっかたんとう": LogController.logController.printTextByFileName("/d1/boss2.txt").then(Destroyer);
                    break;
            case "GOD": LogController.logController.printTextByFileName("/d1/GOD2.txt").then(Destroyer);
                    break;
            case "マスクドADACHI": LogController.logController.printTextByFileName("/d1/Last2.txt").then(Callback1);
                    break;
        }
        if (EnemyController.monster_num == monster_num)
        {
            Destroy(gameObject);
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
    public void Destroyer(){
	Destroy(gameObject);
    }


}