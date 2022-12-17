using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController2D : MonoBehaviour
    {
        // How much to smooth out the movement
        [SerializeField] private float movementSmoothing = 0.1f;

        public Rigidbody2D rb2D;
        private Vector3 _velocity;

        private void Start()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        public void Move(float move)
        {
            Vector3 targetVelocity = new Vector2(move, rb2D.velocity.y);

            rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref _velocity, movementSmoothing);
        }
    }
}