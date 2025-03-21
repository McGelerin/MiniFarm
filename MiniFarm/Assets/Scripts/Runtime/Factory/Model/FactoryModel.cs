using System;
using System.Collections.Generic;
using Runtime.Factory.Data;
using UnityEngine;
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

        public void FactoryProductionCollect(int factoryID, int harvestingTime, bool isRawFactory)
        {
            FactorySaveDataVO factorySaveData = _factorySaveValues[factoryID];

            if (factorySaveData.CompletedTaskAmount == 0) return;

            if (!isRawFactory)
            {
                factorySaveData.TaskAmount -= (factorySaveData.CompletedTaskAmount + factorySaveData.BeforeCompletedTaskAmount);
                if (factorySaveData.TaskAmount == 0)
                {
                    _factorySaveValues.Remove(factoryID);
                }
            }
            
            factorySaveData.CompletedTaskAmount = 0;
            factorySaveData.BeforeCompletedTaskAmount = 0;

            float elapsedTime = 0;
            if (_factorySaveValues.ContainsKey(factoryID))
            {
                elapsedTime = harvestingTime - GetRemainingTime(factoryID, harvestingTime);
                elapsedTime = elapsedTime == harvestingTime ? 0 : elapsedTime;
            }
            
            factorySaveData.StartProductionTime = (DateTime.UtcNow).AddSeconds(-elapsedTime);
        }
        
        public void FactoryCompletedTask(int factoryID, int completedTaskAmount)
        {
            var factorySaveData = _factorySaveValues[factoryID];
            factorySaveData.CompletedTaskAmount = completedTaskAmount;
            ChangeFactoryValue(factoryID, factorySaveData);
        }
        
        public void FactoryFirstTask(int factoryID, int taskAmount)
        {
            var factorySaveData = new FactorySaveDataVO()
            {
                StartProductionTime = DateTime.UtcNow,
                TaskAmount = taskAmount,
                BeforeCompletedTaskAmount = 0,
                CompletedTaskAmount = 0
            };
            ChangeFactoryValue(factoryID, factorySaveData);
        }
        
        public void FactoryIncreaseTask(int factoryID)
        {
            if (!_factorySaveValues.ContainsKey(factoryID))
            {
                FactoryFirstTask(factoryID, 1);
            }
            else
            {
                if (_factorySaveValues[factoryID].TaskAmount == _factorySaveValues[factoryID].CompletedTaskAmount + _factorySaveValues[factoryID].BeforeCompletedTaskAmount)
                {
                    _factorySaveValues[factoryID].TaskAmount++;
                    _factorySaveValues[factoryID].BeforeCompletedTaskAmount += _factorySaveValues[factoryID].CompletedTaskAmount;
                    _factorySaveValues[factoryID].StartProductionTime = DateTime.UtcNow;
                }
                else
                {
                    _factorySaveValues[factoryID].TaskAmount++;
                }
                
                SaveFactoryValues();
            }
        }
        
        public void FactoryDecreaseTask(int factoryID)
        {
            _factorySaveValues[factoryID].TaskAmount--;

            if (_factorySaveValues[factoryID].TaskAmount == 0 && _factorySaveValues[factoryID].CompletedTaskAmount == 0)
            {
                _factorySaveValues.Remove(factoryID);
            }
            
            SaveFactoryValues();
        }
        
        public int GetProducedAmount(int factoryID, int harvestingTime)
        {
            var saveData = _factorySaveValues[factoryID];
            
            double elapsedTime = (DateTime.UtcNow - saveData.StartProductionTime).TotalSeconds;
            return Mathf.Min((int)(elapsedTime / harvestingTime), saveData.TaskAmount);
        }
        
        public float GetRemainingTime(int factoryID, int harvestingTime)
        {
            FactorySaveDataVO saveData = _factorySaveValues[factoryID];
            
            double elapsedTime = (DateTime.UtcNow - saveData.StartProductionTime).TotalSeconds;
            float totalProductionTime = saveData.TaskAmount * harvestingTime;

            if (elapsedTime > totalProductionTime)
            {
                return 0f;
            }

            return (float)(totalProductionTime - elapsedTime) % harvestingTime;
        }

        public int GetCompletedTasks(int factoryID)
        {
            FactorySaveDataVO saveData = _factorySaveValues[factoryID];
            return saveData.CompletedTaskAmount + saveData.BeforeCompletedTaskAmount;
        }
        
        private void SaveFactoryValues() => ES3.Save(nameof(FactorySaveValues), FactorySaveValues, FACTORY_PATH);
        #endregion
    }
}