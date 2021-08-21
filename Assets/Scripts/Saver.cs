using Plugins.Own.Animated;
using Plugins.Switchable;
using Sirenix.OdinInspector;
using UnityEngine;

public class Saver : MonoBehaviour
{
    private const string INTRO = "INTRO";
    public bool DebugIntro;
    public bool SkipIntro;
    [SerializeField] private Animated Finish;
    [SerializeField] private SwitchableGoParent _switch;
    [SerializeField] private int _skipIndex = 2;


    private void Awake()
    {
        Finish.OnPlayAction -= Save;
        Finish.OnPlayAction += Save;

        Load();
    }

    private void Load()
    {
        if (!PlayerPrefs.HasKey(INTRO))
        {
            PlayerPrefs.SetInt(INTRO, 0);
        }
        else
        {
            SkipIntro = PlayerPrefs.GetInt(INTRO) == 1;
        }

        if (SkipIntro)
        {
#if UNITY_EDITOR
            if (DebugIntro) return;
#endif
            _switch.Show(_skipIndex);
        }
        else
        {
            _switch.Show(0);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt(INTRO, 1);
    }

    [Button]
    public void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
}