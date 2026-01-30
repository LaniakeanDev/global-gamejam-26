using UnityEngine;
using UnityEngine.UIElements;

public class UIDisplay : MonoBehaviour
{
    private float elapsedTime = 0f;

    public float initialTime = 130f;

    public float CurrentHealth = 0.5f;
    private Label timeText;
    private Label endText;
    private VisualElement healthBar;
    private VisualElement masksUI;

    private Label scoreUI;


    private float MAX_CONVICTION = 5f;
    private PlayerController playerController;
    private float playerConviction;
    private float score;
    private int collectedMasks;

    private UIDocument uiDocument;

    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        timeText = uiDocument.rootVisualElement.Q<Label>("TimeLabel");
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        masksUI = uiDocument.rootVisualElement.Q<VisualElement>("Masks");
        scoreUI = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        endText = uiDocument.rootVisualElement.Q<Label>("EndText");
        healthBar.style.width = Length.Percent(0.0f * 100.0f);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        float timeToDisplay = initialTime - elapsedTime;
        if (timeToDisplay < 0)
            timeText.text = "Time out";
        else
            timeText.text = minutes(timeToDisplay) + seconds(timeToDisplay);
        if (timeToDisplay < 0)
            if (Mathf.FloorToInt(-timeToDisplay) % 4 == 3)
                endText.text = "";
            else
                endText.text = "   ---TIME OUT---";
        score = playerController.score;
        scoreUI.text = "Score: " + score;
        playerConviction = playerController.conviction;
        collectedMasks = playerController.collectedMasks;
        float barFill_health = playerConviction / MAX_CONVICTION;
        healthBar.style.width = Length.Percent(barFill_health * 100f);
        masksUI.style.width = collectedMasks * 40;

    }

    string minutes(float t)
    {
        return " " + Mathf.FloorToInt(t / 60f);
    }

    string seconds(float t)
    {
        int i = Mathf.FloorToInt(t) % 60;
        string res = ":";
        if (i < 10)
            res += "0";

        return res + i;
    }
}
