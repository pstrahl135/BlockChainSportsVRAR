using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score = 0;
   [SerializeField] TMP_Text scoreText;  

    void Start()
    {
        UpdateScoreText();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ball"))
        {
            score++;

            UpdateScoreText();
            collision.gameObject.SetActive(false);

        }
    }

    void UpdateScoreText()
    {

        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
