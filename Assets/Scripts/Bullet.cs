using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //private Transform playerTransform;
    //public Player player;

    //private void Start()
    //{
    //    playerTransform = GameObject.FindGameObjectWithTag(Const.player).transform;
 


    //}

    // Update is called once per frame
    void Update()
    {
        //float distance = Vector2.Distance(transform.position,playerTransform.position);
        //    Debug.Log("distance" +distance);
        //if (distance<1f)
        //{
        //    Destroy(this.gameObject);
        //    player.DamagedState();
        //}
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player");
            collision.gameObject.GetComponent<Player>().DamagedState();
            Destroy(this.gameObject);

        }
    }
}
