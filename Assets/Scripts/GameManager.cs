using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Linq;
using System.Collections.Generic;


public enum State
{
    Play,
    Starting,
    Pause,
    Timeout,

    Dead
}


public class GameManager : MonoBehaviour
{

    public static State state = State.Starting;

    public static GameManager instance;
    public UIDocument pauseDocument;

    public UIDocument timeoutDocument;
    public UIDocument deathDocument;
    public UIDocument setScoreDocument;
    private Button leaveButton;
    private Button resumeButton;
    private InputAction cancel_action;


    private float timeOfDeath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        cancel_action = InputSystem.actions.FindAction("Cancel");
        state = State.Play;
        setButtons();
        pauseDocument.gameObject.SetActive(false);
        timeoutDocument.gameObject.SetActive(false);
        deathDocument.gameObject.SetActive(false);
        setScoreDocument.gameObject.SetActive(false);
    }

    void setButtons()
    {
        leaveButton = pauseDocument.rootVisualElement.Q<Button>("Abandon");
        resumeButton = pauseDocument.rootVisualElement.Q<Button>("Resume");
        leaveButton.clicked += abandon;
        resumeButton.clicked += resume;
    }

    // Update is called once per frame
    void Update()
    {

        if ((GameManager.state == State.Dead || GameManager.state == State.Timeout) && Time.time > timeOfDeath + 2f)
        {
            SceneManager.LoadScene("Scenes/SplashScene");
        }
        if (cancel_action.WasPressedThisFrame())
        {
            if (GameManager.state == State.Pause)
                resume();
            else if (GameManager.state == State.Play)
                die();
        }

    }

    public void die()
    {
        deathDocument.gameObject.SetActive(true);
        GameManager.state = State.Dead;
        timeOfDeath = Time.time;
    }

    void timeout(int current_score)
    {
        GameManager.state = State.Timeout;
        timeoutDocument.gameObject.SetActive(true);
        string readText = File.ReadAllText("scores.json");
        Score score = Score.CreateFromJSON(readText);
        if (current_score > score.scores[score.names.Count - 1])
        {
            if (score.names.Count >= 6 && current_score > score.scores[5])
            {
                var dict_score = new Dictionary<string, int>();
                for (int i = 0; i < score.names.Count; i++)
                    dict_score.Add(score.names[i], score.scores[i]);
            }
        }
        timeOfDeath = Time.time;
        setScoreDocument.gameObject.SetActive(true);
    }

    void pause()
    {
        if (GameManager.state == State.Play)
        {
            pauseDocument.gameObject.SetActive(true);
            setButtons();
            Time.timeScale = 0;
            GameManager.state = State.Pause;
        }
    }

    void abandon()
    {
        Time.timeScale = 1;
        Debug.Log("abandon");
        SceneManager.LoadScene("Scenes/SplashScene");
    }

    void resume()
    {
        if (GameManager.state == State.Pause)
        {
            Time.timeScale = 1;
            GameManager.state = State.Play;
            pauseDocument.gameObject.SetActive(false);
        }
    }
}
