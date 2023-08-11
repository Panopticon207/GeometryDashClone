using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    public enum PlayerType
    {
        ClickPlayer,
        SpaceShipPlayer
    }
    public PlayerType playerType;
    //ClickPlayerParameters
    public Rigidbody2D clickPlayerRb;
    public bool isGrounded;
    [SerializeField]
    private float lerpSpeed = 300;
    [SerializeField]
    private float jumpPower = 300;
    [SerializeField]
    private float fallMultiplier = 300;
    [SerializeField]
    private SpawnManager spawnManager;
    public Transform playerTransform;
    public BoxCollider2D playerCollider;

    //RaycastParameters
    [SerializeField]
    private LayerMask groundLayerMask;

    Vector2 gravity;


    private void Start()
    {
        Time.timeScale = 1;
        gravity = new Vector2 (0, -Physics2D.gravity.y);
        playerCollider = playerTransform.GetComponent<BoxCollider2D> ();
    }

    private void Update()
    {
        IsGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && playerType == PlayerType.ClickPlayer)
        {
            clickPlayerRb.velocity = new Vector2 (clickPlayerRb.velocity.x, jumpPower);
        }

        if (!IsGrounded() && playerType == PlayerType.ClickPlayer)
        {
            playerTransform.rotation = Quaternion.Euler(playerTransform.eulerAngles.x, playerTransform.eulerAngles.y, playerTransform.eulerAngles.z - lerpSpeed * Time.deltaTime);
        }

        //if (clickPlayerRb.velocity.y < 0)
        //{
        //    clickPlayerRb.velocity -= gravity * fallMultiplier * Time.deltaTime;
        //}

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (clickPlayerCollider.gameObject.CompareTag("Ground"))
        //{
        //    isGrounded = true;
        //    Vector3 rotation = transform.eulerAngles;
        //    transform.rotation = Quaternion.Euler(rotation.x, rotation.y, Mathf.Round((rotation.z / 90)) * 90);
        //    return;
        //}

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            GameOver();
        }
        if (collision.gameObject.CompareTag("LevelPartEnder"))
        {
            LevelPartEnder levelEnd = collision.gameObject.GetComponent<LevelPartEnder>();
            if (!levelEnd.isLevelPartEnd)
            {
                levelEnd.isLevelPartEnd = true;
                spawnManager.SpawnLevelPart();               
            }
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void GameOver()
    {
        //Invoke("RestartLevel", 1f);
        RestartLevel();
        Time.timeScale = 0;      
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    public bool IsGrounded()
    {
        float extraHeight = 0.1f;
        RaycastHit2D raycastHit = Physics2D.Raycast(playerCollider.bounds.center, Vector2.down, playerCollider.bounds.extents.y + extraHeight,groundLayerMask);
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

    public void SwitchPlayerMode()
    {
        if (playerType == PlayerType.ClickPlayer)
        {
            playerType = PlayerType.SpaceShipPlayer;
        }
        if (playerType == PlayerType.SpaceShipPlayer)
        {
            playerType = PlayerType.SpaceShipPlayer;
        }
    }
}
