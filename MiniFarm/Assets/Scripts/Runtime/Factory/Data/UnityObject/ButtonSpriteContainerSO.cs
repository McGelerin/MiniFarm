using System.Collections.Generic;
using Runtime.Identifiers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Factory.Data.UnityObject
{
    [CreateAssetMenu(fileName = "ButtonSpriteContainer", menuName = "MiniFarm/Data/ButtonSpriteContainer", order = 0)]
    public class ButtonSpriteContainerSO : SerializedScriptableObject
    {
        public Dictionary<ResourcesType, Sprite> ResourcesTypeSprites = new Dictionary<ResourcesType, Sprite>();
    }
}