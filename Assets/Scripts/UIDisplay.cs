using UnityEngine;
using UnityEngine.UIElements;

public class UIDisplay : MonoBehaviour
{
    private float elapsedTime = 0f;

    public float CurrentHealth = 0.5f;
    private Label timeText;
    private VisualElement healthBar;
    private VisualElement masks;
    private float MAX_CONVICTION = 5f;
    private PlayerController playerController;
    private float playerConviction;

    private UIDocument uiDocument;

    // private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        timeText = uiDocument.rootVisualElement.Q<Label>("TimeLabel");
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        masks = uiDocument.rootVisualElement.Q<VisualElement>("Masks");
        healthBar.style.width = Length.Percent(0.0f * 100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        timeText.text = "Time: " + Mathf.FloorToInt(elapsedTime);
        playerConviction = playerController.conviction;
        float barFill_health = playerConviction / MAX_CONVICTION;
        healthBar.style.width = Length.Percent(barFill_health * 100f);
        float barFill_masks = elapsedTime / 10f;
        masks.style.width = Length.Percent(barFill_masks * 100f);

    }
}
