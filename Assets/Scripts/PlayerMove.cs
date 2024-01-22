using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private bool canMove = true;

    void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    public void Move()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}
