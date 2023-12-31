using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;
using static SpawnManager;
using static UnityEngine.ParticleSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerType playerType;
    public float speed;

    [Header("Player Parameters")]
    [SerializeField] protected float lerpSpeed = 300;
    [SerializeField] protected float jumpPower = 300;
    [Header("Raycast Parameters")]
    [SerializeField] protected LayerMask groundLayerMask;
    [Header("ParticleSystem Parameters")]
    [SerializeField] protected ParticleSystem moveParticle;
    [SerializeField] protected ParticleSystem levelEndparticle;
    [Header("Sound Parameters")]
    [SerializeField] protected AudioClip crushSound;


    protected Rigidbody2D playerRb;
    protected Transform playerTransform;
    protected BoxCollider2D playerCollider;

    public enum PlayerType
    {
        ClickPlayer,
        SpaceShipPlayer
    }

    private void Awake()
    {
        Time.timeScale = 1;
        playerRb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<Transform>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    public virtual void ControlPlayer()
    {
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
                PlayerController player = Instantiate(playerController, portal.transform.position, portal.transform.rotation);
                SpawnManager.Instance.currentPlayer = player;
                Destroy(gameObject);
            }

        }
    }   

    public void ChangeLevelPart(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LevelPartEnder"))
        {
            LevelPartEnder levelEnd = collision.gameObject.GetComponent<LevelPartEnder>();

            if (!levelEnd.isLevelPartEnd)
            {
                levelEnd.isLevelPartEnd = true;
                SpawnManager.Instance.SpawnLevelPart();
            }
        }
    }

    public virtual void CrashObstacle(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            SoundManager.instance.PlaySound(crushSound, 0.5f);
            Debug.Log("Game Over");
            GameOver();
        }
    }

    public void GameOver()
    {
        
        RestartLevel();
        Time.timeScale = 0;      
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }

    public virtual void PlayPlayerParticle()
    {

    }
    public virtual void FinishLevel(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LevelEnd"))
        {
            levelEndparticle.gameObject.SetActive(true);
            if (!levelEndparticle.isPlaying)               
                levelEndparticle.Play();
            Time.timeScale = 0.5f;
            Invoke("BackToMainMenu", 3f);
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
