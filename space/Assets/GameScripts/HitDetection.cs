using System;
using UnityEngine;

	public class HitDetection : MonoBehaviour {
	public static Collider hitName;
	public void OnTriggerEnter(Collider c){ hitName = c; }
	}


