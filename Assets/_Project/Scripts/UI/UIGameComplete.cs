using TMPro;
using UnityEngine;

public class UIGameComplete : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void ShowGameCompleteScore(int score) => this.scoreText.text = "" + score;
}
