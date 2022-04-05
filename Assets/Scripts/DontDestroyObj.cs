using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class DontDestroyObj : MonoBehaviour
{
	// Unity Messages -------------------------------------------------------------------------------
	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}
}
