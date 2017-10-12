using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusData : MonoBehaviour {
	public static int[,] LvupPlayerStatus = new int[,] {
		// 0,lv 1,mhp	2,mmp	3,mat, 	4,mdf	5,mag
		{2,		5,		3,		2,		0,		3},
		{3,		15,		23,		12,		10,		34},
		{4,		12,		32,		1,		5,		10},
		{5,		5,		3,		2,		0,		3},
		{6,		5,		3,		2,		0,		3},
	};
}
