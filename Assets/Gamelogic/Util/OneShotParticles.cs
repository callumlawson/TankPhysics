using UnityEngine;

namespace Assets.Gamelogic.Util
{
    class OneShotParticles : MonoBehaviour
    {
        private void Start()
        {
            var particles = GetComponent<ParticleSystem>();
            particles.Play();
            Destroy(gameObject, particles.duration);
        }
    }
}
