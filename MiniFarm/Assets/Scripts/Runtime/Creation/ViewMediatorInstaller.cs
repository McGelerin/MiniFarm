using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Runtime.Creation
{
    public class ViewMediatorInstaller<TView, TMediator> : Installer
        where TView : Component
        where TMediator : class, IInitializable, IDisposable, ITickable
    {
        #region ReadonlyFields
        private readonly List<TView> _views = new List<TView>();
        #endregion

        #region Bindings
        public override void InstallBindings()
        {
            var views = Object.FindObjectsByType<TView>(FindObjectsSortMode.InstanceID);
            
            foreach (var view in views)
            {
                _views.Add(view);
                Container.Bind<TView>().FromInstance(view).AsTransient();
            }

            Container.BindInterfacesAndSelfTo<ViewMediatorInitializer<TView, TMediator>>().AsSingle().WithArguments(_views);
        }
        #endregion
    }

    public class ViewMediatorInitializer<TView, TMediator> : IInitializable, IDisposable, ITickable
        where TView : Component
        where TMediator :  class, IInitializable, IDisposable, ITickable
    {
        #region ReadonlyFields
        private readonly DiContainer _container;
        private readonly List<TView> _views;
        private readonly List<TMediator> _mediators = new List<TMediator>();
        #endregion

        #region Constructor
        public ViewMediatorInitializer(DiContainer container, List<TView> views)
        {
            _container = container;
            _views = views;
        }
        #endregion

        #region Core
        public void Initialize() => _views.ForEach(Bindings);
        public void Dispose() => _mediators.ForEach(x => x.Dispose());
        public void Tick() => _mediators.ForEach(x => x.Tick());
        #endregion

        #region Executes
        private void Bindings(TView view)
        {
            DiContainer subContainer = _container.CreateSubContainer();
                
            subContainer.Bind<TView>().FromInstance(view).AsSingle();

            TMediator mediator = subContainer.Instantiate<TMediator>();
            _mediators.Add(mediator);
                
            subContainer.Bind<TMediator>().FromInstance(mediator).AsSingle();
            subContainer.QueueForInject(mediator);
            
            mediator.Initialize();
        }
        #endregion
    }
}