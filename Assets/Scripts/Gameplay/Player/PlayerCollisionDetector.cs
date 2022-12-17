using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerCollisionDetector : MonoBehaviour
    {
        private PlayerController _playerController;

        private void Awake()
        {
            _playerController = transform.parent.GetComponent<PlayerController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Rock"))
            {
                _playerController.DetectObstacle();
            }

            if (other.CompareTag("Prop"))
            {
                _playerController.DetectObstacle();
            }
        }
    }
}
