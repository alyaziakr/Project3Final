using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RedBlueGames.Tools.TextTyper;

public class OpeningController : MonoBehaviour
{
    public TMP_Text title;
    public TMP_Text openingText;
    public GameObject startButton;

    public TextTyper textTyper;
    public Dialogue dialogue;
    private int currentLineIndex;

    void Start()
    {
        // Rotate itself to facing camera
        Camera cam = Camera.main;
        Vector3 camRotateion = cam.transform.eulerAngles;
        camRotateion.x = camRotateion.z = 0;
        transform.eulerAngles = camRotateion;

        LeanTween.value(0, 1f, 1f).setOnUpdate((float value) =>
        {
            title.alpha = value;
        });

        Invoke("HideTitle", 5f);
    }

    void HideTitle()
    {
        LeanTween.value(1f, 0f, 1f).setOnUpdate((float value) =>
        {
            title.alpha = value;
        }).setOnComplete(() =>
        {
            title.gameObject.SetActive(false);

            openingText.gameObject.SetActive(true);
            ShowScript();
        });
    }

    private void Update()
    {
        if (title.gameObject.activeSelf) return;

        if (Input.GetButtonDown("Fire1"))
        {
            if (textTyper.IsSkippable() && textTyper.IsTyping)
            {
                textTyper.Skip();
            }
            else
            {
                ShowScript();
            }
        }
    }

    private void ShowScript()
    {
        if (currentLineIndex >= dialogue.lines.Length)
        {
            startButton.SetActive(true);
            return;
        }

        textTyper.TypeText(dialogue.lines[currentLineIndex]);
        currentLineIndex++;
    }

    public void OnStart()
    {
        Debug.Log("start!");
        AltSceneManager.Instance.NextScene();
    }

}
