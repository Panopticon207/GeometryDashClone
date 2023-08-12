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
    private SpawnManager spawnManager;
    public Transform playerTransform;
    public BoxCollider2D playerCollider;

    public Transform clickPlayerTransform;

    //RaycastParameters
    [SerializeField]
    private LayerMask groundLayerMask;

    //SpaceShipParameters
    [SerializeField]
    private Transform spaceShipTransform;


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

        if (Input.GetKey(KeyCode.Space) && playerType == PlayerType.SpaceShipPlayer)
        {
            clickPlayerRb.gravityScale = -4;
        }
        else
        {
            clickPlayerRb.gravityScale = 4;
        }

        //if (clickPlayerRb.velocity.y < 0)
        //{
        //    clickPlayerRb.velocity -= gravity * fallMultiplier * Time.deltaTime;
        //}

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
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

        if (collision.gameObject.CompareTag("Portal"))
        {
            Portal portal = collision.gameObject.GetComponent<Portal>();
            if (!portal.isModeChanged)
            {
                SwitchPlayerMode();
                portal.isModeChanged = true;                
            }
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

    public void SwitchPlayerMode()
    {
        if (playerType == PlayerType.ClickPlayer)
        {
            playerType = PlayerType.SpaceShipPlayer;
            playerTransform = spaceShipTransform;
            playerCollider = playerTransform.GetComponent<BoxCollider2D>();
            spaceShipTransform.gameObject.SetActive(true);
            clickPlayerTransform.gameObject.SetActive(false);
            return;
        }

        if (playerType == PlayerType.SpaceShipPlayer)
        {
            playerType = PlayerType.ClickPlayer;
            playerTransform = clickPlayerTransform;
            clickPlayerTransform.gameObject.SetActive(true);
            spaceShipTransform.gameObject.SetActive(false);
            return;
        }
    }
}
