using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : MonoBehaviour {

	public static Dictionary<int, int> exp_table = new Dictionary<int, int> () { 
		{1 ,1},
		{2, 26},
		{3, 50},
		{4, 90},
		{5, 130}
	};
}
