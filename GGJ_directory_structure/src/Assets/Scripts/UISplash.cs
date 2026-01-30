using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;





public class UISplash : MonoBehaviour
{

    private Button startButton;
    private Button scoreButton;
    private Button quitButton;

    private Label title;


    private int count = 0;
    private UIDocument uiDocument;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        startButton = uiDocument.rootVisualElement.Q<Button>("Start");
        scoreButton = uiDocument.rootVisualElement.Q<Button>("Scores");
        quitButton = uiDocument.rootVisualElement.Q<Button>("Quit");
        title = uiDocument.rootVisualElement.Q<Label>("Title");
        quitButton.clicked += quit;
        startButton.clicked += startAction;
        scoreButton.clicked += scoreAction;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (count)
        {
            case 10:
                title.text = "-T";
                break;
            case 40:
                title.text = "--";
                break;
            case 90:
                title.text = "-T";
                break;
            case 120:
                title.text = "--TH";
                break;
            case 170:
                title.text = "---THE";
                break;
            case 200:
                title.text = "----TH";
                break;
            case 230:
                title.text = "----THE";
                break;
            case 280:
                title.text = "----THE ";
                break;
            case 290:
                title.text = "----THE G";
                break;
            case 300:
                title.text = "----THE GQ";
                break;
            case 320:
                title.text = "----THE GQM";
                break;
            case 330:
                title.text = "----THE GQME";
                break;
            case 350:
                title.text = "   ---- THE GQME ----";
                break;

            default:
                break;
        }
        count++;
    }
    void quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    void startAction()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }

    void scoreAction()
    {
        SceneManager.LoadScene("Scenes/ScoresScene");
    }


}
