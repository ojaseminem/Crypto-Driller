using System.Collections;
using Gameplay.Handlers;
using Gameplay.Managers;
using ScriptBoy.Digable2DTerrain;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        public bool canMove;

        [SerializeField] private Camera mainCam;
        [SerializeField] private CameraHandler camHandler;
        
        [SerializeField] private Shovel shovel;
        [SerializeField] private float moveSpeed;

        [SerializeField] private GameObject destroyedFx, hitFx;

        [SerializeField] private ParticleSystem dirtPs;

        [SerializeField] private Transform characterPivot;
        [SerializeField] private GameObject characterDestroyed;

        private bool _canTouchMove;
        
        private Vector2 _worldStartPoint;
        private CharacterController2D _characterController;

        private void Start()
        { 
            _characterController = GetComponent<CharacterController2D>();
        }
        
        private void FixedUpdate()
        {
            if (!canMove) return;
            if(!_canTouchMove) return;

            if (Input.touchCount != 1) return;
             
            var currentTouch = Input.GetTouch(0);
            
            if (currentTouch.phase == TouchPhase.Began)
            {
                _worldStartPoint = GetWorldPoint(currentTouch.position);

                Dig();

                var dir = _worldStartPoint - _characterController.rb2D.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
                var tempAngle = Quaternion.Euler(0, 0, angle);
                if(tempAngle.eulerAngles.z is >= 110 and <= 250)
                {
                    characterPivot.rotation = tempAngle;
                }
                _characterController.Move(dir.x * moveSpeed);
            }
            else if (currentTouch.phase == TouchPhase.Moved)
            {
                _worldStartPoint = GetWorldPoint(currentTouch.position);

                ContinuousDig();

                var dir = _worldStartPoint - _characterController.rb2D.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
                var tempAngle = Quaternion.Euler(0, 0, angle);
                if(tempAngle.eulerAngles.z is >= 110 and <= 250)
                {
                    characterPivot.rotation = tempAngle;
                }
                _characterController.Move(dir.x * moveSpeed);
            }
            else if (currentTouch.phase == TouchPhase.Stationary)
            {
                _worldStartPoint = GetWorldPoint(currentTouch.position);

                ContinuousDig();

                var dir = _worldStartPoint - _characterController.rb2D.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
                var tempAngle = Quaternion.Euler(0, 0, angle);
                if(tempAngle.eulerAngles.z is >= 110 and <= 250)
                {
                    characterPivot.rotation = tempAngle;
                }
                _characterController.Move(dir.x * moveSpeed);
            }
        }

        private Vector2 GetWorldPoint (Vector2 screenPoint)
        {
            Physics.Raycast(mainCam.ScreenPointToRay(screenPoint), out var hit);
            return hit.point;
        }

        public void CanTouchMove()
        {
            StartCoroutine(Delay());
            IEnumerator Delay()
            {
                characterPivot.rotation = Quaternion.Euler(0,0, 180);
                yield return new WaitForSeconds(.5f);
                _canTouchMove = true;
                moveSpeed = 5f;
                yield return new WaitForSeconds(5f);
                moveSpeed = 50f;
            }
        }
        
        public void Dig()
        {
            StartCoroutine(DigDelay());
            IEnumerator DigDelay()
            {
                yield return new WaitForSeconds(.1f);
                shovel.Dig();
                yield return new WaitForSeconds(.1f);
            }
        }

        private void ContinuousDig()
        {
            StartCoroutine(DigDelay());
            IEnumerator DigDelay()
            {
                yield return new WaitForSeconds(.2f);
                shovel.Dig();
                yield return new WaitForSeconds(.2f);
            }
        }

        public void DetectObstacle()
        {
            StartCoroutine(DeathRoutine());
            IEnumerator DeathRoutine()
            {
                camHandler.canMove = false;
                dirtPs.Stop();
                ScreenShakeHandler.instance.ScreenShake(.5f);
                Instantiate(hitFx, transform.position, Quaternion.identity);
                yield return new WaitForSeconds(1f);
                characterPivot.GetChild(0).gameObject.SetActive(false);
                characterDestroyed.SetActive(true);
                Instantiate(destroyedFx, transform.position, Quaternion.identity);
                var rb = characterDestroyed.GetComponentsInChildren<Rigidbody2D>();
                foreach (var obj in rb)
                {
                    obj.AddForce(characterDestroyed.transform.position, ForceMode2D.Impulse);
                }
                yield return new WaitForSeconds(2f);
                GameManager.instance.ChangeState(GameState.GameOver);
            }
        }
    }
}