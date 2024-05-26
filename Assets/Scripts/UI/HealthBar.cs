/**
AUTHOR: Dillon Evans

DESCRIPTION:
A health bar for the player or for enemies.

HOW TO USE:
1. Attach the HealthBar component to an object.
2. Set either the player or the enemy that the health bar is for.
3. Set the healthBar and/or healthText objects.
4. Adjust the settings in the Unity editor.

The health bar and health text objects are expected to be children of a canvas.
**/


using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    [Header("Target")]
    [Tooltip(
        "The player whose health will be used for the health bar.\n" +
        "Either player or enemy should not be null."
    )]
    public PlayerController2 player = null;
    [Tooltip(
        "The enemy whose health will be used for the health bar.\n" +
        "Either player or enemy should not be null."
    )]
    public EnemyController enemy = null;


    [Header("Health Bar")]
    [Tooltip(
        "The image object to be the health bar.\n" +
        "Will not display a health bar if this is null."
    )]
    public Image healthBar = null;
    [Tooltip("Color of the health bar at 0% health.")]
    public Color minimumColor = Color.red;
    [Tooltip("Color of the health bar at 50% health.")]
    public Color medianColor = Color.yellow;
    [Tooltip("Color of the health bar at 100% health.")]
    public Color maximumColor = Color.green;


    [Header("Health Text")]
    [Tooltip(
        "The text object to display health text.\n" +
        "Will not display health text if this is null."
    )]
    public TextMeshProUGUI healthText = null;
    [Tooltip(
        "Format of health text (for string.Format).\n" +
        "{0} is current health.\n" +
        "{1} is maximum health.\n" +
        "{2} is health percentage (0 to 100)."
    )]
    public string textFormat = "{0}/{1} ({2:0}%)";


    private float healthBarWidth = 0f;
    private float healthBarX = 0f;


    private int currentHealth = 0;
    private int maximumHealth = 1;
    private float healthPercent = 0f;


    private void Start()
    {
        if (healthBar)
        {
            RectTransform rt = healthBar.rectTransform;
            healthBarWidth = rt.sizeDelta.x;
            healthBarX = rt.anchoredPosition.x;
        }
    }


    private void Update()
    {
        UpdateHealth();
        UpdateHealthBar();
        UpdateText();
    }


    private void UpdateHealth()
    {
        if (player)
        {
            currentHealth = player.currentHealth;
            maximumHealth = player.initialHealth;
        }
        else if (enemy)
        {
            currentHealth = enemy.currentHealth;
            maximumHealth = enemy.initialHealth;
        }
        else
        {
            currentHealth = 0;
        }
        healthPercent = (float) currentHealth / maximumHealth;
    }


    private void UpdateHealthBar()
    {
        if (!healthBar) return;
        RectTransform rt = healthBar.rectTransform;
        // Change size.
        Vector2 size = rt.sizeDelta;
        size.x = healthBarWidth * healthPercent;
        rt.sizeDelta = size;
        // Change position.
        Vector2 position = rt.anchoredPosition;
        float widthDelta = healthBarWidth - size.x;
        position.x = healthBarX - widthDelta / 2f;
        rt.anchoredPosition = position;
        // Change color.
        Color minColor;
        Color maxColor;
        float percent;
        if (healthPercent <= 0.5f)
        {
            minColor = minimumColor;
            maxColor = medianColor;
            percent = healthPercent * 2f;
        }
        else
        {
            minColor = medianColor;
            maxColor = maximumColor;
            percent = healthPercent * 2f - 1f;
        }
        healthBar.color = Color.Lerp(minColor, maxColor, percent);
    }

    private void UpdateText()
    {
        if (!healthText) return;
        healthText.text = string.Format(
            textFormat,
            currentHealth, maximumHealth, healthPercent * 100f
        );
    }
}
