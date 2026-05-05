using UnityEngine;

public class CharacterSoundEffectManager : MonoBehaviour
{
    private AudioSource audioSource;

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRollSoundFX()
    {
        audioSource.PlayOneShot(WorldSoundEffectManager.Instance.rollSoundFX);
    }
}
