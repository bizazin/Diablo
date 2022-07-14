using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUIComponent : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text difference;
    [SerializeField] private StatsType statsType;

    public Slider Slider { get { return slider; } set { slider = value; } }
    public TMP_Text Difference { get { return difference; } set { difference = value; } }
    public StatsType StatsType { get { return statsType; } set { statsType = value; } }
}
