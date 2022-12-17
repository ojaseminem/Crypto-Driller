using DG.Tweening;
using UnityEngine;

namespace Gameplay.Handlers
{
    public class PlayButtonHandler : MonoBehaviour
    {
        private Tweener _initialTween;
        [SerializeField] private GameObject settingsButton;
        private void Start()
        {
            _initialTween = transform.DOScale(1.33f, 1f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public void Clicked()
        {
            _initialTween.Kill();
            settingsButton.transform.DOScale(0.001f, .25f);
            transform.parent.DOScale(0.001f, .25f).OnComplete(() => transform.parent.parent.gameObject.SetActive(false));
        }
    }
}
