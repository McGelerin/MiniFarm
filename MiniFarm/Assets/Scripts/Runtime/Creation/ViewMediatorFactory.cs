using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Runtime.Creation
{
    public class ViewMediatorFactory<TMediator, TView>
        where TMediator : class 
        where TView : Component
    {
        private readonly DiContainer _container;

        public ViewMediatorFactory(DiContainer container)
        {
            _container = container;
        }
        
        public TView Create(Object prefab)
        {
            var subcontainer = InstantiateView(prefab, out var view);
            InstantiateMediator(subcontainer, view);

            return view;
        }
        
        public TMediator CreateWithParent(Object prefab, Transform setParent, bool resetScale = false)
        {
            var subcontainer = InstantiateView(prefab, out var view);
            
            view.transform.SetParent(setParent);
            
            if (resetScale)
                view.transform.localScale = Vector3.one;
            
            return InstantiateMediator(subcontainer, view);

            //return view;
        }

        private static TMediator InstantiateMediator(DiContainer subcontainer, TView view)
        {
            subcontainer.Bind<TView>().FromInstance(view);
            TMediator mediator = subcontainer.Instantiate<TMediator>();
            return mediator;
        }

        private DiContainer InstantiateView(Object prefab, out TView view)
        {
            DiContainer subcontainer = _container.CreateSubContainer();
            view = subcontainer.InstantiatePrefabForComponent<TView>(prefab);
            return subcontainer;
        }
    }
}