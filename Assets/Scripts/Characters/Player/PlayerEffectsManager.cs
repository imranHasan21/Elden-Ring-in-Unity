using UnityEngine;

public class PlayerEfectsManager : CharacterEffectsManager
{
    [Header("Debug Delete Later")]
    [SerializeField] InstantCharacterEffect effectToTest;
    [SerializeField] bool processEffect = false;

    private void Update()
    {
        if (processEffect)
        {
            processEffect = false;
            // WHY ARE WE INSTANTIATE A COPY OF THIS, INSTEAD OF JUST USING IT AS IT IS?
            // ANS. WE DONT WANT TO USE THE SAME FIXED VALUE

            // WHEN WE INSTANTIATE IT, THE ORIGINAL IS NOT EFFECTED
            TakeStaminaDamageEffect effect = Instantiate(effectToTest) as TakeStaminaDamageEffect;

            // wHEN WE DONT INSTANTIATE IT, THE ORIGINAL IS CHANGED (YOU DO NOT WANT THIS IN MOST EFFECT)
            //effectToTest.staminaDamage = 55;

            ProcessInstantEffect(effectToTest);
        }
    }
}
