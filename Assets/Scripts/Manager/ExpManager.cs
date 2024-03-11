using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpManager : Singleton<ExpManager>
{
    public delegate void ExpChangeHandler(int amount);
    public event ExpChangeHandler OnExpChange;
    public void AddExp(int amount)
    {
        OnExpChange?.Invoke(amount);
    }
}
