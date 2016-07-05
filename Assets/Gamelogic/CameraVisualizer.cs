using Assets.Gamelogic.Util;
using UnityEngine;

namespace Assets.Gamelogic.Visualizers.Controls
{
    public class CameraVisualizer : MonoBehaviour
    {
        public float SensitivityX;
        public float SensitivityY;

        public GameObject OurCamera;
        public GameObject CameraRoot;

        private float LateralRotation;
        private float VerticalRotation;

        private Quaternion _rootAngle;
        private Quaternion _rotateAngle;
      
        void OnEnable()
        {
            OurCamera.gameObject.SetActive(true);
            InputUtils.SetCursorVisibility(false);

            _rootAngle = CameraRoot.transform.rotation;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) &&  Cursor.visible && InputUtils.GetIsMouseInWindow())
            {
                InputUtils.SetCursorVisibility(false);
            } 
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                InputUtils.SetCursorVisibility(true);
            }

            if (Cursor.visible) return;

            LateralRotation = Input.GetAxis("Mouse X") * SensitivityX;
            VerticalRotation = -Input.GetAxis("Mouse Y") * SensitivityY;

            _rootAngle = Quaternion.AngleAxis(LateralRotation, Vector3.up) * Quaternion.AngleAxis(VerticalRotation, _rootAngle * -Vector3.left) * _rootAngle;

            CameraRoot.transform.rotation = _rootAngle;
        }

        void OnDisable()
        {
            OurCamera.gameObject.SetActive(false);
            InputUtils.SetCursorVisibility(true);
        }
    }
}
