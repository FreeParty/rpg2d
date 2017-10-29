using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour {

    public string fileName = "ending.txt";

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Show());
        GameObject.Find("Player").GetComponent<SpriteRenderer>().sortingOrder = 14;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FadeCallback()
    {
        GameObject.Find("Credit").GetComponent<Image>().enabled = true;
        GameObject.Find("Credit").transform.Find("Panel").gameObject.SetActive(true);
    }

    void ShowCallback()
    {
        GameObject.Find("Player").GetComponent<SpriteRenderer>().sortingOrder = 4;
        StartCoroutine(GameObject.Find("Fade").GetComponent<FadeinController>().StartFadeOut(FadeCallback));
    }

    IEnumerator Show()
    {
        yield return new WaitUntil(() => LogController.logController != null);
        LogController.logController.GetComponent<LogController>().printTextByFileName(fileName).then(ShowCallback);
    }
}
