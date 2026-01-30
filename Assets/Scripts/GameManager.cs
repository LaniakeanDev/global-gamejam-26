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

    Scoring,

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

    public TextField scoreField;
    public Label timeoutLabel;
    private InputAction cancel_action;
    private InputAction validate_action;

    public Dictionary<string, int> dict_score = new Dictionary<string, int>();

    public int score_player;



    private float timeOfDeath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        cancel_action = InputSystem.actions.FindAction("Cancel");
        validate_action = InputSystem.actions.FindAction("Submit");
        state = State.Play;
        setButtons();
        timeoutLabel = pauseDocument.rootVisualElement.Q<Label>("TimeoutText");
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

        if ((GameManager.state == State.Dead || GameManager.state == State.Timeout) && Time.time > timeOfDeath + 3f)
        {
            SceneManager.LoadScene("Scenes/SplashScene");
        }
        if (cancel_action.WasPressedThisFrame())
        {
            if (GameManager.state == State.Pause)
                resume();
            else if (GameManager.state == State.Play)
                pause();
            else if (GameManager.state == State.Scoring)
            {
                Debug.Log(scoreField.value);
                validate_name();
            }
        }
        if (validate_action.WasPressedThisFrame())
            validate_name();
    }

    void validate_name()
    {
        dict_score.Add(scoreField.value, score_player);
        Score score = new Score();
        score.SaveToJson(dict_score);
        Time.timeScale = 1;
        SceneManager.LoadScene("Scenes/ScoresScene");
    }

    public void die()
    {
        if (GameManager.state == State.Play)
        {
            deathDocument.gameObject.SetActive(true);
            GameManager.state = State.Dead;
            timeOfDeath = Time.time + 1f;
        }
    }

    public void timeout(int current_score)
    {
        timeOfDeath = Time.time;
        score_player = current_score;
        GameManager.state = State.Timeout;
        timeoutDocument.gameObject.SetActive(true);
        timeoutLabel = timeoutDocument.rootVisualElement.Q<Label>("TimeoutText");
        string readText = File.ReadAllText("scores.json");
        Score score = Score.CreateFromJSON(readText);
        if ((score.names.Count < 6)
        || (score.names.Count >= 6 && current_score > score.scores[5]))
        {
            timeoutLabel.text = "CONGRATS\nYOU MADE IT\nINTO HIGH SCORES !";
            GameManager.state = State.Scoring;
            Time.timeScale = 0;
            setScoreDocument.gameObject.SetActive(true);
            scoreField = setScoreDocument.rootVisualElement.Q<TextField>("ScoreField");
            dict_score = new Dictionary<string, int>();
            for (int i = 0; i < score.names.Count; i++)
                dict_score.Add(score.names[i], score.scores[i]);
            setScoreDocument.gameObject.SetActive(true);

        }
        else
        {
            timeoutLabel.text = "UNFORTUNATELY YOU\nDID NOT MAKE IT\nINTO HIGH SCORES";
        }

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
