using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

using NaughtyAttributes;

public class AltSceneManager : Manager<AltSceneManager>
{
    public AltSceneList sceneList;
    [ReadOnly]
    public AltScene currentScene;
    public GameObject fadeSphere;
    public bool isAR;
    [ShowIf("isAR")]
    public Transform sceneContentContainer;

    private IEnumerator loadCoroutine;
    private bool isLoading;
    private Material fadeMaterial;
    private WaitForSeconds fadeWait;
    private AltSceneController currentSceneController;

    IEnumerator Start()
    {
        fadeWait = new WaitForSeconds(1f);

        fadeMaterial = fadeSphere.GetComponent<Renderer>().material;
        fadeMaterial.color = Color.black;
        fadeSphere.SetActive(true);

#if !UNITY_EDITOR
        if (isAR)
        {
            // Fade out
            LeanTween.alpha(fadeSphere, 0f, 1f).setOnComplete(() =>
            {
                fadeSphere.SetActive(false);
            });
            AltRealityARManager.onExperienceStart += OnFirstStartExperience;
            yield break;
        }
#endif

        // Wait for a second to see if there's a scene loaded already
        yield return fadeWait;

        // If there's no scene, load the first one in the sceneList
        if (currentSceneController == null)
        {
            currentScene = sceneList.scenes[0];
            yield return SceneManager.LoadSceneAsync(currentScene.sceneName, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene.sceneName));
        }
        else
        {
            currentScene = GetSceneFromPath(currentSceneController.sceneReference.scene.path);
        }

        // Fade out
        LeanTween.alpha(fadeSphere, 0f, 1f).setOnComplete(() =>
        {
            fadeSphere.SetActive(false);
        });
    }

    private void OnFirstStartExperience()
    {
#if !UNITY_EDITOR
        AltRealityARManager.onExperienceStart -= OnFirstStartExperience;
        StartCoroutine(LoadFirstScene());
#endif
    }

    IEnumerator LoadFirstScene()
    {
        // Fade in
        fadeSphere.SetActive(true);
        LeanTween.alpha(fadeSphere, 1f, 1f);
        yield return fadeWait;

        // Load scene
        currentScene = sceneList.scenes[0];
        yield return SceneManager.LoadSceneAsync(currentScene.sceneName, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentScene.sceneName));

        // Fade out
        LeanTween.alpha(fadeSphere, 0f, 1f).setOnComplete(() =>
        {
            fadeSphere.SetActive(false);
        });
    }

    private void Update()
    {
        // Demo
        if (Input.GetKeyDown("n") || (Input.touchCount > 1 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            NextScene();
        }
    }

    public void NextScene()
    {
        if (currentScene)
        {
            int currentIndex = sceneList.scenes.IndexOf(currentScene);
            currentIndex++;

            if (currentIndex >= sceneList.scenes.Count)
            {
                currentIndex = 0;
            }

            SetEnvironment(sceneList.scenes[currentIndex]);
        }
        else
        {
            SetEnvironment(sceneList.scenes[0]);
        }
    }

    public void SetEnvironment(AltScene newEnv)
    {
        Debug.Log("Change scene: " + newEnv.sceneName);
        if (newEnv == currentScene) return;
        if (isLoading) return;
        isLoading = true;
        loadCoroutine = LoadEnvironment(newEnv);
        StartCoroutine(loadCoroutine);
    }

    private IEnumerator LoadEnvironment(AltScene newEnv)
    {
        // Fade out
        fadeMaterial.color = Color.clear;
        fadeSphere.SetActive(true);
        LeanTween.alpha(fadeSphere, 1f, 1f);
        yield return fadeWait;

        // Unload
        if (currentSceneController)
        {
            currentSceneController.Exit();
        }

        if (currentScene)
        {
            yield return SceneManager.UnloadSceneAsync(currentScene.sceneName);
            yield return Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }

        // Load
        yield return SceneManager.LoadSceneAsync(newEnv.sceneName, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(newEnv.sceneName));
        currentScene = newEnv;
        isLoading = false;

        // Fade in
        LeanTween.alpha(fadeSphere, 0f, 1f).setOnComplete(() =>
        {
            fadeSphere.SetActive(false);
        });
    }

    public void RegisterScene(AltSceneController newSceneController)
    {
        currentSceneController = newSceneController;
    }

    private AltScene GetSceneFromPath(string path)
    {
        string sceneName = Path.GetFileNameWithoutExtension(path).ToLower();
        foreach (AltScene scene in sceneList.scenes)
        {
            if (sceneName == scene.name.ToLower())
            {
                return scene;
            }
        }
        return sceneList.scenes[0];
    }

    private AltScene GetSceneFromName(string sceneName)
    {
        foreach (AltScene scene in sceneList.scenes)
        {
            if (sceneName == scene.name)
            {
                return scene;
            }
        }
        return sceneList.scenes[0];
    }

    public void SelectScene(string sceneName)
    {
        AltScene nextScene = GetSceneFromName(sceneName);
        SetEnvironment(nextScene);
    }
}
