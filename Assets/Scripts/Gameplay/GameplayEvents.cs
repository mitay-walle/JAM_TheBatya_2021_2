using Actor;
using Plugins.Switchable;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class GameplayEvents : MonoBehaviour
    {
        #region Vars

        [SerializeField] private Saver _saver;
        [SerializeField] private SwitchableGoParent _ringSwitch;
        [SerializeField] private SwitchableGoParent _phoneSwitch;
        [SerializeField] private SwitchableGoParent _tapSwitch;
        [SerializeField] private SwitchableGoParent _knifeSwitch;
        [SerializeField] private GameObject _knifeWaterOnFloorObject;

        [SerializeField] private Actor.Actor _mother;
        [SerializeField] private Sequence _phoneSequence;
        [SerializeField] private Sequence _deathRingSequence;
        [SerializeField] private Sequence _removeWaterSequence;
        [SerializeField] private Sequence _removeKnifeSequence;
        [SerializeField] private Sequence _deathKnifeSequence;
        [SerializeField] private Sequence[] _FridgeSequences;
        [SerializeField] private Sequence _deathFridgeSequence;

        // Defaults

        [Header("Defaults"), SerializeField] private GameObject Intro;
        [SerializeField] private SwitchableGoParent IntroScenes;
        [SerializeField] private GameObject ActorScenes;
        [SerializeField] private SwitchableGoParent _motherActions;

        [ShowInInspector] private bool _isRingInWater;
        [ShowInInspector] private bool _isKnifeOnFloor;
        [ShowInInspector] private bool _isWaterOnFloor;
        [ShowInInspector] private bool _isFridgeMortal;
        [ShowInInspector] private bool _isFridgeMortalFinished;

        private bool _isKnifeAndWaterOnFloor => _isWaterOnFloor && _isKnifeOnFloor;
        private int _fridgeClickIndex;

        #endregion

        #region General

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
            _knifeWaterOnFloorObject.SetActive(false);
        }

        public void OnReplayClick()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void OnResetSaveClick()
        {
            _saver.Clear();
            OnReplayClick();
        }

        
        public void OnRepeatIntroClick()
        {
            _saver.ClearIntro();
            _saver.Load();
        }
        
        #endregion

        #region Phone & Ring

        public void OnPhoneClick()
        {
            if (!_isRingInWater && !_isKnifeAndWaterOnFloor)
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

        public void OnRingClick()
        {
            if (!_isRingInWater) _ringSwitch.Show(2);
            _isRingInWater = true;
            _phoneSwitch.Show(2);
            SaveDeath(0);
        }

        #endregion

        #region Water & Knife

        public void OnStorageUseStart()
        {
            if (!_isKnifeAndWaterOnFloor)
            {
                if (!_isWaterOnFloor) _tapSwitch.Show(1);
                if (!_isKnifeOnFloor) _knifeSwitch.Show(1);
            }
        }

        public void OnStorageUseFinish()
        {
            if (!_isKnifeAndWaterOnFloor)
            {
                if (!_isWaterOnFloor) _tapSwitch.Show(0);
                if (!_isKnifeOnFloor) _knifeSwitch.Show(0);
            }
        }

        public void TryRemoveWater()
        {
            if (_isKnifeAndWaterOnFloor)
            {
                _deathKnifeSequence.Play();
            }
            else if (_isWaterOnFloor)
            {
                Debug.Log("Remove Water");

                _removeWaterSequence.Play();
            }
            else if (_isKnifeOnFloor)
            {
                TryRemoveKnife();
            }
        }

        public void TryRemoveKnife()
        {
            if (_isKnifeAndWaterOnFloor) return;

            if (_isKnifeOnFloor)
            {
                Debug.Log("Remove Knife");
                _removeKnifeSequence.Play();
            }
        }

        public void RemoveWater()
        {
            _isWaterOnFloor = false;
            _tapSwitch.Show(0);
            _mother.PlayDefaultSequence();
        }

        public void RemoveKnife()
        {
            _isKnifeOnFloor = false;
            _knifeSwitch.Show(0);
        }

        public void OnKnifeClick()
        {
            _isKnifeOnFloor = true;
            _knifeSwitch.Show(2);
            if (_isKnifeAndWaterOnFloor) OnKnifeWaterLastClick();
        }

        public void OnWaterClick()
        {
            _isWaterOnFloor = true;
            _tapSwitch.Show(2);
            if (_isKnifeAndWaterOnFloor) OnKnifeWaterLastClick();
        }

        private void OnKnifeWaterLastClick()
        {
            _knifeWaterOnFloorObject.SetActive(true);
            _phoneSwitch.Show(2);
            SaveDeath(1);
        }

        #endregion

        #region Fridge

        public void TryOpenFridge()
        {
            if (_isFridgeMortal)
            {
                OnFridgeSecondClick();
            }
        }
        
        public void OnFridgeFirstClick()
        {
            _isFridgeMortal = true;
        }
        
        public void OnFridgeSecondClick()
        {
            _FridgeSequences[_fridgeClickIndex].Play();
            _fridgeClickIndex++;
        }

        public void OnFridgeSequenceFinish()
        {
            if (_isFridgeMortalFinished) return;
            
            _fridgeClickIndex = 0;
            _mother.PlayDefaultSequence();
            _isFridgeMortal = false;
        }

        public void OnFridgeTripleClick()
        {
            _isFridgeMortalFinished = true;
            _deathFridgeSequence.Play();
            _phoneSwitch.Show(2);
            
            SaveDeath(2);
        }

        public void SaveDeath(int index)
        {
            _saver.SaveDeath(index);
        }

        #endregion
    }
}