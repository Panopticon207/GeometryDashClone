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
    private Rigidbody2D playerRb;
    public bool isGrounded;
    [SerializeField]
    private float lerpSpeed = 300;
    [SerializeField]
    private float jumpPower = 300;
    [SerializeField]
    private float fallMultiplier = 300;
    Vector2 gravity;
    [SerializeField]
    private SpawnManager spawnManager;

    private void Start()
    {
        Time.timeScale = 1;
        gravity = new Vector2 (0, -Physics2D.gravity.y);
        playerRb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            playerRb.velocity = new Vector2 (playerRb.velocity.x, jumpPower);
        }

        if (!isGrounded)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - lerpSpeed * Time.deltaTime);
        }
        if (isGrounded)
        {
            

        }

        if (playerRb.velocity.y < 0)
        {
            playerRb.velocity -= gravity * fallMultiplier * Time.deltaTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Vector3 rotation = transform.eulerAngles;
            transform.rotation = Quaternion.Euler(rotation.x, rotation.y, Mathf.Round((rotation.z / 90)) * 90);
            return;
        }

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
}
