using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] int currentHealth, maxHealth, currentExp, maxExp, currentLevel;

    private void HandleExpChange(int newExp)
    {
        currentExp += newExp;
        if (currentExp >= maxExp)
        {
            LevelUp();
        }
    }
    public void LevelUp()
    {
        maxHealth += 10;
        currentExp = maxHealth;
        currentExp++;
        currentExp = 0;
        maxExp += 100;
    }
    private void OnEnable()
    {
        ExpManager.Instance.onExpChange += HandleExpChange;
    }
    private void OnDisable()
    {
        ExpManager.Instance.onExpChange -= HandleExpChange;
    }
}
