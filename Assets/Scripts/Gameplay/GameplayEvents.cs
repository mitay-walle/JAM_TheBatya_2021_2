using Actor;
using Plugins.Switchable;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Gameplay
{
    public class GameplayEvents : MonoBehaviour
    {
        [SerializeField] private SwitchableGoParent _ringSwitch;
        [SerializeField] private SwitchableGoParent _phoneSwitch;
        [SerializeField] private Actor.Actor _mother;
        [SerializeField] private Sequence _phoneSequence;
        [SerializeField] private Sequence _deathRingSequence;
        
        // Defaults
        
        [Header("Defaults"),SerializeField] private GameObject Intro;
        [SerializeField] private SwitchableGoParent IntroScenes;
        [SerializeField] private GameObject ActorScenes;
        [SerializeField] private SwitchableGoParent _motherActions;

        [ShowInInspector] private bool _isRingInWater;

        #region Phone

        public void OnPhoneClick()
        {
            if (!_isRingInWater)
            {
                _phoneSequence.Play();
            }
        }

        public void OnPhoneRing()
        {
            if (!_isRingInWater)
            {
                _phoneSwitch.Show(1);
                _ringSwitch.Show(1);
            }
        }

        public void OnPhoneTalk()
        {
            _phoneSwitch.Show(2);
        }

        public void OnPhoneTalkFinish()
        {
            if (!_isRingInWater)
            {
                _ringSwitch.Show(0);
                _phoneSwitch.Show(2);
            }
        }
        
        public void OnPhoneSeqFinish()
        {
            if (!_isRingInWater)
            {
                _mother.PlayDefaultSequence();
            }
            else
            {
                _deathRingSequence.Play();
            }
        }

        #endregion

        public void OnRingClick()
        {
            if (!_isRingInWater) _ringSwitch.Show(2);
            _isRingInWater = true;
        }

        [Button]
        private void EditScenes()
        {
            Intro.SetActive(false);
            ActorScenes.SetActive(true);
        }
        
        [Button]
        public void SetDefaults()
        {
            _motherActions.Show(0);
            Intro.SetActive(true);
            IntroScenes.Show(0);
            ActorScenes.SetActive(false);
        }
    }
}