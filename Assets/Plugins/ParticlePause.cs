using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins
{
    [ExecuteAlways]
    public class ParticlePause : MonoBehaviour
    {
        [SerializeField] private bool _executeOnEnable = true;
        [SerializeField] private bool _instant = true;
        [SerializeField] private float _time = .2f;

        private void OnEnable()
        {
            if (_executeOnEnable) Execute();
        }

        [Button]
        public void Execute()
        {
            StartCoroutine(ExecuteCor());
        }

        IEnumerator ExecuteCor()
        {
            ParticleSystem fx = GetComponent<ParticleSystem>();
            
            fx.Stop(true,ParticleSystemStopBehavior.StopEmittingAndClear);
            fx.Play();
            
            if (_instant)
            {
                fx.Simulate(_time, true, false);
            }
            else
            {
                yield return new WaitForSeconds(_time);
            }

            fx.Pause();
        }
    }
}