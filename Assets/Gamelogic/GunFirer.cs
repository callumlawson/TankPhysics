//using Assets.Gamelogic.Util;
//using UnityEngine;
//
//namespace Assets.Gamelogic.Visualizers.Combat
//{
//    class GunFirer : ExtendedMonobehaviour
//    {
//        public RectTransform AimPointIndicator;
//        public GameObject BarrelAssembly;
//        public GameObject OurCamera;
//
//        private bool _reloading;
//        private Rigidbody _ourRigidbody;
//
//        void OnEnable()
//        {
//            _ourRigidbody = gameObject.GetComponent<Rigidbody>();
//        }
//
//        private void Update()
//        {
//            RaycastHit hit;
//            var ray = new Ray(BarrelAssembly.transform.position, BarrelAssembly.transform.rotation * Vector3.forward);
//            var madeContact = Physics.Raycast(ray, out hit);
//
//            var possibleHitPoint = madeContact ? hit.point.ToNativeVector() : (Improbable.Math.Vector3?)null;
//            PositionAimIndicator(possibleHitPoint);
//
//            if (!Input.GetMouseButtonDown(0) || Health.IsDead || _reloading) return;
//
//            _reloading = true;
//            Invoke(FinishReload, Gun.ReloadTimeInSeconds);
//
//            long? posibleEntityHit;
//            if (madeContact && hit.collider.gameObject)
//            {
//                posibleEntityHit = HierarchyUtils.GetEntityIdForObject(HierarchyUtils.GetEnclosingGameObject(hit.collider.gameObject).gameObject);
//            }
//            else
//            {
//                posibleEntityHit = null;
//            }
//
//            ApplyRecoilForce();
//            
//            Gun.Update.TriggerFired(new Fired(posibleEntityHit, possibleHitPoint, ray.direction.ToNativeVector3f())).FinishAndSend();
//        }
//
//        private void ApplyRecoilForce()
//        {
//            _ourRigidbody.AddForceAtPosition(BarrelAssembly.transform.rotation*-Vector3.forward*60000,
//                BarrelAssembly.transform.position);
//        }
//
//        private void PositionAimIndicator(Improbable.Math.Vector3? possibleHitPosition)
//        {
//            if (possibleHitPosition == null) return;
//            var aimingRectPosition = OurCamera.GetComponent<Camera>().WorldToScreenPoint(possibleHitPosition.Value.ToUnityVector());
//            AimPointIndicator.anchoredPosition3D = aimingRectPosition;
//        }
//
//        private void FinishReload()
//        {
//            _reloading = false;
//        }
//
//        private void OnDisable()
//        {
//            Destroy(AimPointIndicator);
//        }
//    }
//}
