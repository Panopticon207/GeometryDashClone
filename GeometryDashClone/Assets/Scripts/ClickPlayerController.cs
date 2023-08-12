using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPlayerController : PlayerController
{
    private void Start()
    {
        SetParameters();
    }

    private void Update()
    {
        IsGrounded();
        ControlPlayer();
        Roll();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CrashObstacle(collision);
        ChangeLevelPart(collision);
        InteractWithPortal(collision);
    }
}
