using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This class presents a number using a TextMeshProUGUI component.
/// </summary>
public class TMPNumberPresenter : MonoBehaviour
{
    [SerializeField]
    private string prefix = "x";
    [SerializeField]
    private TextMeshProUGUI text;
    private int number;
    public int Number
    {
        get => number;
        set
        {
            number = value;
            PresentNumber();
        }
    }

    public void PresentNumber()
    {
        text.text = prefix + number.ToString();
    }
}