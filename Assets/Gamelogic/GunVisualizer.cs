//using Combat;
//using Combat.Events.Gun;
//using Improbable.Unity.Common.Core.Math;
//using Improbable.Unity.Visualizer;
//using UnityEngine;
//
//namespace Assets.Gamelogic.Visualizers.Combat
//{
//    class GunVisualizer : MonoBehaviour
//    {
//        [Require] public GunReader Gun;
//
//        public ParticleSystem HitVFX;
//        public ParticleSystem GunFireVFX;
//        public AudioSource GunFireSFX;
//
//        public void OnEnable()
//        {
//            Gun.Fired += OnFired;
//        }
//
//        private void OnFired(Fired fired)
//        {
//            GunFireVFX.Play();
//            GunFireSFX.Play();
//            if (fired.PositionHit != null)
//            {
//                Instantiate(HitVFX, fired.PositionHit.Value.ToUnityVector(), Quaternion.identity);
//            }
//        }
//    }
//}
