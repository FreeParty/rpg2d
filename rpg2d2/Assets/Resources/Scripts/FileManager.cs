
/**
 FileTest
   Copyright (c) 2017 templa00
    This software is released under the MIT License.
    http://opensource.org/licenses/mit-license.php
*/

// データを初期化するか？
//#define DATA_INIT

using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// ファイルマネージャー
/// </summary>
public class FileManager
{
    // StreamingAssetsパス
#if UNITY_EDITOR
    // StreamingAssetsのパス
    public static string StreamingAssetsPath = Application.dataPath + "/StreamingAssets";

    // Android
#elif UNITY_ANDROID
    // パス(Android)
	public static string StreamingAssetsPath = "jar:file://" + Application.dataPath + "!/assets";


    // iOS
#elif UNITY_IPHONE
    public static string StreamingAssetsPath = path = Application.dataPath + "/Raw";


#else
    public static string StreamingAssetsPath = Application.dataPath + "/StreamingAssets";
#endif

    /// <summary>
    /// ファイル読み込み
    /// </summary>
    /// <param name="callback">読み込んだ結果をコールバックする</param>
    /// <param name="_file_path">ファイルパス</param>
    /// <returns></returns>
	public static bool is_init_load = false;
	public static string[] result2;
    public static IEnumerator ReadFileText (Action<string[]> callback, string _file_path)
    {
        // 結果
        string result = string.Empty;

        // ファイル
        FileInfo file;

        // 初回読み込みか？

        // 保存データパス
        var save_file_path = Application.persistentDataPath + _file_path;

        // 初回起動時ロードパス
        var init_file_path = StreamingAssetsPath + _file_path;

        // 読み込み先パス
        string load_path = string.Empty;

        

        // 保存先にデータがある場合
        if (File.Exists(save_file_path))
        {

            // データを初期化する場合
#if DATA_INIT

            Debug.Log("強制的に初回読み込み");

            // 初回起動時と同じ処理をするようにする
            is_init_load = true;

            // セーブデータ読み込み
            file = new FileInfo(save_file_path);
            
            // ファイル削除
            file.Delete();
            
            // 初回パスデータを取得する
            load_path = init_file_path;
#else
            Debug.Log("2回目以降の読み込み");

            // セーブデータのパスを設定する
            load_path = save_file_path;
#endif
        }
        // 初回起動時
        else
        {
            Debug.Log("初回読み込み");

            is_init_load = true;

            // 初回パスデータを取得する
            load_path = init_file_path;
        }

        Debug.Log("データ読込パス : " + load_path);

        // win or ios
#if UNITY_EDITOR || UNITY_IPHONE

        // JSONファイルを読み込む
        file = new FileInfo(load_path);
        using (StreamReader sr = new StreamReader(file.OpenRead(), Encoding.UTF8))
        {
			result2 = sr.ReadToEnd().Replace("#player_name#", PlayerContoroller.player_name).Split(new string[] { "\n+_new_+\n" }, StringSplitOptions.RemoveEmptyEntries);
        }
        yield return new WaitForSeconds(0f);
        // Android
#elif UNITY_ANDROID
        // 初回ロードの場合
		List<String> description = new List<String>();

		if (is_init_load)
        {
			WWW www = new WWW(load_path);
            /// wwwの通信が終わるまで待機
            yield return www;

            string txtBuffer = string.Empty;
            TextReader txtReader = new StringReader(www.text);
            while ((txtBuffer = txtReader.ReadLine()) != null)
            {
				description.Add(txtBuffer.Replace("#player_name#", PlayerContoroller.player_name) + "\r\n");
            }
			result2 = description.ToArray();
        }
        // 初回ロードではない場合
        else
        {
            // ファイルを読み込む
			file = new FileInfo(save_file_path);
            using (StreamReader sr = new StreamReader(file.OpenRead(), Encoding.UTF8))
            {
				result2 = sr.ReadToEnd().Replace("#player_name#", PlayerContoroller.player_name).Split(new string[] { "\n+_new_+\n" }, StringSplitOptions.RemoveEmptyEntries);
            }
            yield return new WaitForSeconds(0f);
        }
#endif
        callback(result2);
    }

    /// <summary>
    /// jsonファイルを上書きする
    /// </summary>
    /// <param name="_folda_path">フォルダパス</param>
    /// <param name="_file_name">ファイル名</param>
    /// <param name="_contents">上書きする文字列</param>
    public static void WriteText (string _folda_path, string _file_name, string _contents)
    {
        // 保存フォルダパス
        var save_folda_path = Application.persistentDataPath + _folda_path;

        // 保存データパス
        var save_path = Application.persistentDataPath + _folda_path + _file_name;

        // フォルダがある場合
        if (Directory.Exists(save_folda_path))
        {
            Debug.Log("フォルダがあります");
        }
        else
        {
            Debug.Log("フォルダが無いので作成します");

            // ディレクトリ作成
            Directory.CreateDirectory(save_folda_path);
        }

        // 保存先 persistentDataPath
        // [win] : C:/Users/HomePC/AppData/LocalLow/DefaultCompany/プロジェクト名/save_data.json
        // [Android] : /data/app/xxxx.apk
        // [ios] : /var/mobile/Applications/xxxxxx/myappname.app/Data
        FileInfo file = new FileInfo(save_path);

        Debug.Log("保存先パス : " + save_path);

        // ファイルがある場合
        if (File.Exists(save_path))
        {
            // 削除
            file.Delete();
        }

        // 書き込み用ストリーム
        StreamWriter stream;

        stream = file.AppendText();      // StreamWriter を作成
        stream.WriteLine(_contents);     // 書き込み
        stream.Flush();                  // バッファ書き込み
        stream.Close();                  // 閉じる
    }
}

