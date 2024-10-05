using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText; 
    private int score;

    public UnityEvent<int> onScoreChanged;
    void Awake()
    {
        score = 0;
        UpdateScoreUI(); 
    }

    // Method to add score
    public void AddScore(int amount)
    {
        score += amount;
        onScoreChanged.Invoke(score); 
        UpdateScoreUI();
    }
    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
