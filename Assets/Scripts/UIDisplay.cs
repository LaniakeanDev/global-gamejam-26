using UnityEngine;
using UnityEngine.UIElements;

public class UIDisplay : MonoBehaviour
{
    private float elapsedTime = 0f;

    public float CurrentHealth = 1.5f;
    private Label timeText;
    private Label scoreUI;
    private VisualElement healthBar;
    private VisualElement masksUI;
    private float MAX_CONVICTION = 5f;
    private PlayerController playerController;
    private float playerConviction;
    private float score;
    private int collectedMasks;

    private UIDocument uiDocument;

    // private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        timeText = uiDocument.rootVisualElement.Q<Label>("TimeLabel");
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        masksUI = uiDocument.rootVisualElement.Q<VisualElement>("Masks");
        scoreUI = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        healthBar.style.width = Length.Percent(0.0f * 100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        timeText.text = "Time: " + Mathf.FloorToInt(elapsedTime);
        score = playerController.score;
        scoreUI.text = "Score: " + score;
        playerConviction = playerController.conviction;
        collectedMasks = playerController.collectedMasks;
        float barFill_health = playerConviction / MAX_CONVICTION;
        healthBar.style.width = Length.Percent(barFill_health * 100f);
        masksUI.style.width = collectedMasks * 40;

    }
}
