using UnityEngine;

namespace Gameplay.Handlers
{
    public class BarrelHandler : MonoBehaviour
    {
        [SerializeField] private GameObject barrelNormal, barrelDestroyed;
        [SerializeField] private ParticleSystem destroyFx;
        
        public void BarrelDestroyed()
        {
            destroyFx.Play(true);
            Destroy(barrelNormal);
            barrelDestroyed.SetActive(true);
            Destroy(barrelDestroyed, 5f);
        }
    }
}