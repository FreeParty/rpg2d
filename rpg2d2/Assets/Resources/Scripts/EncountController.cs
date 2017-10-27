using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class EncountController : MonoBehaviour {

	void Start()
	{
	}

    void Update()
    {
        float x = CrossPlatformInputManager.GetAxis("Horizontal"); // X
        float y = CrossPlatformInputManager.GetAxis("Vertical"); //y
        if(x != 0 || y != 0)
        {
            RandomEncount();
        }
    }

    public void Encount()
    {
	    GameObject.Find("BGM Source").GetComponent<BGMcontroller>().EncountSound();
        GameObject.Find("GameManager").GetComponent<GameManager>().SceneChange("battle?mn=-1",true);
    }


	public void RandomEncount()
	{
		int num = Random.Range (1, 200);
		if (num == 50) 
		{
            Encount();
        }
	}
}