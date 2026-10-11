using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource evacuationSource;
    [SerializeField] private AudioSource heartBeatSource;
    [SerializeField] private AudioSource waterSource;
    [SerializeField] private AudioSource rattleSource;

    [SerializeField] private AudioClip heartbeat;
    [SerializeField] private AudioClip waterGushing;
    [SerializeField] private AudioClip rattle;

    [Header("Floor 1")]

    [Header("Evacuation")]
    [SerializeField] private AudioClip evacuate;
    [SerializeField] private AudioClip siren;

    [SerializeField] private AudioClip itemPickup;
    [SerializeField] private AudioClip itemDrop;

    [SerializeField] private AudioClip keyPickup;
    [SerializeField] private AudioClip boxPush;
    [SerializeField] private AudioClip waterWalk;
    [SerializeField] private AudioClip objectSplash;
    [SerializeField] private AudioClip tenseMusic;
    [SerializeField] private AudioClip emergencyLightHum;
    [SerializeField] private AudioClip slidingDoorOpen;
    [SerializeField] private AudioClip unscrew;
    [SerializeField] private AudioClip paperPickup;
    [SerializeField] private AudioClip panelFall;
    [SerializeField] private AudioClip sparks;
    [SerializeField] private AudioClip floor2DoorOpen;

    [Header("Floor 2")]
    [SerializeField] private AudioClip woodHit;
    [SerializeField] private AudioClip woodBreak;
    [SerializeField] private AudioClip metalHit;
    [SerializeField] private AudioClip metalPlateFall;
    [SerializeField] private AudioClip electricityOff;
    [SerializeField] private AudioClip fuseRemove;
    [SerializeField] private AudioClip fuseInsert;
    [SerializeField] private AudioClip hammerSwing;

    [Header("Floor 3")]
    [SerializeField] private AudioClip boatSplash;
    [SerializeField] private AudioClip impact;
    [SerializeField] private AudioClip grunt;
    [SerializeField] private AudioClip glassBreak;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip endingMusic;
    [SerializeField] private AudioClip sea;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Play(AudioClip clip)
    {
        if (clip != null)
            sfxSource.PlayOneShot(clip);
    }

    public void PlayKeyPickup() => Play(keyPickup);
    public void PlayItemPickup() => Play(itemPickup);
    public void PlayItemDrop() => Play(itemDrop);
    public void PlayBoxPush() => Play(boxPush);
    public void PlayWaterWalk() => Play(waterWalk);
    public void PlayObjectSplash() => Play(objectSplash);
    public void PlaySlidingDoorOpen() => Play(slidingDoorOpen);
    public void PlayUnscrew() => Play(unscrew);
    public void PlayPaperPickup() => Play(paperPickup);
    public void PlayPanelFall() => Play(panelFall);
    public void PlaySparks() => Play(sparks);
    public void PlayFloor2DoorOpen() => Play(floor2DoorOpen);

    public void PlayWoodHit() => Play(woodHit);
    public void PlayWoodBreak() => Play(woodBreak);
    public void PlayMetalHit() => Play(metalHit);
    public void PlayMetalPlateFall() => Play(metalPlateFall);
    public void PlayElectricityOff() => Play(electricityOff);
    public void PlayFuseRemove() => Play(fuseRemove);
    public void PlayFuseInsert() => Play(fuseInsert);
    public void PlayHammerSwing() => Play(hammerSwing);

    public void PlayBoatSplash() => Play(boatSplash);
    public void PlayImpact() => Play(impact);
    public void PlayGrunt() => Play(grunt);
    public void PlayGlassBreak() => Play(glassBreak);
    public void PlayClick() => Play(click);

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null)
            return;

        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlayHeartBeat()
    {
        heartBeatSource.clip = heartbeat;
        heartBeatSource.loop = true;
        heartBeatSource.Play();
        Debug.Log($"Source: {heartBeatSource}, Clip: {heartBeatSource?.clip}, Active: {heartBeatSource?.gameObject.activeInHierarchy}");
    }

    public void PlayWaterGushing()
    {
        waterSource.clip = waterGushing;
        waterSource.loop = true;
        waterSource.Play();
    }

    public void PlayRattle()
    {
        rattleSource.clip = rattle;
        rattleSource.loop = true;
        rattleSource.Play();
    }

    public void PlayTenseMusic() => PlayMusic(tenseMusic);
    public void PlayEndingMusic() => PlayMusic(endingMusic);

    public void StartEmergencyLightHum()
    {
        sfxSource.clip = emergencyLightHum;
        sfxSource.loop = true;
        sfxSource.Play();
    }

    public void StopEmergencyLightHum()
    {
        if (sfxSource.clip == emergencyLightHum)
        {
            sfxSource.Stop();
            sfxSource.loop = false;
            sfxSource.clip = null;
        }
    }

    public void StartSea()
    {
        sfxSource.clip = sea;
        sfxSource.loop = true;
        sfxSource.Play();
    }

    public void StopSea()
    {
        if (sfxSource.clip == sea)
        {
            sfxSource.Stop();
            sfxSource.loop = false;
            sfxSource.clip = null;
        }
    }

    public void PlayEvacuation()
    {
        Debug.Log($"Source: {evacuationSource}, Voice: {evacuate}");
        StartCoroutine(EvacuationSequence());
    }

    private System.Collections.IEnumerator EvacuationSequence()
    {
        evacuationSource.loop = false;
        evacuationSource.clip = evacuate;
        evacuationSource.Play();

        yield return new WaitWhile(() => evacuationSource.isPlaying);

        evacuationSource.clip = siren;
        evacuationSource.loop = true;
        evacuationSource.Play();
    }
}