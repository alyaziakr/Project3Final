using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GreedStartOver : MonoBehaviour
{
    public void OnStart()
    {
        AltSceneManager.Instance.SelectScene("GreedDungeon");
    }
}
