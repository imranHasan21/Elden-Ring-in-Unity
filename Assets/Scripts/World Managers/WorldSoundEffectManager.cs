using UnityEngine;

public class WorldSoundEffectManager : MonoBehaviour
{
    public static WorldSoundEffectManager Instance;

    [Header("ACTION SOUNND FX")]
    public AudioClip rollSoundFX;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
