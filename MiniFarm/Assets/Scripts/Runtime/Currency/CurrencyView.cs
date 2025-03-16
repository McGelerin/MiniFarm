using System.Collections.Generic;
using Runtime.Identifiers;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Runtime.Currency
{
    public class CurrencyView : SerializedMonoBehaviour
    {
        #region Fields
        [SerializeField] private Dictionary<CurrencyTypes, TextMeshProUGUI> currencyTexts;
        #endregion

        #region Getters
        public Dictionary<CurrencyTypes, TextMeshProUGUI> CurrencyTexts => currencyTexts;
        
        #endregion
    }
}