using System;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Runtime.Creation
{
    public class ViewMonoMediatorFactory<TMediator, TView> : IFactory<UnityEngine.Object, TMediator, TView>
        where TMediator : Component
        where TView : Component
    {
        private readonly DiContainer _container;

        public ViewMonoMediatorFactory(DiContainer container)
        {
            _container = container;
        }

        public TView Create(Object prefab, TMediator mediatorType)
        {
            throw new Exception("View Model Factory Cannot be used with Mediator reference!");
        }

        public TView Create(Object prefab)
        {
            DiContainer subcontainer = _container.CreateSubContainer();

            TView view = subcontainer.InstantiatePrefabForComponent<TView>(prefab);
            subcontainer.Bind<TView>().FromInstance(view);
            subcontainer.Bind<TMediator>().FromNewComponentOn(view.gameObject);
            
            return view;
        }
    }
}