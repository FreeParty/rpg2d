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

    public GameObject log_obj;
    public GameObject name_obj;
    public GameObject hp_obj;
    public GameObject mp_obj;
    public GameObject a_button;
    public GameObject b_button;
    public GameObject sound_box;
    public GameObject commands;

    public class IntAndBool
    {
        public int damage;
        public bool isCelanHit;
    }

    void Start()
    {
        if (log_obj == null)
        {
            log_obj = GameObject.Find("BattleField").transform.Find("LogWindow").gameObject;
        }
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
        }
        else
        {
            if (e_damage.damage > 0)
            {
                messages = new string[] { EnemyController.monster_name + "に" + e_damage.damage + "のダメージを与えた" };
            }
            else
            {
                messages = new string[] { EnemyController.monster_name + "は攻撃をかわした！" };
            }
        }

        log_obj.SetActive(true);
        if (EnemyController.enemy_status["hp"] > 0)
        {
            if (PlayerContoroller.player_status["ag"] > EnemyController.enemy_status["ag"]) //AttackToEnemy => AttackToPlayer => ToggleCommands
            {
                log_obj.GetComponent<LogController>().printText(messages).then(AttackToPlayer);
            }
            else //AttackToPlayer => AttackToEnemy => ToggleCommands
            {
                log_obj.GetComponent<LogController>().printText(messages).then(ToggleCommands);
            }
        }
        else
        {
            log_obj.GetComponent<LogController>().printText(messages).cancel(Enemy_die);
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
        }
        else
        {
            if (p_damage.damage > 0)
            {
                messages = new string[] { EnemyController.monster_name + "から" + p_damage.damage + "のダメージを受けた。" };
            }
            else
            {
                messages = new string[] { PlayerContoroller.player_name + "は攻撃をかわした！" };
            }
        }

        log_obj.SetActive(true);
        if (PlayerContoroller.player_status["hp"] > 0)
        {
            if (PlayerContoroller.player_status["ag"] > EnemyController.enemy_status["ag"]) //AttackToEnemy => AttackToPlayer => ToggleCommands
            {
                log_obj.GetComponent<LogController>().printText(messages).then(ToggleCommands);
            }
            else //AttackToPlayer => AttackToEnemy => ToggleCommands
            {
                log_obj.GetComponent<LogController>().printText(messages).then(AttackToEnemy);
            }
        }
        else
        {
            log_obj.GetComponent<LogController>().printText(messages).cancel(Player_die);
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

    public void Player_die()
    { // player死亡時に呼ばれる
        Debug.Log("player is died");
    }

    public void Enemy_die()
    { // enemy 死亡時に呼ばれる
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

        log_obj.SetActive(true);
        log_obj.GetComponent<LogController>().printText(new string[] { EnemyController.monster_name + "を倒した。", "経験値を" + EnemyController.enemy_status["get_exp"] + "獲得した。\n" + EnemyController.enemy_status["get_money"] + "円を手に入れた。" })
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

                log_obj.SetActive(true);
                log_obj.GetComponent<LogController>().printText(new string[] { "レベルアップ！",string.Format ("{0}のレベルが{1}にあがった！\n HP+{2} MP+{3}, ちから+{4} ぼうぎょ+{5} すばやさ+{6}",
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
            case 1:
                if (random <= 99) return true;
                break;
            case 2:
                if (random <= 99) return true;
                break;
            case 3:
                if (random <= 99) return true;
                break;
        }
        return false;
    }


    public void Drop()
    {
        PlayerContoroller.my_items.Add(EnemyController.enemy_status["drop"]);
        log_obj.SetActive(true);
        log_obj.GetComponent<LogController>().printText(new string[]{string.Format(string.Format ("{0}は{1}を落としていった！\n{2}は{1}を手に入れた",
            EnemyController.monster_name, OpenBoxContoroller.ItemName (EnemyController.enemy_status ["drop"]), PlayerContoroller.player_name))})
            .then(new LogController.Callback(BackField));
    }

    public static void BackField()
    {
        SceneManager.LoadScene("Scene/" + SceneManager2d.current_scene);
    }
}
