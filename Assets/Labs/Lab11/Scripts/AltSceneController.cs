using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AltSceneController : MonoBehaviour
{
    [HideInInspector]
    public GameObject sceneReference;

    private void Start()
    {
        Register();
    }

    public void Register()
    {
        if (AltSceneManager.Instance == null)
        {
            SceneManager.LoadScene("BaseScene", LoadSceneMode.Additive);
            SceneManager.sceneLoaded += HandleSceneLoaded;
        }
        else
        {
            Setup();
        }
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        SceneManager.sceneLoaded -= HandleSceneLoaded;
        Setup();
    }

    private void Setup()
    {
        // Create new object for referencing scene
        SceneManager.SetActiveScene(gameObject.scene);
        sceneReference = new GameObject("SceneRoot");

        Transform contentTransform = AltSceneManager.Instance.sceneContentContainer;
        transform.SetParent(contentTransform, true);
        AltSceneManager.Instance.RegisterScene(this);
    }

    public void Exit()
    {
        // Attach scene back for unloading
        transform.SetParent(sceneReference.transform);
    }
}
