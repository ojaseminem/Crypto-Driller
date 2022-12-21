using Gameplay.Managers;
using UnityEngine;

namespace Gameplay.Misc
{
    public class PickUp : MonoBehaviour
    {
        [SerializeField] private GameObject pickupEffect;
        [SerializeField] private int coinOrDiamond;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Pickup();
            }
        }

        private void Pickup()
        {
            TurnOffVisibility();

            if (coinOrDiamond == 0)
            {
                GameMenuManager.instance.IncrementCoin();
                AudioManager.instance.PlaySound("Coin");
            }
            else if(coinOrDiamond == 1)
            {
                GameMenuManager.instance.IncrementDiamond();
                AudioManager.instance.PlaySound("Diamond");
            }

            var pickedUpEffect = Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(pickedUpEffect, 2f);
            Destroy(gameObject, 2f);
        }

        private void TurnOffVisibility()
        {
            transform.GetComponent<BoxCollider>().enabled = false;
            transform.GetComponent<ParticleSystem>().Stop(true);
        }
    }
}
