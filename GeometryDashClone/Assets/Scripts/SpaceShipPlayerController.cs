using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipPlayerController : PlayerController
{
    public override void ControlPlayer()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, playerRb.velocity.y * 2);

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
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
            SoundManager.instance.PlaySound(crushSound, 0.5f);
            GameOver();
        }
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
