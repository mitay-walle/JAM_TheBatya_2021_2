using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEditor;
using UnityEngine;


namespace UI.Animation.ACV
{
    public sealed class ACV_Text : AnimatedChangeValueUI<TMP_Text>
    {
        public enum eSeparateType
        {
            None = 0,
            MoneyLike = 1,
            Time = 2,
        }


        [Header("Format")] public string Key = "${0}"; //TODO: Localize локализовать
        public eSeparateType SeparateType;
        public bool FormatNeedLocalize;
        public bool LocalizeOnChange;
        public bool showMax;
        public bool CalcSizeOnChange;
        public bool roundToInt;

        [OnInspectorGUI]
        private void Validate()
        {
            if (LocalizeOnChange && Animated) Set(value, false);
        }

        public override void SetValue()
        {
            string newValue = string.Empty;

            float valueToShow = roundToInt ? (int) (animatedValue + .5f) : animatedValue;

            //var key = FormatNeedLocalize ? 1.L(Key, false) : Key;
            var key = Key;


            switch (SeparateType)
            {
                case eSeparateType.None:
                    newValue = valueToShow.ToString();
                    break;
                case eSeparateType.MoneyLike:
                    newValue = valueToShow.ToString();
                    //newValue = TextUtility.SeparateBySpaces(valueToShow,roundToInt);
                    break;
                case eSeparateType.Time:
                    key = TimeSpan.FromSeconds(valueToShow).ToString(@"mm\:ss");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


            if (showMax)
            {
                Animated.text = string.Format(key, newValue, maxValue);
            }
            else
            {
                Animated.text = string.Format(key, newValue);
            }

            if (CalcSizeOnChange)
            {
                Animated.GetPreferredValues();
                (transform as RectTransform).sizeDelta = Animated.GetPreferredValues();
            }
        }

        public new void Reset()
        {
            base.Reset();

            Animated = GetComponent<TextMeshProUGUI>();

#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
    }
}