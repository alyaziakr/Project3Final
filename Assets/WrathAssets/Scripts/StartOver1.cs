using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class StartOver1 : MonoBehaviour
{
	public void OnStart() {  
        AltSceneManager.Instance.SelectScene("WrathIsland");
    }
}
