using Plugins.Extensions;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    [FoldoutGroup("References"), SerializeField]
    private AudioSource AS;

    [FoldoutGroup("References"), SerializeField]
    private AudioClip[] _clips;

    [FoldoutGroup("Settings"), SerializeField]
    private bool _playOnEnable;

    [FoldoutGroup("Settings"), SerializeField]
    private bool _oneShot;

    [FoldoutGroup("Settings"), SerializeField]
    private bool _randomTime;

    [FoldoutGroup("Settings"), SerializeField]
    private bool _useVolume;

    [ShowIf(nameof(_useVolume)), FoldoutGroup("Settings"), SerializeField]
    private Vector2 _volume = Vector2.one;

    [FoldoutGroup("Settings"), SerializeField]
    private bool _usePitch;

    [ShowIf(nameof(_usePitch)), FoldoutGroup("Settings"), SerializeField]
    private Vector2 _pitch = Vector2.one;

    [FoldoutGroup("Settings"), SerializeField]
    private Vector2 _delay;
    
    void OnEnable()
    {
        if (_playOnEnable)
        {
            if (_delay.x > 0 || _delay.y > 0)
            {
                Invoke(nameof(Play), _delay.Random());
            }
            else
            {
                Play();
            }
        }
    }

    [Button(ButtonSizes.Large)]
    public void Play()
    {
        if (!AS) Reset();


        if (_usePitch) AS.pitch = _pitch.Random();

        AudioClip clip = AS.clip;

        if (_clips != null && _clips.Length > 0)
        {
            clip = _clips.Random();
        }

        if (_oneShot)
        {
            float volume = 1;
            if (_useVolume) volume = _volume.Random();
            AS.PlayOneShot(clip, volume);
        }
        else
        {
            if (_useVolume) AS.volume = _volume.Random();
            AS.clip = clip;
            AS.Play();
        }

        if (_randomTime) AS.time = Random.Range(0, AS.clip.length);
    }

    [Button]
    protected virtual void Reset()
    {
#if UNITY_EDITOR
        Undo.RecordObject(this, "reset");
#endif

        AS = GetComponent<AudioSource>();
        if (!AS) AS = GetComponentInChildren<AudioSource>(true);
        if (!AS) AS = GetComponentInChildren<AudioSource>(true);
        if (!AS) AS = GetComponentInParent<AudioSource>();
        if (!AS) AS = gameObject.AddComponent<AudioSource>();
    }
}