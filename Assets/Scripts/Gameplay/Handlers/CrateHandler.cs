using System.Collections;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Handlers
{
    public class CrateHandler : MonoBehaviour
    {
        [SerializeField] private BarrelHandler barrelHandler;
        [SerializeField] private PlayerAnimEventHandler playerAnimEventHandler;
        [SerializeField] private ParticleSystem hitFx, explosionFx;
        [SerializeField] private GameObject brokenCrate;
        [SerializeField] private Animator towerAnim;

        public void BeginPlay()
        {
            StartCoroutine(Delay());
            IEnumerator Delay()
            {
                yield return new WaitForSeconds(.35f);
                hitFx.Play(true);
                transform.GetComponent<Animator>().SetTrigger("Drop");
                towerAnim.SetTrigger("Drop");
            }
        }

        private void ShakeScreen()
        {
            transform.GetComponent<MeshRenderer>().enabled = false;
            brokenCrate.SetActive(true);
            Destroy(brokenCrate, 5f);
            explosionFx.Play(true);
            ScreenShakeHandler.instance.ScreenShake(1f);
        }
        
        private void ContinuePlay()
        {
            barrelHandler.BarrelDestroyed();
            playerAnimEventHandler.SwitchToJump();
        }
    }
}