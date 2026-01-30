using UnityEngine;
using UnityEngine.UIElements;
using System;
using System.IO;
using UnityEngine.SceneManagement;


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
    public UIDocument pauseDocument;
    public UIDocument deathDocument;
    private Button leaveButton;
    private Button resumeButton;


    private float timeOfDeath;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = State.Starting;
        setButtons();
        pauseDocument.gameObject.SetActive(false);
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

        if (GameManager.state == State.Starting && Time.time > 1)
        {
            GameManager.state = State.Play;
            pause();
        }
        if (GameManager.state == State.Dead && Time.time > timeOfDeath + 3f)
        {
            SceneManager.LoadScene("Scenes/SplashScene");
        }

    }

    void die()
    {
        deathDocument.gameObject.SetActive(true);
        GameManager.state = State.Dead;
        timeOfDeath = Time.time;
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
