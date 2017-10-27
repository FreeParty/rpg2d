using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{

    /*
     * GetComponent<LogController>().printText(messages)はLogControllerを返す
     * LogControllerにはthenというメソッドがあり、そこに関数を入れるとログの表示が完了後実行される
     * void型の引数なしの関数を指定できる
     * 
     * 使い方はGetComponent<LogController>().printText(messages).then(callback1).then(callback2)...とつなげる
     * void型のcallback関数の型はLogController.Callback
     * 
     * また、メソッドcancelはそれまでに登録したコールバック関数を削除する。
     * 引数にvoid型の引数なしの関数を指定すると、コールバック関数を削除したうえで代わりにその関数を実行する。
     */

    public GameObject name_obj;
    public GameObject hp_obj;
    public GameObject mp_obj;
    public GameObject a_button;
    public GameObject b_button;
    public GameObject sound_box;
    public GameObject commands;
    ItemController ic;

    int runcounter = 0;
    public bool isUsedItem = false;

    public class IntAndBool
    {
        public int damage;
        public bool isCelanHit;
    }

    void Start()
    {
        if (name_obj == null)
        {
            name_obj = GameObject.Find("Name");
        }
        name_obj.GetComponent<Text>().text = PlayerContoroller.player_name;

        if (name_obj == null)
        {
            name_obj = GameObject.Find("Name");
        }
        if (hp_obj == null)
        {
            hp_obj = GameObject.Find("p_hp");
        }
        if (hp_obj == null)
        {
            mp_obj = GameObject.Find("p_mp");
        }
        if (sound_box == null)
        {
            sound_box = GameObject.Find("BattleSounds");
        }
        StatusUpdate();

        string imgPath = "";
        switch (GameObject.Find("GameManager").GetComponent<GameManager>().prevSceneName)
        {
            case "map_east":
                imgPath = "Materials/east";
                break;
            case "map_west":
                imgPath = "Materials/west02";
                break;
            case "map_dendai1_1":
                imgPath = "Materials/dendai1-01";
                break;
            case "map_dendai1_2":
                imgPath = "Materials/dendai1-02";
                break;
            case "map_dendai1_3":
                imgPath = "Materials/dendai1-03";
                break;
            case "map_dendai2_1":
                imgPath = "Materials/dendai2_1-01";
                break;
            case "map_dendai2_2":
                imgPath = "Materials/dendai2_2";
                break;
            default:
                imgPath = "Materials/field1";
                break;
        }
        Texture2D texture = Resources.Load(imgPath) as Texture2D;
        GameObject.Find("BattleField").GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        ic = GameObject.Find("ItemController").GetComponent<ItemController>();
    }

    public void StatusUpdate()
    {
        GameObject.Find("StatusWindowInBattle").GetComponent<StatusController>().Print();
    }

    public void AttackToEnemy()
    {
        IntAndBool e_damage = E_damage();
        EnemyController.enemy_status["hp"] -= e_damage.damage;

        string[] messages;
        if (e_damage.isCelanHit)
        {
            messages = new string[] { "会心の一撃！\n" + EnemyController.monster_name + "に" + e_damage.damage + "のダメージを与えた" };
            sound_box.GetComponent<BattleSoundsController>().Critical();
        }
        else
        {
            if (e_damage.damage > 0)
            {
                messages = new string[] { EnemyController.monster_name + "に" + e_damage.damage + "のダメージを与えた" };
                sound_box.GetComponent<BattleSoundsController>().Attack();
            }
            else
            {
                messages = new string[] { EnemyController.monster_name + "は攻撃をかわした！" };
                sound_box.GetComponent<BattleSoundsController>().Miss();
            }
        }

        if (EnemyController.enemy_status["hp"] > 0)
        {
            if (PlayerContoroller.player_status["ag"] > EnemyController.enemy_status["ag"]) //AttackToEnemy => AttackToPlayer => ToggleCommands
            {
                LogController.logController.printText(messages).then(AttackToPlayer);
            }
            else //AttackToPlayer => AttackToEnemy => ToggleCommands
            {
                LogController.logController.printText(messages).then(ToggleCommands);
            }
        }
        else
        {
            LogController.logController.printText(messages).cancel(Enemy_die);
        }
    }

    public void AttackToPlayer()
    {
        IntAndBool p_damage = P_damage();
        PlayerContoroller.player_status["hp"] -= p_damage.damage;
        StatusUpdate();

        string[] messages;
        if (p_damage.isCelanHit)
        {
            messages = new string[] { "痛恨の一撃！\n" + EnemyController.monster_name + "から" + p_damage.damage + "のダメージを受けた。" };
            sound_box.GetComponent<BattleSoundsController>().Critical();
        }
        else
        {
            if (p_damage.damage > 0)
            {
                messages = new string[] { EnemyController.monster_name + "から" + p_damage.damage + "のダメージを受けた。" };
                sound_box.GetComponent<BattleSoundsController>().Attack();
            }
            else
            {
                messages = new string[] { PlayerContoroller.player_name + "は攻撃をかわした！" };
                sound_box.GetComponent<BattleSoundsController>().Miss();
            }
        }

        if (PlayerContoroller.player_status["hp"] > 0)
        {
            if (PlayerContoroller.player_status["ag"] > EnemyController.enemy_status["ag"] || isUsedItem)
            { //AttackToEnemy => AttackToPlayer => ToggleCommands
                LogController.logController.printText(messages).then(ToggleCommands);
                if (isUsedItem) isUsedItem = false;
            }
            else
            { //AttackToPlayer => AttackToEnemy => ToggleCommands
                LogController.logController.printText(messages).then(AttackToEnemy);
            }
        }
        else
        {
            LogController.logController.printText(messages).cancel(Player_die);
        }
    }
    public void AttackToPlayer_Guard()
    {
        IntAndBool p_damage = P_damage();
        int guard_damage = p_damage.damage * 3 / 4;
        PlayerContoroller.player_status["hp"] -= guard_damage;
        StatusUpdate();

        string[] messages;
        if (p_damage.isCelanHit)
        {
            messages = new string[] { PlayerContoroller.player_name + "は身を守っている\n痛恨の一撃！\n" + EnemyController.monster_name + "から" + guard_damage + "のダメージを受けた。" };
            sound_box.GetComponent<BattleSoundsController>().Critical();
        }
        else
        {
            if (guard_damage > 0)
            {
                messages = new string[] { PlayerContoroller.player_name + "は身を守っている\n" + EnemyController.monster_name + "から" + guard_damage + "のダメージを受けた。" };
                sound_box.GetComponent<BattleSoundsController>().Attack();
            }
            else
            {
                messages = new string[] { PlayerContoroller.player_name + "は身を守っている\n" + PlayerContoroller.player_name + "は攻撃を防いだ！" };
                sound_box.GetComponent<BattleSoundsController>().Guard();
            }
        }

        if (PlayerContoroller.player_status["hp"] > 0)
        {
            LogController.logController.printText(messages).then(ToggleCommands);
        }
        else
        {
            LogController.logController.printText(messages).cancel(Player_die);
        }
    }
    public void AttackToPlayer_Run()
    {
        IntAndBool p_damage = P_damage();
        PlayerContoroller.player_status["hp"] -= p_damage.damage;
        StatusUpdate();

        string[] messages;
        if (p_damage.isCelanHit)
        {
            messages = new string[] { "痛恨の一撃！\n" + EnemyController.monster_name + "から" + p_damage.damage + "のダメージを受けた。" };
            sound_box.GetComponent<BattleSoundsController>().Critical();
        }
        else
        {
            if (p_damage.damage > 0)
            {
                messages = new string[] { EnemyController.monster_name + "から" + p_damage.damage + "のダメージを受けた。" };
                sound_box.GetComponent<BattleSoundsController>().Attack();
            }
            else
            {
                messages = new string[] { PlayerContoroller.player_name + "は攻撃をかわした！" };
                sound_box.GetComponent<BattleSoundsController>().Miss();
            }
        }

        if (PlayerContoroller.player_status["hp"] > 0)
        {
            LogController.logController.printText(messages).then(ToggleCommands);
        }
        else
        {
            LogController.logController.printText(messages).cancel(Player_die);
        }
    }


    public static void ToggleCommands()
    {

        if (GameObject.Find("Commands") != null)
        {
            GameObject.Find("Commands").SetActive(false);
        }
        else
        {
            GameObject.Find("BattleField").transform.Find("Commands").gameObject.SetActive(true);
        }
    }

    // たたかう が押された時に発火
    public void Fight()
    {
        ToggleCommands();
        if (PlayerContoroller.player_status["ag"] > EnemyController.enemy_status["ag"]) //Playerが先手
        {
            AttackToEnemy();
        }
        else
        {
            AttackToPlayer();
        }
    }

    // ぼうぎょ が押された時に発火
    public void Guard()
    {
        ToggleCommands();
        AttackToPlayer_Guard();
    }

    // にげる が押された時に発火
    public void Runaway()
    {
        Boolean runflag = false;
        ToggleCommands();
        if (EnemyController.enemy_status["type"] == 0)
        {    //ボスフラグ判定
            if (PlayerContoroller.player_status["ag"] > EnemyController.enemy_status["ag"]) //確定逃げ
                runflag = true;
            else
            {
                switch (runcounter)
                {
                    case 0:
                        if (UnityEngine.Random.Range(0, 3) == 1) runflag = true;
                        runcounter++;
                        break;
                    case 1:
                        if (UnityEngine.Random.Range(0, 4) > 0) runflag = true;
                        runcounter++;
                        break;
                    case 2:
                        runflag = true;
                        break;
                }
            }

            if (runflag)
            {
                runcounter = 0;
                sound_box.GetComponent<BattleSoundsController>().Run();
                LogController.logController.printText(new string[] { PlayerContoroller.player_name + "は逃げだした。" }).then(BackField);
            }
            else
            {
                sound_box.GetComponent<BattleSoundsController>().Run();
                LogController.logController.printText(new string[] { PlayerContoroller.player_name + "は逃げだした。\nしかし 回り込まれてしまった！" }).then(AttackToPlayer_Run);
            }
        }
        else
            LogController.logController.printText(new string[] { EnemyController.monster_name + "からは逃げることはできない！" }).then(AttackToPlayer_Run);
    }

    public IntAndBool E_damage()
    { // プレイヤーが与えるダメージ
        int e_damage = 0;
        bool kaishin = false;

        // 会心の一撃
        if (UnityEngine.Random.Range(1, 3) == 1)
        {
            e_damage = (int)(PlayerContoroller.player_status["at"] * UnityEngine.Random.Range(0.6f, 1.5f)) + UnityEngine.Random.Range(1, PlayerContoroller.player_status["ag"]);
            kaishin = true;
        }
        else
        {
            if (PlayerContoroller.player_status["at"] - EnemyController.enemy_status["df"] > 0)
            {
                e_damage = (int)(PlayerContoroller.player_status["at"] - EnemyController.enemy_status["df"] * UnityEngine.Random.Range(0.6f, 1.5f)) + UnityEngine.Random.Range(1, PlayerContoroller.player_status["ag"]);
            }
            else
            {
                e_damage = (int)(UnityEngine.Random.Range(1, 3) * UnityEngine.Random.Range(0.6f, 1.5f));
            }
        }

        return new IntAndBool()
        {
            damage = e_damage,
            isCelanHit = kaishin
        };
    }

    public IntAndBool P_damage()
    {
        int p_damage = 0;
        bool tsukon = false;

        // 痛恨の一撃
        if (UnityEngine.Random.Range(1, 32) == 1)
        {
            p_damage = (int)(EnemyController.enemy_status["at"] * UnityEngine.Random.Range(0.6f, 1.5f)) + UnityEngine.Random.Range(1, EnemyController.enemy_status["ag"]);
            tsukon = true;
        }
        else
        {

            if (EnemyController.enemy_status["at"] - PlayerContoroller.player_status["df"] > 0)
            {
                p_damage = (int)(EnemyController.enemy_status["at"] - PlayerContoroller.player_status["df"] * UnityEngine.Random.Range(0.6f, 1.5f)) + UnityEngine.Random.Range(1, EnemyController.enemy_status["ag"]);
            }
            else
            {
                p_damage = (int)(UnityEngine.Random.Range(1, 3) * UnityEngine.Random.Range(0.6f, 1.5f));
            }
        }
        return new IntAndBool()
        {
            damage = p_damage,
            isCelanHit = tsukon
        };
    }

    public void EndCallback(string answer)
    {
        switch (answer)
        {
            case "はい":
                GameObject.Find("GameManager").GetComponent<GameManager>().Load();
                break;
            case "いいえ":
                GameObject.Find("GameManager").GetComponent<GameManager>().SceneChange("main", true);
                break;
        }
    }

    public void AlertCallback()
    {
        AlertController.alertController.ShowAlertByOptions("コンティニュー", "最後にセーブした地点からやり直しますか？", new string[] { "はい", "いいえ" }, EndCallback);
    }


    public void Player_die()
    { // player死亡時に呼ばれる
        sound_box.GetComponent<BattleSoundsController>().Dead();
        LogController.logController.printText(new string[] { PlayerContoroller.player_name + "は死んでしまった。" }).then(AlertCallback);
    }

    public void Enemy_die()
    { // enemy 死亡時に呼ばれる
        sound_box.GetComponent<BattleSoundsController>().Dead();
        PlayerContoroller.player_status["exp"] += EnemyController.enemy_status["get_exp"];
        PlayerContoroller.player_status["money"] += EnemyController.enemy_status["get_money"];

        LogController.Callback callback;//メッセージ表示後実行する関数
        if (Check_lvup())
        {
            callback = Play_lvup;
        }
        else if (Check_drop())
        {
            callback = Drop;
        }
        else
        {
            callback = BackField;
        }

        LogController.logController.printText(new string[] { EnemyController.monster_name + "を倒した。", "経験値を" + EnemyController.enemy_status["get_exp"] + "獲得した。\n" + EnemyController.enemy_status["get_money"] + "円を手に入れた。" })
            .then(callback);
    }

    public bool Check_lvup()
    { // レベルアップ判定
        foreach (int key in ExpController.exp_table.Keys)
        {
            if (PlayerContoroller.player_status["lv"] == key)
            {
                if (PlayerContoroller.player_status["exp"] >= ExpController.exp_table[key])
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void Miss()
    {
        Debug.Log("Miss");
    }

    public void Play_lvup()
    {
        sound_box.GetComponent<BattleSoundsController>().LvUp();
        PlayerContoroller.player_status["lv"] += 1;
        for (int i = 0; i < StatusData.LvupPlayerStatus.GetLength(0); i++)
        {
            if (PlayerContoroller.player_status["lv"] == StatusData.LvupPlayerStatus[i, 0])
            {
                PlayerContoroller.player_status["mhp"] += StatusData.LvupPlayerStatus[i, 1];
                PlayerContoroller.player_status["mmp"] += StatusData.LvupPlayerStatus[i, 2];
                PlayerContoroller.player_status["mat"] += StatusData.LvupPlayerStatus[i, 3];
                PlayerContoroller.player_status["mdf"] += StatusData.LvupPlayerStatus[i, 4];
                PlayerContoroller.player_status["mag"] += StatusData.LvupPlayerStatus[i, 5];
                StatusUpdate();

                LogController.Callback callback;//メッセージ表示後実行する関数
                if (Check_drop())
                {
                    callback = Drop;
                }
                else
                {
                    callback = BackField;
                }

                LogController.logController.printText(new string[] { "レベルアップ！",string.Format ("{0}のレベルが{1}にあがった！\n HP+{2} MP+{3}, ちから+{4} ぼうぎょ+{5} すばやさ+{6}",
                    PlayerContoroller.player_name, PlayerContoroller.player_status["lv"], StatusData.LvupPlayerStatus[i, 1], StatusData.LvupPlayerStatus[i, 2], StatusData.LvupPlayerStatus[i, 3], StatusData.LvupPlayerStatus[i, 4], StatusData.LvupPlayerStatus[i, 5])})
                    .then(callback);
                break;
            }
        }
    }

    public bool Check_drop()
    {
        int random = UnityEngine.Random.Range(0, 100);
        switch (EnemyController.enemy_status["drop_pro"])
        {
            case 0:
                return true; // 100/100
            case 1:
                if (random <= 10) return true; // 10/100
                break;
            case 2:
                if (random <= 20) return true; // 20/100
                break;
            case 3:
                if (random <= 50) return true; // 50/100
                break;
            default:
                break;
        }
        return false;
    }


    public void Drop()
    {
        sound_box.GetComponent<BattleSoundsController>().Drop();
        PlayerContoroller.my_items.Add(EnemyController.enemy_status["drop"]);
        LogController.logController.printText(new string[]{string.Format(string.Format ("{0}は{1}を落としていった！\n{2}は{1}を手に入れた",
            EnemyController.monster_name, ItemList.ItemName (EnemyController.enemy_status ["drop"]), PlayerContoroller.player_name))})
            .then(new LogController.Callback(BackField));
    }

    public void BackField()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().BackScene(true);
    }
}