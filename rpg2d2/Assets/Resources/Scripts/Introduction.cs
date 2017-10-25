using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour {

    public string fileName = "test.txt";
    private string firstSceneName = "map_station";

    // Use this for initialization
    void Start () {
        StartCoroutine(Show());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowCallback()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().SceneChange("map_station", true);
    }

    IEnumerator Show()
    {
        yield return new WaitUntil(() => LogController.logController != null);
        LogController.logController.GetComponent<LogController>().printTextByFileName(fileName).then(ShowCallback);
    }
}
