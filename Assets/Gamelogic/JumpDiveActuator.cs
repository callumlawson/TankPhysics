//using Assets.Gamelogic.Util;
//using UnityEngine;
//
//namespace Assets.Gamelogic.Visualizers.Physical
//{
//    class JumpDiveActuator : ExtendedMonobehaviour
//    {
//        public float JumpDriveForce;
//        public ParticleSystem JumpVFX;
//        public AudioSource JumpSFX;
//
//        private Rigidbody _ourRidgidbody;
//
//        private bool _jumpDriveReset = true;
//
//        void OnEnable()
//        {
//            _ourRidgidbody = gameObject.GetComponent<Rigidbody>();
//        }
//
//        void Update()
//        {
//            if (Input.GetKeyDown(KeyCode.Space) && _jumpDriveReset)
//            {
//                JumpVFX.Play();
//                JumpSFX.Play();
//                _ourRidgidbody.AddForceAtPosition(transform.up * JumpDriveForce, _ourRidgidbody.worldCenterOfMass, ForceMode.Impulse);
//                _jumpDriveReset = false;
//                Invoke(ResetJumpDrive, 4.0f);
//            }
//        }
//
//        private void ResetJumpDrive()
//        {
//            _jumpDriveReset = true;
//        }
//    }
//}
