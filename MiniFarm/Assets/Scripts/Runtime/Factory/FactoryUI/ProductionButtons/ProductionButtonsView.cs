using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Factory.FactoryUI.ProductionButtons
{
    public class ProductionButtonsView : MonoBehaviour
    {
        [SerializeField] private Animator productionButtonsAnimator;
        [SerializeField] private Image consumedResourcesTypeImage;
        [SerializeField] private TextMeshProUGUI consumedResourcesAmountText;
        [SerializeField] private Button issueOrderButton;
        [SerializeField] private Button revokeOrderButton;

        public Animator ProductionButtonsAnimator => productionButtonsAnimator;
        public Image ConsumedResourcesTypeImage => consumedResourcesTypeImage;
        public TextMeshProUGUI ConsumedResourcesAmountText => consumedResourcesAmountText;
        public Button IssueOrderButton => issueOrderButton;
        public Button RevokeOrderButton => revokeOrderButton;
    }
}