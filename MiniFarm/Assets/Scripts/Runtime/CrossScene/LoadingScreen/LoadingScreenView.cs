using UnityEngine;
using UnityEngine.UI;

namespace Runtime.CrossScene.LoadingScreen
{
    public class LoadingScreenView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image fillImage;
        public CanvasGroup CanvasGroup => canvasGroup;
        public Image FillImage => fillImage;
    }
}