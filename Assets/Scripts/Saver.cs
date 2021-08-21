using Gameplay;
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
    [SerializeField] private Animated Scene1;
    [SerializeField] private SwitchableGoParent _switch;
    [SerializeField] private int _skipIndex = 2;
    [SerializeField] private GameplayEvents _gameplay;


    private void Awake()
    {
        Finish.OnPlayAction -= Save;
        Finish.OnPlayAction += Save;

        Load();
    }

    public void Load()
    {
        _gameplay.SetDefaults();

        Debug.Log("Load");

        if (!PlayerPrefs.HasKey(INTRO))
        {
            PlayerPrefs.SetInt(INTRO, 0);
            PlayerPrefs.Save();
            SkipIntro = false;
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
            Debug.Log("Skip Intro");
            _switch.Show(_skipIndex);
        }
        else
        {
            Debug.Log("Show full Intro");
            _switch.Show(0);
            Scene1.Play();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt(INTRO, 1);
    }

    [Button]
    public void Clear()
    {
        Debug.Log("Clear");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}