using Gameplay.Managers;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerAnimEventHandler : MonoBehaviour
    {
        [SerializeField] private ParticleSystem dirtParticleSystem;
        
        [SerializeField] private Animator anim;
        private static readonly int s_Drill = Animator.StringToHash("Drill");
        private static readonly int s_Jump = Animator.StringToHash("Jump");

        private void Start()
        {
            transform.rotation = Quaternion.identity;
        }

        public void SwitchToJump() => anim.SetTrigger(s_Jump);
        
        private void SwitchToDrill()
        {
            GameMenuManager.instance.Play();
            anim.SetTrigger(s_Drill);
            dirtParticleSystem.Play();
        }
    }
}