using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipPlayerController : PlayerController
{
    public override void ControlPlayer()
    {
        if (Input.GetKey(KeyCode.Space) && playerType == PlayerType.SpaceShipPlayer)
        {
            playerRb.gravityScale = -4;
        }
        else
        {
            playerRb.gravityScale = 4;
        }
    }

    public override void CrashObstacle(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("GameOver");
            GameOver();
        }
    }

    private void Start()
    {
        SetParameters();
    }

    private void Update()
    {
        ControlPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CrashObstacle(collision);
        ChangeLevelPart(collision);
        InteractWithPortal(collision);
    }
}
