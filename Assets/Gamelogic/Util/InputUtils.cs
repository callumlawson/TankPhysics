using UnityEngine;

namespace Assets.Gamelogic.Util
{
    public class InputUtils
    {
        public static void SetCursorVisibility(bool visible)
        {
            Cursor.lockState = visible ? CursorLockMode.None : CursorLockMode.Confined;
            Cursor.visible = visible;
        }

        public static bool GetIsMouseInWindow()
        {
            var screenRect = new Rect(0, 0, Screen.width, Screen.height);
            return screenRect.Contains(Input.mousePosition);
        }
    }
}
