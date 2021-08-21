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
        [SerializeField] private Sequence _phoneSequence;

        [ShowInInspector] private bool _isRingInWater;

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

        public void OnRingClick()
        {
            if (!_isRingInWater) _ringSwitch.Show(2);
            _isRingInWater = true;
        }
    }
}