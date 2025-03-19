using System.Collections.Generic;
using Runtime.Identifiers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Currency.Data
{
    [CreateAssetMenu(fileName = "CurrencyDataSO", menuName = "MiniFarm/Data/CurrencyDataSO", order = 0)]
    public class CurrencyDataSO : SerializedScriptableObject
    {
        #region Fields
        [SerializeField] private Dictionary<CurrencyTypes, int> defaultCurrencyValues;
        #endregion

        #region Getters
        public Dictionary<CurrencyTypes, int> DefaultCurrencyValues => defaultCurrencyValues;
        #endregion
    }
}