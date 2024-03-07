using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chief : Singleton<Chief>
{
    public UiManager uiManager;
    public bool isMove = false;
    private void Update()
    {
        if (isMove)
        {

            Move();
        }
    }
    public void Move()
    {
        Debug.Log("Move");
        transform.Translate(Vector3.right * 3f * Time.deltaTime);
        Invoke("hide", 1f);

        uiManager.PanelFadeIn();
    }

    void hide()
    {
        gameObject.SetActive(false);
    }
}
