using DG.Tweening;
using UnityEngine;

namespace Gameplay.Misc
{
    public class KillMachine : MonoBehaviour
    {
        [SerializeField] private float defaultPos;
        [SerializeField] private float initialPos;
        [SerializeField] private float coveredPos;

        private void Start()
        {
            transform.position = new Vector3(0, defaultPos, 0);
        }

        public void GameBegan()
        {
            transform.DOLocalMoveY(initialPos, 4f)
                .OnComplete(() => transform.DOLocalMoveY(defaultPos, 3f));
        }

        public void GameOver()
        {
            transform.DOLocalMoveY(coveredPos, 6f);
        }
    }
}
