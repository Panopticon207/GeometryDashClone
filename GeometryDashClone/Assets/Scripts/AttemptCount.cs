using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI.Table;


public class AttemptCount : MonoBehaviour
{
    public Text attemptCountText;
    private int attemptCount;

    private void Start()
    {
        ShowAttemptCount();
        SaveAttemptCount();
    }

    public void SaveAttemptCount()
    {
        PlayerPrefs.SetInt("attempcount", attemptCount);
    }

    private void ShowAttemptCount()
    {
        gameObject.SetActive(true);
        if (PlayerPrefs.HasKey("attempcount"))
        {
            attemptCountText.text = PlayerPrefs.GetInt("attempcount").ToString();
            attemptCount = PlayerPrefs.GetInt("attempcount");
            attemptCount += 1;
        }
        else
        {
            PlayerPrefs.SetInt("attempcount", 1);
            attemptCountText.text = PlayerPrefs.GetInt("attempcount").ToString();
            attemptCount = 2;
        }

        transform.DOScale(new Vector3(1, 1,1),1f).OnComplete(() =>
        {
            gameObject.SetActive(false);
        }); 

    }


    private void OnDestroy()
    {
        transform.DOKill();
    }

}
