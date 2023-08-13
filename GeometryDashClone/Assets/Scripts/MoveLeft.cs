using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{

    public float speed;

    private void Start()
    {
        speed = SpawnManager.Instance.currentPlayer.speed;
    }

    private void Update()
    {

        transform.Translate(Vector3.left * Time.deltaTime * speed);

    }


}
