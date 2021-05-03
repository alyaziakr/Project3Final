using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSidetoSide : MonoBehaviour
{
	public float speed = 1.5f;
	public float length = 2f;
	public bool IsYAxis = true;
	public bool IsZAxis = false;
    //public bool IsXAxis = false;

	private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
    	rigidbody = GetComponent<Rigidbody>();
    	if (IsYAxis == true)
    	{
        	LeanTween.moveY(gameObject,length, speed).setEaseInOutCubic().setLoopPingPong();
        }
        else if (IsYAxis == false & IsZAxis == false)
        {
        	LeanTween.moveX(gameObject,length, speed).setEaseInOutCubic().setLoopPingPong();
        }
        else
        {
        	LeanTween.moveZ(gameObject,length, speed).setEaseInOutCubic().setLoopPingPong();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "MainCamera")
        {
        	
        	Debug.Log("boop the players head");
        	LeanTween.pause(gameObject);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag =="MainCamera")
        {
        	LeanTween.resume(gameObject);
        }
    }
}

