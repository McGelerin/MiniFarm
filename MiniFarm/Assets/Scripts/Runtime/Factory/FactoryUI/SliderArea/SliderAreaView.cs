using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Factory.FactoryUI
{
    public class SliderAreaView : MonoBehaviour
    {
        [SerializeField] private Slider countdownSlider;
        [SerializeField] private TextMeshProUGUI countdownText;
        [SerializeField] private Image badgeImage;
        [SerializeField] private TextMeshProUGUI stockText;
        [SerializeField] private TextMeshProUGUI taskText;

        public Slider CountdownSlider => countdownSlider;
        public TextMeshProUGUI CountdownText => countdownText;
        public Image BadgeImage => badgeImage;
        public TextMeshProUGUI StockText => stockText;
        public TextMeshProUGUI TaskText => taskText;
    }
}