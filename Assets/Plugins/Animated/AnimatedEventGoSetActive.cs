using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.Own.Animated
{
    public class AnimatedEventGoSetActive : AnimatedEvents
    {
        [LabelWidth(75), HorizontalGroup("Play"), SerializeField]
        private GameObject onPlay;

        [LabelWidth(1), ToggleLeft, EnableIf("onPlay"), HideLabel, HorizontalGroup("Play", Width = 15), SerializeField]
        private bool onPlayState;

        [LabelWidth(75), HorizontalGroup("Finish"), SerializeField]
        private GameObject onFinish;

        [LabelWidth(1), ToggleLeft, EnableIf("onFinish"), HideLabel, HorizontalGroup("Finish", Width = 15),
         SerializeField]
        private bool onFinishState;

        [LabelWidth(75), HorizontalGroup("Reset"), SerializeField]
        private GameObject onReset;

        [LabelWidth(1), ToggleLeft, EnableIf("onReset"), HideLabel, HorizontalGroup("Reset", Width = 15),
         SerializeField]
        private bool onResetState;

        [LabelWidth(75), HorizontalGroup("Timed"), SerializeField]
        private GameObject onTime;

        [LabelWidth(1), ToggleLeft, EnableIf("onTime"), HideLabel, HorizontalGroup("Timed", Width = 15), SerializeField]
        private bool onTimeState;

        public override void OnPlay()
        {
            if (onPlay != null)
            {
                onPlay.SetActive(onPlayState);
                if (Debugging)
                    Debug.Log($"[ AnimatedEventGoSetActive ] OnPlay()  {onPlay.name}.SetActive({onPlayState})",this);
            }
        }

        public override void OnFinish()
        {
            if (onFinish != null)
            {
                onFinish.SetActive(onFinishState);
                if (Debugging)
                    Debug.Log($"[ AnimatedEventGoSetActive ] OnFinish()  {onFinish?.name}.SetActive({onFinishState})",this);
            }
        }

        public override void OnReset()
        {
            if (onReset != null)
            {
                onReset.SetActive(onResetState);
                if (Debugging)
                    Debug.Log($"[ AnimatedEventGoSetActive ] OnReset()  {onReset?.name}.SetActive({onResetState})",this);
            }
        }

        public override void OnTime()
        {
            if (onTime != null)
            {
                onTime.SetActive(onTimeState);
                if (Debugging)
                    Debug.Log($"[ AnimatedEventGoSetActive ] OnTime()  {onTime?.name}.SetActive({onTimeState})",this);
            }
        }
    }
}