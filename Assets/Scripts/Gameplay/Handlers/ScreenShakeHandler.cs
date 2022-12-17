using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Handlers
{
    public class ScreenShakeHandler : MonoBehaviour
    {
        #region Singleton

        public static ScreenShakeHandler instance;
        private void Awake() => instance = this;

        #endregion

        [SerializeField] private AnimationCurve curve;
        [HideInInspector] public float duration = 1f;

        public void ScreenShake(float tempDuration)
        {
            duration = tempDuration;
            StartCoroutine(Shake());
        }

        private IEnumerator Shake()
        {
            var startPos = transform.position;
            var elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                var strength = curve.Evaluate(elapsedTime / duration);
                transform.position = startPos + Random.insideUnitSphere * strength;
                yield return null;
            }

            transform.position = startPos;
        }
    }
}
