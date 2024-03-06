using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loading : MonoBehaviour
{
    private Animator animator;
    public static Loading Instance;
    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    public void LoadingOpen()
    {
        animator.SetBool(Const.isLoading,true);
    }

    public void LoadingClose()
    {
        animator.SetBool(Const.isLoading, false);
    }
}

