using UnityEngine;

namespace Gameplay.Player
{
    public class EndComponentHandler : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.collider.CompareTag($"Ground"))
            {
                Destroy(col.gameObject);
            }
        }
    }
}
