using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;
using System;
using System.IO;
using UnityEngine.SceneManagement;

public class UIScores : MonoBehaviour
{
    private Label[] score_labels = new Label[6];

    private Button backButton;
    void Start()
    {
        string readText = File.ReadAllText("scores.json");
        Score score = Score.CreateFromJSON(readText);
        UIDocument uiDocument = GetComponent<UIDocument>();
        for (int i = 0; i < score.names.Count; i++)
        {
            if (i == 6)
                break;
            score_labels[i] = uiDocument.rootVisualElement.Q<Label>("Score" + (i + 1));
            score_labels[i].text = score.names[i] + "  :  ";
            score_labels[i].text += score.scores[i];
        }
        backButton = uiDocument.rootVisualElement.Q<Button>("Back");
        backButton.clicked += backAction;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void backAction()
    {
        SceneManager.LoadScene("Scenes/SplashScene");
    }
}
