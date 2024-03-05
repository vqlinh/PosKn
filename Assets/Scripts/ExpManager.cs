using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public delegate void ExpChangeHandler(int amount);
    public event ExpChangeHandler onExpChange;

    public static ExpManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void AddExp(int amount)
    {
        onExpChange?.Invoke(amount);
    }
}
