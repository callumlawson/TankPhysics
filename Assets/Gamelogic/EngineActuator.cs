using Assets.Gamelogic.Util;
using UnityEngine;

namespace Assets.Gamelogic.Visualizers
{
    public class EngineActuator : ExtendedMonobehaviour
    {
        public float ForwardPower;
        public float TorquePower;

        public GameObject LeftTrackColliders;
        public GameObject RightTrackColliders;

        public AudioSource AccelerateSFX;
        public AudioSource RunSFX;
        public AudioSource StopSFX;

        private Rigidbody _ourRigidbody;
        private Collider[] _leftColliders;
        private Collider[] _rightColliders;
        private int _numCollidersPerTrack;
        private float _groundCheckRayLength;

        private bool _accelerateSoundReset = true;
        private bool _accelerateControlsReset = true;
        private const float TorqueAssistCutoff = 1.0f;
        private static readonly Vector3 RelativeCenterOfMass = new Vector3(0, 1.5f, 0);

        void OnEnable()
        {
            _ourRigidbody = GetComponent<Rigidbody>();
            _leftColliders = LeftTrackColliders.GetComponentsInChildren<Collider>();
            _rightColliders = RightTrackColliders.GetComponentsInChildren<Collider>();
            _numCollidersPerTrack = _leftColliders.Length;
            _groundCheckRayLength = _leftColliders[0].bounds.extents.y / 2.0f + 0.5f;
            _ourRigidbody.centerOfMass = RelativeCenterOfMass;
        }

        private void OnIsDeadUpdated(bool isDead)
        {
            if (isDead)
            {
                RunSFX.Stop();
                StopSFX.Play();
            }
            else
            {
                RunSFX.Play();
            }
        }

        void FixedUpdate()
        {
            ApplyEngineForces();
            PlayAccelerateSFX();
        }

        private void ApplyEngineForces()
        {
            var leftTrackForce = LeftTrackTotalForce()/_numCollidersPerTrack;
            foreach (var trackCollider in _leftColliders)
            {
                ApplyForceIfGrounded(_ourRigidbody, trackCollider, leftTrackForce);
            }

            var rightTrackForce = RightTrackTotalForce()/_numCollidersPerTrack;
            foreach (var trackCollider in _rightColliders)
            {
                ApplyForceIfGrounded(_ourRigidbody, trackCollider, rightTrackForce);
            }

            var ourSpeed = _ourRigidbody.velocity.magnitude;
            if (ourSpeed < TorqueAssistCutoff)
            {
                _ourRigidbody.AddTorque(transform.up*TorqueAssistMagnitude()*Mathf.Clamp(TorqueAssistCutoff - ourSpeed, 0, 1));
            }

            SetEngineVolume(Mathf.Abs(RightTrackTotalForce()) + Mathf.Abs(LeftTrackTotalForce()));
        }

        private void ApplyForceIfGrounded(Rigidbody ourRigidbody, Collider trackCollider, float forceToApply)
        {
            if (IsGrounded(trackCollider))
            {
                ourRigidbody.AddForceAtPosition(trackCollider.transform.rotation * Vector3.forward * forceToApply, trackCollider.transform.position);
               
            }
        }

        private bool IsGrounded(Collider trackCollider)
        {
            var groundCheckRay = new Ray(trackCollider.transform.position, -trackCollider.transform.up);

            RaycastHit hit;
            var grounded = Physics.Raycast(groundCheckRay, out hit, _groundCheckRayLength);

            var debugRayColor = grounded ? Color.green : Color.red;

            UnityEngine.Debug.DrawRay(
                groundCheckRay.origin, 
                groundCheckRay.direction.normalized * _groundCheckRayLength,
                debugRayColor
            );

            return grounded;
        }

        private float LeftTrackTotalForce()
        {
            if (Input.GetKey(KeyCode.W))
            {
                return ForwardPower;
            } 
            if (Input.GetKey(KeyCode.S))
            {
                return -ForwardPower;
            }
            return 0.0f;
        }

        private float RightTrackTotalForce()
        {
            if (Input.GetKey(KeyCode.E))
            {
                return ForwardPower;
            } 
            if (Input.GetKey(KeyCode.D))
            {
                return -ForwardPower;
            }
            return 0.0f;
        }

        private float TorqueAssistMagnitude()
        {
            if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.S))
            {
                return -TorquePower;
            }
            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
            {
                return TorquePower;
            }
            return 0.0f;
        }

        private void SetEngineVolume(float totalForce)
        {
            var forceRatio = (totalForce * 0.15f) / (ForwardPower * 2.0f);
            RunSFX.volume = Mathf.Lerp(RunSFX.volume, forceRatio + 0.25f, 0.05f);
        }

        private void PlayAccelerateSFX()
        {
            if (_accelerateSoundReset && _accelerateControlsReset && Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.E))
            {
                AccelerateSFX.Play();
                _accelerateSoundReset = false;
                _accelerateControlsReset = false;
                Invoke(ResetAccelerationSound, 4.0f);
            }
            else if (!(Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.E)))
            {
                _accelerateControlsReset = true;
            }
        }

        private void ResetAccelerationSound()
        {
            _accelerateSoundReset = true;
        }
    }
}
