using System;
using Runtime.Factory.Data;
using Runtime.Factory.FactoryUI;
using Runtime.Factory.Model;
using Runtime.Identifiers;
using Runtime.Identifiers.Template;
using Runtime.Signals.Production;
using Runtime.Signals.SliderArea;
using UniRx;
using UnityEngine;

namespace Runtime.Factory.FactoryProductionManager
{
    public class FactoryProductionManager : SignalListener
    {
        private readonly SliderAreaView _sliderAreaView;
        private readonly FactoryView _factoryView;
        private readonly FactoryModel _factoryModel;

        public FactoryProductionManager(SliderAreaView sliderAreaView, FactoryView factoryView, FactoryModel factoryModel)
        {
            _sliderAreaView = sliderAreaView;
            _factoryView = factoryView;
            _factoryModel = factoryModel;
        }

        protected override void SubscribeToSignals()
        {
            _signalBus.GetStream<CheckFactoryProductionSignal>()
                .Subscribe(OnCheckFactoryProductionSignal)
                .AddTo(_disposables);

            _signalBus.GetStream<CompleteTimerSignal>()
                .Subscribe(OnCompleteTimerSignal)
                .AddTo(_disposables);
        }

        private void OnCheckFactoryProductionSignal(CheckFactoryProductionSignal signal)
        {
            FactoryVO factoryVo = _factoryView.FactoryVo;
            
            if (!_factoryModel.FactorySaveValues.ContainsKey(factoryVo.FactoryID)) return;

            var factorySaveValue = _factoryModel.FactorySaveValues[factoryVo.FactoryID];

            int producedAmount = GetProducedAmount();
            float remainingTime = GetRemainingTime();
            
            _sliderAreaView.StockText.SetText(producedAmount.ToString());

            if (factoryVo.GainedHarvestAmount * factorySaveValue.CompletedTaskAmount < _factoryView.FactoryVo.HarvestCapacity)
            {
                if (producedAmount * factoryVo.GainedHarvestAmount < factoryVo.HarvestCapacity)
                {
                    _signalBus.Fire(new StartTimerSignal(remainingTime));
                
                    _factoryModel.FactoryCompletedTask(factoryVo.FactoryID, producedAmount , factoryVo.ConsumedResourcesType != ResourcesType.None);
                
                    _sliderAreaView.StockText.SetText((producedAmount * factoryVo.GainedHarvestAmount).ToString());
                }
                else
                {
                    _factoryModel.FactoryCompletedTask(factoryVo.FactoryID,  factoryVo.HarvestCapacity, factoryVo.ConsumedResourcesType != ResourcesType.None);
                
                    _sliderAreaView.CountdownSlider.value = 0;
                    _sliderAreaView.CountdownText.SetText("Full");
                
                    _sliderAreaView.StockText.SetText((factoryVo.HarvestCapacity).ToString());
                }
            }
            else
            {
                _sliderAreaView.CountdownSlider.value = 0;
                _sliderAreaView.CountdownText.SetText("Full");
                
                _sliderAreaView.StockText.SetText((factoryVo.HarvestCapacity).ToString());
            }
            
            if (!_sliderAreaView.CountdownSlider.gameObject.activeSelf && (factorySaveValue.TaskAmount > 0 || factorySaveValue.CompletedTaskAmount > 0))
            {
                _sliderAreaView.CountdownSlider.gameObject.SetActive(true);
            }
        }
        
        private int GetProducedAmount()
        {
            var saveData = _factoryModel.FactorySaveValues[_factoryView.FactoryVo.FactoryID];
            
            double elapsedTime = (DateTime.UtcNow - saveData.StartProductionTime).TotalSeconds;
            return Mathf.Min((int)(elapsedTime / _factoryView.FactoryVo.HarvestingTime), saveData.TaskAmount);
        }
        
        private float GetRemainingTime()
        {
            var saveData = _factoryModel.FactorySaveValues[_factoryView.FactoryVo.FactoryID];
            
            double elapsedTime = (DateTime.UtcNow - saveData.StartProductionTime).TotalSeconds;
            float totalProductionTime = saveData.TaskAmount * _factoryView.FactoryVo.HarvestingTime;

            if (elapsedTime >= totalProductionTime)
            {
                return 0f;
            }

            return (float)(totalProductionTime - elapsedTime) % _factoryView.FactoryVo.HarvestingTime;
        }

        private void OnCompleteTimerSignal(CompleteTimerSignal signal)
        {
            OnCheckFactoryProductionSignal(default);
        }
    }
}