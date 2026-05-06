using UnityEditor;
using UnityEngine;

public class CharacterEffectsManager : MonoBehaviour
{
    // PROCESS INSTANCE EFFECT (TAKE DAMAGE, HEAL)

    // PROCESSED TIME EFFECTS (POISON, BUILD UPS)

    // PROCESS STATIC EFFECT (ADDING OR REMOVING BUFF FROM ITEM OR TALISMAN)

    CharacterManager character;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public virtual void ProcessInstantEffect(InstantCharacterEffect effect)
    {
        // TAKE IN A EFFECT

        // PROCESS IT
        effect.ProcessEffect(character);
    }
}
