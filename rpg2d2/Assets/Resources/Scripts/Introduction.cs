using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour {

    public string fileName = "introduction.txt";
    private string firstSceneName = "map_station";

    // Use this for initialization
    void Start () {
        StartCoroutine(Show());
        GameObject.Find("Player").GetComponent<SpriteRenderer>().sortingOrder = 14;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowCallback()
    {
        GameObject.Find("Player").GetComponent<SpriteRenderer>().sortingOrder = 4;
        GameObject.Find("GameManager").GetComponent<GameManager>().SceneChange(firstSceneName, true);
    }

    IEnumerator Show()
    {
        yield return new WaitUntil(() => LogController.logController != null);
        LogController.logController.GetComponent<LogController>().printTextByFileName(fileName).then(ShowCallback);
    }
}
