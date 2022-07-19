using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUIComponent : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text textValue;
    [SerializeField] private StatsType statsType;

    public Slider Slider { get { return slider; } set { slider = value; } }
    public TMP_Text TextValue { get { return textValue; } set { textValue = value; } }
    public StatsType StatsType { get { return statsType; } set { statsType = value; } }
}
