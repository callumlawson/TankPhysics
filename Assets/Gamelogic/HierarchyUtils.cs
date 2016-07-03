using Assets.Gamelogic.Visualizers;
using UnityEngine;

namespace Assets.Gamelogic.Util
{
    public static class HierarchyUtils
    {
        public static bool IsLocalPlayer(GameObject gameObject)
        {
            return gameObject.GetComponent<LocalPlayerSetup>().IsLocalPlayer;
        }

        public static void SetLayerRecursively(GameObject gameObject, int newLayer)
        {
            if (gameObject == null)
            {
                return;
            }

            gameObject.layer = newLayer;

            foreach (Transform child in gameObject.transform)
            {
                SetLayerRecursively(child.gameObject, newLayer);
            }
        }
    }
}
