using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using static SpawnManager;

public class PlayerController : MonoBehaviour
{
    public enum PlayerType
    {
        ClickPlayer,
        SpaceShipPlayer
    }
    public PlayerType playerType;
    //PlayerParameters
    public Rigidbody2D playerRb;
    [SerializeField]
    private float lerpSpeed = 300;
    [SerializeField]
    private float jumpPower = 300;
    private SpawnManager spawnManager;
    public Transform playerTransform;
    public BoxCollider2D playerCollider;


    //RaycastParameters
    [SerializeField]
    private LayerMask groundLayerMask;

    public virtual void SetParameters()
    {
        Time.timeScale = 1;
        playerCollider = playerTransform.GetComponent<BoxCollider2D>();
        playerRb = playerTransform.GetComponent<Rigidbody2D>();
    }
    public virtual void SpacePlayerControl()
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

    public virtual void Roll()
    {
        if (!IsGrounded() && playerType == PlayerType.ClickPlayer)
        {
            playerTransform.rotation = Quaternion.Euler(playerTransform.eulerAngles.x, playerTransform.eulerAngles.y, playerTransform.eulerAngles.z - lerpSpeed * Time.deltaTime);
        }
    }

    public virtual void ControlPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded() && playerType == PlayerType.ClickPlayer)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, jumpPower);
        }
    }

    public virtual void InteractWithPortal(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Portal"))
        {
            Portal portal = collision.gameObject.GetComponent<Portal>();
            if (!portal.isModeChanged)
            {
                portal.isModeChanged = true;
                PlayerType playerTypeToSpawn = portal.playerTypeToSpawn;
                PlayerTypeLibrary playerTypeLibrary = Resources.Load<PlayerTypeLibrary>("PlayerTypes");
                PlayerController playerController = playerTypeLibrary.GetPrefabFromItemType(playerTypeToSpawn);
                Instantiate(playerController, portal.transform.position, portal.transform.rotation);
                Destroy(gameObject);
            }

        }
    }   

    public virtual void ChangeLevelPart(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LevelPartEnder"))
        {
            LevelPartEnder levelEnd = collision.gameObject.GetComponent<LevelPartEnder>();
            if (!levelEnd.isLevelPartEnd)
            {
                levelEnd.isLevelPartEnd = true;
                SpawnManager.instance.SpawnLevelPart();
            }
        }
    }

    public virtual void CrashObstacle(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over");
            GameOver();
        }
    }

    public virtual void GameOver()
    {
        RestartLevel();
        Time.timeScale = 0;      
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    public virtual bool IsGrounded()
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

}
