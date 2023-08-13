using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstacle : MonoBehaviour
{
    public ObstacleType obstacleType;
    public PlayerController currentPlayer;
    public float lerpSpeed = 5f;
    public float movingColumnEndPoint;
    public float endPoint;
    public enum ObstacleType
    {
        Square,
        Triangle,
        Platform,
        SmallTriangle,
        HighGround,
        Ground,
        MovingColumn,
    }

    private void Start()
    {
        currentPlayer = SpawnManager.Instance.currentPlayer;
    }

    private void Update()
    {
        if (gameObject == null)
            return;

        if (currentPlayer == null)
            return;

        if (transform.position.x - currentPlayer.transform.position.x < 10)
        {
            StartAnimation();
        }
    }

    public virtual void StartAnimation()
    {
        if (obstacleType == ObstacleType.Square)
        {
            transform.DOMoveY(0.207f, 0.55f).SetEase(Ease.OutQuad);
        }
        else if (obstacleType == ObstacleType.Triangle)
        {
            transform.DOMoveY(-0.1f, 0.55f).SetEase(Ease.OutQuad);
        }
        else if (obstacleType == ObstacleType.MovingColumn)
        {
            transform.DOMoveY(endPoint, .5f).SetEase(Ease.OutQuad);
        }
        
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

}
