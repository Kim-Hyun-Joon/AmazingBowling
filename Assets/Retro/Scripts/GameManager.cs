using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent onReset;

    public static GameManager instance;

    public GameObject readyPanel;
    public Text scoreText;
    public Text bestScoreText;
    public Text messageText;

    public bool isRoundActive = false;
    private int score = 0;
    public ShooterRotator shooterRotator;
    public CamFollow cam;


    private void Awake() {
        instance = this;

        UpdateUI();
    }
    private void Start() {
        StartCoroutine("RoundRoutine");
    }

    public void AddScore(int newScore) {
        score += newScore;
        UpdateBestScore();
    }

    void UpdateBestScore() {
        if (GetBestScore() < score) {
            PlayerPrefs.SetInt("BestScore", score);
        }
    }

    int GetBestScore() {
        int bestScore = PlayerPrefs.GetInt("BestScore");
        return bestScore;
    }
    
    void UpdateUI() {
        scoreText.text = "Score : " + score;
        bestScoreText.text = "Bext Score : " + GetBestScore();
    }
    public void OnBallDestroy() {
        UpdateUI();
        isRoundActive = false;
    }
    public void Reset() {
        score = 0;
        UpdateUI();

        //라운드를 다시 처음부터 시작
        StartCoroutine("RoundRoutine");
    }
    IEnumerator RoundRoutine() {
        //Ready
        onReset.Invoke();

        readyPanel.SetActive(true);
        cam.SetTarget(shooterRotator.transform, CamFollow.State.Idle);
        shooterRotator.enabled = false;

        isRoundActive = false;
        messageText.text = "Ready...";

        yield return new WaitForSeconds(3.0f);
        //Play
        isRoundActive = true;
        readyPanel.SetActive(false);
        shooterRotator.enabled = true;
        cam.SetTarget(shooterRotator.transform, CamFollow.State.Ready);

        while(isRoundActive) {
            yield return null;
        }

        //End
        readyPanel.SetActive(true);
        shooterRotator.enabled = false;

        messageText.text = "Wait For Next Round...";

        yield return new WaitForSeconds(3.0f);
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
