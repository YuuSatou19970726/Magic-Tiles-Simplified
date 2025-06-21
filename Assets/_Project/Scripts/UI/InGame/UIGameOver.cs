using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void ShowGameOverScore(int score) => this.scoreText.text = "" + score;
}