using Assets.Gamelogic.Util;
using UnityEngine;

namespace Assets.Gamelogic.Visualizers
{
    class LocalPlayerSetup : MonoBehaviour
    {
        public bool IsLocalPlayer;

        void OnEnable()
        {
            HierarchyUtils.SetLayerRecursively(gameObject, LayerMask.NameToLayer("Ignore Raycast"));
            IsLocalPlayer = true;
        }

        void OnDisable()
        {
            HierarchyUtils.SetLayerRecursively(gameObject, LayerMask.NameToLayer("Default"));
            IsLocalPlayer = false;
        }
    }
}
