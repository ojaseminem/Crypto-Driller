using System;
using UnityEngine;

namespace Gameplay.Player
{
    public class CameraHandler : MonoBehaviour
    {
        public bool canMove;
        
        [SerializeField] private float movementSpeedMultiplier;

        private bool _toggle;
        
        private int _tempOffset;

        private void FixedUpdate()
        {
            if(canMove)
            {
                if (_toggle)
                {
                    _toggle = false;
                    if (movementSpeedMultiplier < 3.5f) movementSpeedMultiplier += 0.05f * Time.fixedDeltaTime;
                }
                else
                {
                    _toggle = true;
                    if (Time.timeScale < 2f) Time.timeScale += 0.0025f * Time.fixedDeltaTime;
                }
            }
        }

        private void LateUpdate()
        {
            if(canMove) FollowCharacter();
        }

        private void FollowCharacter()
        {
            var self = transform.position;
            transform.position = new Vector3(self.x, self.y - movementSpeedMultiplier * Time.deltaTime);
        }
    }
}
