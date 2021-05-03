using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARhotspotGluttony : MonoBehaviour
{
    private Transform mainCamera;
    private Transform arSessionOrigin;
    void Start()
    {
        mainCamera = Camera.main.transform;
        arSessionOrigin = mainCamera.parent; 
    }

    public void OnTap()
    {
    	float distance = Vector3.Distance(transform.position, mainCamera.position);

    	Vector3 destination = transform.position - mainCamera.localPosition;

        Vector3 heightOffset = new Vector3(0,1.5f,0);

    	LeanTween.move(arSessionOrigin.gameObject, destination+ heightOffset, distance / 4f);
    }
}
