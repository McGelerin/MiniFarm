using UnityEngine;

namespace Runtime.Extensions
{
    public static class CanvasGroupExtensions
    {
        public static void ChangeActivation(this CanvasGroup canvasGroup, bool isActive)
        {
            canvasGroup.alpha = isActive ? 1 : 0;
            canvasGroup.blocksRaycasts = isActive;
            canvasGroup.interactable = isActive;
        }
    }
}