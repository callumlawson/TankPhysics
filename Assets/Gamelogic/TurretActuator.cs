using UnityEngine;

namespace Assets.Gamelogic.Visualizers.Physical
{
    public class TurretActuator : MonoBehaviour
    {
        public GameObject OurCamera;
        public GameObject Turret;
        public GameObject BarrelAssembly;

        public float GunDepression;
        public float GunElevation;
        public float TurretTraverseDegreesPerSecond;

        private float FarDistance = 200.0f;
      
        private void Update()
        {
            var targetPosition = GetTargetPosition();

            TransformBarrel(targetPosition);
            TransformTurret(targetPosition);
        }

        private void TransformBarrel(Vector3 targetPosition)
        {
            var pointProjectedOnBarrelPlane = Math3d.ProjectPointOnPlane(Turret.transform.right, BarrelAssembly.transform.position, targetPosition);

            BarrelAssembly.transform.rotation = Quaternion.RotateTowards(
                BarrelAssembly.transform.rotation,
                Quaternion.LookRotation(pointProjectedOnBarrelPlane - BarrelAssembly.transform.position, Turret.transform.up),
                TurretTraverseDegreesPerSecond*Time.deltaTime
                );

            var barrelAssemblyLocalAngles = BarrelAssembly.transform.localEulerAngles;
            BarrelAssembly.transform.localRotation = Quaternion.Euler(ClampBarrelAngle(barrelAssemblyLocalAngles.x), 0, 0);
        }

        private void TransformTurret(Vector3 targetPosition)
        {
            var pointProjectedOnTurretPlane = Math3d.ProjectPointOnPlane(Turret.transform.up, Turret.transform.position, targetPosition);

            Turret.transform.rotation = Quaternion.RotateTowards(
                Turret.transform.rotation,
                Quaternion.LookRotation(pointProjectedOnTurretPlane - Turret.transform.position, Turret.transform.up),
                TurretTraverseDegreesPerSecond*Time.deltaTime
                );
        }

        private Vector3 GetTargetPosition()
        {
            RaycastHit hit;
            var ray = OurCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.5f));
            return Physics.Raycast(ray, out hit) ? hit.point : ray.GetPoint(FarDistance);
        }

        private float ClampBarrelAngle(float angle)
        {
            return angle < 180 ? Mathf.Clamp(angle, 0, GunDepression) : Mathf.Clamp(angle, 360 - GunElevation, 360);
        }
    }
}