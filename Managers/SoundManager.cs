using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager s { get; private set; }

    public Sound[] FootStepSounds;
    public Sound[] ObstacleImpactSounds;
    public Sound[] VividImpactSounds;
    public Sound[] WeaponSounds;
    public Sound[] EnemyBasicSounds;
    public Sound[] EnemyWeaponSounds;
    public Sound[] MetalSounds;
    public Sound[] OtherSounds;
    public Sound[] ShieldSounds;
    public Sound[] RangedSounds;
    public Sound[] ShoutSounds;
    public enum SoundType
    {
        FootStep,
        ObstacleImpact,
        VividImpact,
        Weapon,
        EnemyBasic,
        EnemyWeapon,
        Metal,
        Other,
        Shield,
        Ranged,
        Shout,
    }

    [SerializeField] private GameObject emptyAudioSource;

    private void Awake() { s = this; }


    public void Play(int ID, Transform target, SoundType soundType)
    {
        Sound s = new Sound();

        switch (soundType)
        {
            case SoundType.FootStep : { s = FootStepSounds[ID]; break; }
            case SoundType.ObstacleImpact : { s = ObstacleImpactSounds[ID]; break; }
            case SoundType.VividImpact : { s = VividImpactSounds[ID]; break; }
            case SoundType.Weapon : { s = WeaponSounds[ID]; break; }
            case SoundType.EnemyBasic : { s = EnemyBasicSounds[ID]; break; }
            case SoundType.EnemyWeapon : { s = EnemyWeaponSounds[ID]; break; }
            case SoundType.Metal : { s = MetalSounds[ID]; break; }
            case SoundType.Other : { s = OtherSounds[ID]; break; }
            case SoundType.Shield : { s = ShieldSounds[ID]; break; }
            case SoundType.Ranged : { s = RangedSounds[ID]; break; }
            case SoundType.Shout : { s = ShoutSounds[ID]; break; }
        }

        GameObject g = Instantiate(emptyAudioSource, target.transform.localPosition, target.transform.localRotation);

        g.name = s.clip.name;

        s.source = g.GetComponent<AudioSource>();

        s.source.gameObject.transform.localPosition = target.transform.localPosition;

        s.source.clip = s.clip;

        s.source.volume = s.volume;

        s.source.Play();
    }
}

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1;

    [HideInInspector] public AudioSource source;
}