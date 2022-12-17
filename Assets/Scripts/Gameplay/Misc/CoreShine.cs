using System.Collections;
using DG.Tweening;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Misc
{
    public class CoreShine : MonoBehaviour
    {
        [SerializeField] private PlayerController pc;
        private void Start()
        {
            StartCoroutine(CoreShineGlow());
        }

        private IEnumerator CoreShineGlow()
        {
            var selfSr = transform.GetComponent<SpriteRenderer>();
            yield return new WaitUntil(() => pc.canMove = true);
            yield return new WaitForSeconds(10f);
            selfSr.DOFade(0.7f, 10f).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
