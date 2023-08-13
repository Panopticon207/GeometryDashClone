using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipPlayerController : PlayerController
{
    public override void ControlPlayer()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, playerRb.velocity.y * 2);

        if (Input.GetKey(KeyCode.Space))
        {
            playerRb.gravityScale = -4;
            //if (!particle.isPlaying)
            //    particle.Play();
        }
        else
        {
            playerRb.gravityScale = 4;
            //if (particle.isPlaying)
            //    particle.Stop();
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
