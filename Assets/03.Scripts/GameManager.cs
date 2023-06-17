using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region instance
    public static GameManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnPlay(bool isplay);
    public OnPlay onPlay;

    public float gameSpeed = 2.5f;
    public bool isPlay = false;
    public GameObject playBtn;

    public TMP_Text bestScoreTxt;
    public TMP_Text scoreTxt;
    public int score = 0;

    private void Start()
    {
        bestScoreTxt.text = "best score: " + PlayerPrefs.GetInt("BestScore", 0).ToString();
    }

    IEnumerator AddScore()
    {
        while (isPlay)
        {
            score++;
            scoreTxt.text = "score: " + score.ToString();
            gameSpeed += 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void PlayBtnClick()
    {
        playBtn.SetActive(false);
        isPlay = true;
        onPlay.Invoke(isPlay);
        score = 0;
        scoreTxt.text = score.ToString();
        StartCoroutine(AddScore());
    }

    public void GameOver()
    {
        playBtn.SetActive(true);
        isPlay = false;
        onPlay.Invoke(isPlay);
        StopCoroutine(AddScore());
        if (PlayerPrefs.GetInt("BestScore", 0) < score)
        {
            PlayerPrefs.SetInt("BestScore", score);
            bestScoreTxt.text = "best score: " + score.ToString();
        }
    }
}
