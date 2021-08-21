using Plugins.Own.Animated;
using Plugins.Switchable;
using Sirenix.OdinInspector;
using UnityEngine;

public class Saver : MonoBehaviour
{
    private const string INTRO = "INTRO";
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
            Debug.Log("Create pref");
        }
        else
        {
            SkipIntro = PlayerPrefs.GetInt(INTRO) == 1;
            Debug.Log("Get existing pref");
        }

        if (SkipIntro)
        {
            _switch.Show(_skipIndex);
            Debug.Log("Skip intro");
        }
    }

    [Button]
    public void Save()
    {
        Debug.Log("Save");
        PlayerPrefs.SetInt(INTRO,1);
    }
    
    [Button]
    public void Clear()
    {
        Debug.Log("Clear");
        PlayerPrefs.DeleteAll();
    }
    
}
