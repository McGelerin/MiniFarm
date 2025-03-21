using System;
using Runtime.Factory.Model;
using Runtime.Identifiers;
using Runtime.Identifiers.Template;
using Runtime.Signals.SliderArea;
using UniRx;

namespace Runtime.Factory.FactoryUI.SliderArea
{
    public class TaskTextHandler : SignalListener
    {
        private readonly FactoryView _factoryView;
        private readonly SliderAreaView _sliderAreaView;
        private readonly FactoryModel _factoryModel;

        public TaskTextHandler(FactoryView factoryView, SliderAreaView sliderAreaView, FactoryModel factoryModel)
        {
            _factoryView = factoryView;
            _sliderAreaView = sliderAreaView;
            _factoryModel = factoryModel;
        }

        protected override void SubscribeToSignals()
        {
            _signalBus.GetStream<CheckTaskTextSignal>()
                .Subscribe(OnCheckTaskTextSignal)
                .AddTo(_disposables);
        }

        private void OnCheckTaskTextSignal(CheckTaskTextSignal signal)
        {
            if (_factoryView.FactoryVo.ConsumedResourcesType == ResourcesType.None) return;

            string taskText = String.Empty;

            int factoryID = _factoryView.FactoryVo.FactoryID;
            
            if (_factoryModel.FactorySaveValues.ContainsKey(factoryID))
            {
                int task = _factoryModel.GetCompletedTasks(factoryID) + 
                    (_factoryModel.FactorySaveValues[factoryID].TaskAmount - _factoryModel.FactorySaveValues[factoryID].CompletedTaskAmount);

                taskText = task + "/" + _factoryView.FactoryVo.HarvestCapacity;
            }
            else
            {
                taskText = "0" + "/" + _factoryView.FactoryVo.HarvestCapacity;
            }
            
            _sliderAreaView.TaskText.SetText(taskText);
            
            if (!_sliderAreaView.TaskText.gameObject.activeSelf)
            {
                _sliderAreaView.TaskText.gameObject.SetActive(true);
            }
            
        }
    }
}