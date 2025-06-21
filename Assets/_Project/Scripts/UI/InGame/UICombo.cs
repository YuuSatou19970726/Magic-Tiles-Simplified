using TMPro;
using UnityEngine;

public class UICombo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI comboCountText;

    public void UpdateComboCount(int count)
    {
        this.comboCountText.text = "x " + count;
    }
}
