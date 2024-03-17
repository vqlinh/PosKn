using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chief : Singleton<Chief>
{
    public bool isMove = false;

    private void Update()
    {
        if (isMove) Move();
    }

    public void Move()
    {
        transform.Translate(Vector3.right * 3f * Time.deltaTime);
        Invoke("hide", 1f);

    }

    void hide()
    {
        gameObject.SetActive(false);
    }
}
