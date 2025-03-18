using System;
using System.Collections.Generic;
using Runtime.Factory.Data;
using Zenject;

namespace Runtime.Factory.Model
{
    public class FactoryModel : IInitializable
    {
        #region Constants
        private const string FACTORY_PATH = "FACTORY_PATH";
        #endregion
        
        private Dictionary<int, FactorySaveDataVO> _factorySaveValues;

        public Dictionary<int, FactorySaveDataVO> FactorySaveValues => _factorySaveValues;

        
        public void Initialize()
        {
            _factorySaveValues = ES3.Load(nameof(FactorySaveValues), FACTORY_PATH, new Dictionary<int, FactorySaveDataVO>());
        }
        
        #region Executes
        
        private void ChangeFactoryValue(int factoryID, FactorySaveDataVO factorySaveData)
        {
            if (_factorySaveValues != null && _factorySaveValues.ContainsKey(factoryID))
            {
                _factorySaveValues[factoryID] = factorySaveData;
            }
            else
            {
                _factorySaveValues.Add(factoryID, factorySaveData);
            }
            
            SaveFactoryValues();
        }

        public void FactoryProductionCollect(int factoryID)
        {
            var factorySaveData = _factorySaveValues[factoryID];

            if (factorySaveData.TaskAmount == 0)
            {
                _factorySaveValues.Remove(factoryID);
            }
            else
            {
                if (factorySaveData.CompletedTaskAmount <= 0) return;
                
                factorySaveData.CompletedTaskAmount = 0;
                factorySaveData.StartProductionTime = DateTime.UtcNow;
            }
        }
        
        public void FactoryCompletedTask(int factoryID, int completedTaskAmount, bool isRawFactory)
        {
            var factorySaveData = _factorySaveValues[factoryID];

            if (isRawFactory)
            {
                factorySaveData.TaskAmount -= (completedTaskAmount - factorySaveData.CompletedTaskAmount);
            }
            
            factorySaveData.CompletedTaskAmount = completedTaskAmount;
            ChangeFactoryValue(factoryID, factorySaveData);
        }
        
        public void FactoryFirstTask(int factoryID, int taskAmount)
        {
            var factorySaveData = new FactorySaveDataVO()
            {
                StartProductionTime = DateTime.UtcNow,
                TaskAmount = taskAmount,
                CompletedTaskAmount = 0
            };
            ChangeFactoryValue(factoryID, factorySaveData);
        }
        
        private void SaveFactoryValues() => ES3.Save(nameof(FactorySaveValues), FactorySaveValues, FACTORY_PATH);
        #endregion
    }
}