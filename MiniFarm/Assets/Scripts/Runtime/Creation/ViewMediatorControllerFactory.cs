using UnityEngine;
using Zenject;

namespace Runtime.Creation
{
    public class ViewMediatorControllerFactory<TController, TMediator, TView>
        where TController : class 
        where TMediator : class 
        where TView : Component
    {
        private readonly DiContainer _container;

        public ViewMediatorControllerFactory(DiContainer container)
        {
            _container = container;
        }

        public TView Create(Object prefab)
        {
            DiContainer subcontainer = InstantiateView(prefab, out TView view);
            TMediator mediator =  InstantiateMediator(subcontainer, view);
            InstantiateController(subcontainer, mediator);
            
            return view;
        }
        
        public TView CreateWithParent(Object prefab, Transform parent)
        {
            DiContainer subcontainer = InstantiateView(prefab, out TView view);
            view.transform.SetParent(parent);
            
            TMediator mediator =  InstantiateMediator(subcontainer, view);
            InstantiateController(subcontainer, mediator);
            
            return view;
        }

        private static void InstantiateController(DiContainer subcontainer, TMediator mediator)
        {
            subcontainer.Bind<TMediator>().FromInstance(mediator);
            TController controller = subcontainer.Instantiate<TController>();
        }

        private DiContainer InstantiateView(Object prefab, out TView view)
        {
            DiContainer subcontainer = _container.CreateSubContainer();
            view = subcontainer.InstantiatePrefabForComponent<TView>(prefab);
            return subcontainer;
        }
        
        private static TMediator InstantiateMediator(DiContainer subcontainer, TView view)
        {
            subcontainer.Bind<TView>().FromInstance(view);
            TMediator mediator = subcontainer.Instantiate<TMediator>();
            return mediator;
        }
    }
}