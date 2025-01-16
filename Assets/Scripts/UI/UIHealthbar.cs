using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthbar : MonoBehaviour
{
    public PlayerController player;

    public RectTransform healthBar;
    Vector2 defaultSize;

    void Awake()
    {
        defaultSize = healthBar.sizeDelta;
    }

    void Update()
    {
        Vector2 newSize = defaultSize;
        newSize.x = Mathf.Lerp(healthBar.sizeDelta.x, newSize.x * player.health / player.maxHealth, Time.deltaTime * 3);
        healthBar.sizeDelta = newSize;
    }
}
