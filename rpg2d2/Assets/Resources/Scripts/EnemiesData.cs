﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesData : MonoBehaviour
{
    public static string[,] westSceneMonsters = new string[,] {
	//[0]NO,[1] name,              [2] HP, [3]MP, [4]attack, [5]guarg, [6]ag, [7]enemy_type, [8] drop_no, [9] get_exp, [10] get_money, [11] drop_probability_type
	    { "-3", "ぬし",                  "10", "10", "4", "6", "4", "2", "27", "15", "30","0"},
        { "0", "ちんぴら",               "5", "0", "3", "2", "2", "0", "1", "2", "2","3"},
        { "1", "こわいひと",             "7", "0", "5", "1", "3", "0", "2", "3", "5","2"},
        { "2", "おくすりまん",           "6", "3", "4", "1", "2", "0", "3", "2", "2","1"},
        { "4", "しゅうきょうか",         "8", "5", "1", "2", "4", "1", "4", "2", "5","3"},
        { "5", "きぎょうせんし",         "2", "0", "5", "1", "1", "0", "5", "4", "5","3"},
        { "6", "げんきなこども",         "2", "0", "2", "1", "5", "0", "6", "2", "1","1"},
        { "7", "うるさいおばさん",       "3", "1", "3", "2", "2", "1", "4", "4", "3","1"},
        { "8", "よっぱらい",             "3", "0", "3", "3", "0", "0", "5", "4", "5","3"},
        { "9", "けもの",                 "2", "0", "5", "0", "6", "0", "7", "4", "0","1"},
    };
    public static string[,] eastSceneMonsters = new string[,] {
        { "0", "がくせい",              "4", "0", "3", "2", "3", "0", "8", "4", "3","3"},
        { "1", "トんだおじさん",        "5", "2", "5", "2", "2", "0", "9", "6", "5","3"},
        { "2", "うるさいおばさんV2",    "5", "2", "4", "4", "3", "1", "4", "6", "6","2"},
        { "3", "かみつきがめ",          "4", "0", "4", "3", "3", "0", "5", "5", "6","3"},
        { "4", "からす",                "2", "0", "7", "0", "10", "0", "7", "7", "0","1"},
        { "5", "こうこうせい",          "6", "0", "5", "5", "4", "0", "8", "6", "2","3"},
    };
    public static string[,] dendai2_1SceneMonsters = new string[,] {
        { "-3", "GOD",                  "77", "60", "15", "3", "0", "3", "28", "100", "100","0"},
        { "0", "でんだいせい",          "5", "2", "4", "3", "3", "0", "8", "5", "3","3"},
        { "1", "うぇい",                "5", "2", "5", "3", "4", "0", "8", "6", "5","3"},
        { "2", "いんきゃ",              "6", "6", "3", "6", "1", "0", "8", "6", "3","3"},
        { "3", "せいそういん",          "5", "6", "4", "5", "3", "0", "11", "7", "7","2"},
        { "4", "かーどげーまー",        "4", "7", "4", "4", "4", "0", "12", "6", "1","2"},
        { "5", "くさいがくせい",        "5", "3", "4", "10", "1", "0", "8", "6", "6","0"},
    };
    public static string[,] dendai2_2SceneMonsters = new string[,] {
        { "-3", "上級でんだいせい",     "20", "20", "7", "2", "8", "1", "24", "20", "40","0"},
        { "0", "でんだいせいV2",        "7", "4", "6", "4", "5", "0", "8", "7", "4","3"},
        { "1", "うぇいV2",              "7", "4", "7", "4", "5", "0", "8", "8", "7","3"},
        { "2", "いんきゃV2",            "8", "8", "4", "8", "1", "0", "8", "8", "6","3"},
        { "3", "じゅんきょうじゅ",      "7", "7", "6", "5", "2", "0", "11", "8", "12","2"},
        { "4", "かーどげーまーV2",      "7", "10", "5", "5", "2", "0", "12", "8", "1","2"},
        { "5", "ほもがき",              "6", "8", "9", "3", "4", "0", "8", "8", "8","2"},
    };
    public static string[,] dendai1_1SceneMonsters = new string[,] {
        { "-3", "がっかたんとう",       "35", "30", "10", "7", "8", "2", "15", "30", "60","0"},
        { "0", "けんきゅうせい",        "8", "5", "8", "5", "5", "0", "8", "8", "5","3"},
        { "1", "りゅうねんせい",        "9", "4", "8", "6", "6", "0", "8", "8", "10","3"},
        { "2", "ざんりゅうとどけ",      "10", "10", "5", "10", "1", "0", "8", "9", "7","3"},
        { "3", "レポートたんとう",      "7", "7", "6", "5", "2", "0", "11", "8", "12","2"},
        { "4", "おきゃくさま",          "7", "10", "5", "5", "2", "0", "12", "8", "1","2"},
    };
    public static string[,] dendai1_2SceneMonsters = new string[,] {
        { "0", "けんきゅうせいV2",      "9", "6", "9", "6", "6", "0", "8", "9", "6","3"},
        { "1", "りゅうねんせいV2",      "10", "6", "10", "7", "7", "0", "8", "10", "15","3"},
        { "2", "ざんりゅうとどけV2",    "2", "20", "18", "3", "0", "0", "8", "16", "10","3"},
        { "3", "レポートたんとうV2",    "10", "10","10", "8", "4", "1", "11", "12", "16","2"},
        { "4", "おきゃくさまV2",        "8", "12", "12", "1", "2", "0", "12", "14", "-100","2"},
    };
    public static string[,] dendai1_3SceneMonsters = new string[,] {
        { "-3", "THEラスボス",          "70", "100", "15", "15", "15", "3", "17", "100", "200","0"},
        { "0", "研究おわらん",          "11", "8", "10", "9", "6", "0", "8", "11", "10","3"},
        { "1", "留年3年目",             "20", "20","9",  "13","5", "0", "8", "13", "20","3"},
        { "2", "残留3日目",             "4", "30", "25", "4", "0", "1", "8", "20", "20","3"},
        { "3", "お客様",                "5", "5", "5", "1", "2", "1", "12", "30", "-120","3"},
        { "4", "教授",                  "15", "12", "12", "10", "7", "0", "12", "25", "30","3"},
    };

    public static string[,] mainSceneMonsters = new string[,] {
		//[0]NO, [1] name, [2] HP, [3]MP, [4]attack, [5]guarg, [6]ag, [7]enemy_type, [8] drop_no, [9] get_exp, [10] get_money, [11] drop_probability_type
		{ "0", "スライム",              "5","0","2","2","3","0","1","1","1","1"},
        { "1", "もりのようせい",        "12", "0","8","4","1","0","1","1","5","2"},
        { "2", "ありせんし",            "7", "0","6","3","4","1","2","2","2","3"},
    };

    public static string[,] GetMonsterList(string sceneName)
    {
        switch (sceneName)
        {
            case "map_west":
                return westSceneMonsters;
            case "map_east":
                return eastSceneMonsters;
            case "map_dendai2_1":
                return dendai2_1SceneMonsters;
            case "map_dendai2_2":
                return dendai2_2SceneMonsters;
            case "map_dendai1_1":
                return dendai1_1SceneMonsters;
            case "map_dendai1_2":
                return dendai1_2SceneMonsters;
            case "map_dendai1_3":
                return dendai1_3SceneMonsters;
            default:
                return mainSceneMonsters;
        }
    }
}