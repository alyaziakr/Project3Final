using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : Manager<NavigationManager>
{
	public float normalHeight = 1.2f;
    private WalkOnSurface walkOnSurface;

    void Start()
    {
    	walkOnSurface = GetComponent<WalkOnSurface>();

#if !UNITY_EDITOR
    walkOnSurface.enabled = true;
#endif

    }

    // Update is called once per frame
    void OnEnable()
    {
        AltRealityARManager.onExperienceStart += OnExperienceStart;
        AltRealityARManager.onExperienceReset += OnExperienceReset;
    }
    void OnDisable()
    {
        AltRealityARManager.onExperienceStart -= OnExperienceStart;
        AltRealityARManager.onExperienceReset -= OnExperienceReset;
    }

    private void OnExperienceStart(){
    	walkOnSurface.enabled = true;
    }
    private void OnExperienceReset(){
    	walkOnSurface.enabled = false;
    }

    public void CloseToAClimable(){
    	walkOnSurface.enabled = false;
    }

    public void LeaveAClimable(){
    	walkOnSurface.enabled = true;
    }
}
