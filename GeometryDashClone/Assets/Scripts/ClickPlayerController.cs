using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPlayerController : PlayerController
{

    
    private void Update()
    {
        IsGrounded();
        ControlPlayer();
        Roll();
        PlayPlayerParticle();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        CrashObstacle(collision);
        ChangeLevelPart(collision);
        InteractWithPortal(collision);
    }

    public void Roll()
    {
        if (!IsGrounded() && playerType == PlayerType.ClickPlayer)
        {
            playerTransform.rotation = Quaternion.Euler(playerTransform.eulerAngles.x, playerTransform.eulerAngles.y, playerTransform.eulerAngles.z - lerpSpeed * Time.deltaTime);
        }
    }

    public bool IsGrounded()
    {
        float extraHeight = 0.1f;
        //RaycastHit2D raycastHit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + extraHeight,groundLayerMask);
        Vector2 playerPosition = playerCollider.bounds.center;
        Vector2 boxSize = new Vector2(playerCollider.bounds.extents.x * 2, extraHeight);
        RaycastHit2D raycastHit = Physics2D.BoxCast(playerPosition, boxSize, 0f, Vector2.down, playerCollider.bounds.extents.y + extraHeight, groundLayerMask);
        Color rayColor;

        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
            Vector3 rotation = playerTransform.eulerAngles;
            playerTransform.rotation = Quaternion.Euler(rotation.x, rotation.y, Mathf.Round((rotation.z / 90)) * 90);
            return raycastHit.collider != null;
        }
        else
        {
            rayColor = Color.red;
        }

        Debug.DrawRay(playerCollider.bounds.center, Vector2.down * (playerCollider.bounds.extents.y + extraHeight), rayColor);
        return raycastHit.collider != null;
    }

    public override void ControlPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpPower);
            
        }
    }

    public override void PlayPlayerParticle()
    {
        particle.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        particle.transform.localPosition = new Vector2(0,0);

        if (IsGrounded())
        {
            //particle.gameObject.SetActive(true);
            if (!particle.isPlaying)
                particle.Play();
        }
        else
        {
            //particle.gameObject.SetActive(false);
            if (particle.isPlaying)
                particle.Stop();
        }
    }
}
