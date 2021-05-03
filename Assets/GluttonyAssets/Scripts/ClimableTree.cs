using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimableTree : MonoBehaviour
{
	private void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			NavigationManager.Instance.CloseToAClimable();
			Debug.Log("close");
		}
	}

	private void OnTriggerExit (Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			NavigationManager.Instance.LeaveAClimable();
			Debug.Log("left");
		}
	}
}