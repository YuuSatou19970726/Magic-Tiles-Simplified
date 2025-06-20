using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public void UpdateScore(int score, string scoreType)
    {
        this.scoreText.text = "" + score;
    }
}
