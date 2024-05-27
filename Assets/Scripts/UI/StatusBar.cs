/**
AUTHOR: Dillon Evans

DESCRIPTION:
A status bar for displaying things like health or mana.

HOW TO USE:
1. Attach the StatusBar component to an object.
2. Set either the player or the enemy that the status bar is for.
3. Set the bar and/or text objects.
4. Adjust the settings in the Unity editor.

The bar and text objects are expected to be children of a canvas.
**/


using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class StatusBar : MonoBehaviour
{
    [Header("Target")]
    [Tooltip(
        "The player whose health will be used for the status bar.\n" +
        "Either player or enemy should not be null."
    )]
    public PlayerController2 player = null;
    [Tooltip(
        "The enemy whose health will be used for the status bar.\n" +
        "Either player or enemy should not be null."
    )]
    public EnemyController enemy = null;


    [Header("Bar")]
    [Tooltip(
        "The image object to be the status bar.\n" +
        "Will not display a status bar if this is null."
    )]
    public Image bar = null;
    [Tooltip("Color of the status bar at 0%.")]
    public Color barMinimumColor = Color.red;
    [Tooltip("Color of the status bar at 50%.")]
    public Color barMedianColor = Color.yellow;
    [Tooltip("Color of the status bar at 100%.")]
    public Color barMaximumColor = Color.green;


    [Header("Text")]
    [Tooltip(
        "The text object to display status text.\n" +
        "Will not display status text if this is null."
    )]
    public TextMeshProUGUI text = null;
    [Tooltip(
        "Format of status text (for string.Format).\n" +
        "{0} is current value.\n" +
        "{1} is maximum value.\n" +
        "{2} is percentage (a float from 0 to 100)."
    )]
    public string textFormat = "{0}/{1} ({2:0}%)";


    [HideInInspector]
    public int currentValue = 0;
    [HideInInspector]
    public int maximumValue = 1;
    [HideInInspector]
    public float valuePercent = 0f;


    private float initialBarWidth = 0f;
    private float initialBarX = 0f;


    private void Start()
    {
        if (bar)
        {
            RectTransform rt = bar.rectTransform;
            initialBarWidth = rt.sizeDelta.x;
            initialBarX = rt.anchoredPosition.x;
        }
    }

    private void Update()
    {
        UpdateValue();
        UpdateBar();
        UpdateText();
    }


    private void UpdateValue()
    {
        if (player)
        {
            currentValue = player.currentHealth;
            maximumValue = player.initialHealth;
        }
        else if (enemy)
        {
            currentValue = enemy.currentHealth;
            maximumValue = enemy.initialHealth;
        }
        else
        {
            currentValue = 0;
        }
        valuePercent = (float) currentValue / maximumValue;
    }

    private void UpdateBar()
    {
        if (!bar) return;
        RectTransform rt = bar.rectTransform;
        // Update size.
        Vector2 size = rt.sizeDelta;
        size.x = initialBarWidth * valuePercent;
        rt.sizeDelta = size;
        // Update position.
        Vector2 position = rt.anchoredPosition;
        float widthDelta = initialBarWidth - size.x;
        position.x = initialBarX - widthDelta / 2f;
        rt.anchoredPosition = position;
        // Update color.
        if (valuePercent <= 0.5f)
        {
            bar.color = Color.Lerp(barMinimumColor, barMedianColor, valuePercent * 2f);
        }
        else
        {
            bar.color = Color.Lerp(barMedianColor, barMaximumColor, valuePercent * 2f - 1f);
        }
    }

    private void UpdateText()
    {
        if (!text) return;
        text.text = string.Format(
            textFormat,
            currentValue, maximumValue, valuePercent * 100f
        );
    }
}
