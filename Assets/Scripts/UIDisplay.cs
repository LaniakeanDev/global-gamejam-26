using UnityEngine;
using UnityEngine.UIElements;

public class UIDisplay : MonoBehaviour
{
    private float elapsedTime = 0f;

    public float CurrentHealth = 0.5f;
    private Label timeText;
    private VisualElement healthBar;
    private VisualElement masks;

    private UIDocument uiDocument;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        timeText = uiDocument.rootVisualElement.Q<Label>("TimeLabel");
        healthBar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        masks = uiDocument.rootVisualElement.Q<VisualElement>("Masks");
        healthBar.style.width = Length.Percent(0.0f * 100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        timeText.text = "Time: " + Mathf.FloorToInt(elapsedTime);
        float barFill_health = elapsedTime / 20f;
        healthBar.style.width = Length.Percent(barFill_health * 100f);
        float barFill_masks = elapsedTime / 10f;
        masks.style.width = Length.Percent(barFill_masks * 100f);

    }
}
