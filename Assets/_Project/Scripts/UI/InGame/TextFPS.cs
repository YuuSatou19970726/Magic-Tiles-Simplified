using TMPro;
using UnityEngine;

public class TextFPS : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textFPS;

    private void Awake()
    {
        // InvokeRepeating(nameof(this.UpdateFPS), 0f, 1f);

        // Set frame rate to default
        // Application.targetFrameRate = -1;

        // Set target FPS in game
        // Application.targetFrameRate = 120;
    }

    private void UpdateFPS()
    {
        float fps = 1 / Time.deltaTime;
        this.textFPS.text = fps.ToString("F2");
    }

    public void ShowFPS(bool enable)
    {
        this.textFPS.gameObject.SetActive(enable);

        if (enable)
            InvokeRepeating(nameof(this.UpdateFPS), 0f, 1f);
        else
            CancelInvoke();
    }
}