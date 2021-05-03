using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyToHotspot1 : MonoBehaviour
{
	public LayerMask hotspotLayer;

	private void FixedUpdate()
	{
#if UNITY_EDITOR
		FlyInEditor();
#else
		FlyInPhone();
#endif

	}
	private void FlyInEditor()
	{
		Ray ray;
		RaycastHit hit;

		if (Input.GetMouseButtonDown(0))
		{
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if(Physics.Raycast(ray, out hit, 50f, hotspotLayer))
			{
				Debug.Log("hit a hotspot");
				ARhotspotWrath aRHotspot = hit.collider.GetComponent<ARhotspotWrath>();
				if (aRHotspot)
				{
					aRHotspot.OnTap();
				}
			}
		}
	}
	private void FlyInPhone()
	{
		if (Input.touchCount==0) return;

		Ray ray;
		RaycastHit hit; 

		if (Input.GetTouch(0).phase == TouchPhase.Began)
		{
			ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			if(Physics.Raycast(ray, out hit, 50f, hotspotLayer))
			{
				Debug.Log("hit a hotspot");
				ARhotspotWrath aRHotspot = hit.collider.GetComponent<ARhotspotWrath>();
				if (aRHotspot)
				{
					aRHotspot.OnTap();
				}
			}
		}
	}
}
