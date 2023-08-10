using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeat : MonoBehaviour
{
    [SerializeField]
    private float respawnPoint;
    [SerializeField]
    private GameObject otherBackGround;

    private BoxCollider2D col;
    private float backGroundWidth;



    private void Start()
    {
        col = GetComponent<BoxCollider2D>();
        backGroundWidth = col.bounds.size.x;
    }


    private void Update()
    {
        if (transform.position.x < respawnPoint)
        {
            transform.position = new Vector2(otherBackGround.transform.position.x + backGroundWidth -1, otherBackGround.transform.position.y);
        }     
    }




}
