using System;
using UnityEngine;

namespace Gameplay.Player
{
    public class EndComponentHandler3D : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("FX"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
