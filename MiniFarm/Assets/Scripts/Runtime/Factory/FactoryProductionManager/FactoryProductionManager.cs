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

            int producedAmount = _factoryModel.GetProducedAmount(factoryVo.FactoryID, factoryVo.HarvestingTime);
            float remainingTime = _factoryModel.GetRemainingTime(factoryVo.FactoryID, factoryVo.HarvestingTime);
            _factoryModel.FactoryCompletedTask(factoryVo.FactoryID, producedAmount);
            
            int completedTasks = _factoryModel.GetCompletedTasks(factoryVo.FactoryID);

            
            if (factorySaveValue.TaskAmount > _factoryModel.FactorySaveValues[factoryVo.FactoryID].CompletedTaskAmount)
            {
                _signalBus.Fire(new StartTimerSignal(remainingTime));
            }
            else
            {
                if (factoryVo.GainedHarvestAmount * completedTasks < _factoryView.FactoryVo.HarvestCapacity)
                {
                    _signalBus.Fire(new StopTimerSignal());
                    _sliderAreaView.CountdownSlider.value = 0;
                    _sliderAreaView.CountdownText.SetText("");
                }
                else
                { 
                    _signalBus.Fire(new StopTimerSignal());
                    _sliderAreaView.CountdownSlider.value = 0; 
                    _sliderAreaView.CountdownText.SetText("Full");
                }
            }
            
            _sliderAreaView.StockText.SetText((completedTasks * factoryVo.GainedHarvestAmount).ToString());
            
            if (!_sliderAreaView.CountdownSlider.gameObject.activeSelf && (factorySaveValue.TaskAmount > 0 || completedTasks > 0))
            {
                _sliderAreaView.CountdownSlider.gameObject.SetActive(true);
            }
            
            _signalBus.Fire(new CheckTaskTextSignal());
        }

        private void OnCompleteTimerSignal(CompleteTimerSignal signal)
        {
            OnCheckFactoryProductionSignal(default);
        }
    }
}