using Runtime.Factory.Data;
using UnityEngine;

namespace Runtime.Factory
{
    public class FactoryView : MonoBehaviour
    {
        [SerializeField] private FactoryVO factoryVo;
        public FactoryVO FactoryVo => factoryVo;
    }
}