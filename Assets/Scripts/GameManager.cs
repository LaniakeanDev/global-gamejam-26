using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


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

        if (GameManager.state == State.Dead && Time.time > timeOfDeath + 3f)
        {
            SceneManager.LoadScene("Scenes/SplashScene");
        }
        if (cancel_action.WasPressedThisFrame())
        {
            if (GameManager.state == State.Pause)
                resume();
            else if (GameManager.state == State.Play)
                pause();
        }

    }

    void die()
    {
        deathDocument.gameObject.SetActive(true);
        GameManager.state = State.Dead;
        timeOfDeath = Time.time;
    }

    void timeout()
    {
        GameManager.state = State.Timeout;
        string readText = File.ReadAllText("scores.json");
        Score score = Score.CreateFromJSON(readText);
        // score.SaveToJson(scores_dict);

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
