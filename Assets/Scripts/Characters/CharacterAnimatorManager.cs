using UnityEngine;
using Unity.Netcode;

public class CharacterAnimatorManager : MonoBehaviour
{
    CharacterManager character;
    int vertical;
    int horizontal;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
        vertical = Animator.StringToHash("Vertical");
        horizontal = Animator.StringToHash("Horizontal");
    }

    public void UpdateAnimatorMovementParameters(float horizontalValue, float verticalValue, bool isSprinting)
    {
        float horizontalAmount = horizontalValue;
        float verticalAmount = verticalValue;

        if (isSprinting)
        {
            verticalAmount = 2;
        }
        character.animator.SetFloat(horizontal, horizontalAmount, 0.1f, Time.deltaTime);
        character.animator.SetFloat(vertical, verticalAmount, 0.1f, Time.deltaTime);
    }

    public virtual void PlayTargetActionAnimation(
        string targetAnimation,
        bool isPerformingAction,
        bool applyRootMotion = true,
        bool canMove = false,
        bool canRotate = false)
    {
        character.animator.applyRootMotion = applyRootMotion;
        character.animator.CrossFade(targetAnimation, 0.2f);
        //CAN BE USED TO STOP CHARACTER FROM ATTEMTING NEW ACTION
        // FOR EXAMPLE IF YOU GET DAMAGED AND BEGIN PERFORMING DAMAGE ANIMATION
        // THIS FLAG WILL TURN TRUE IF YOU ARE STUNNED
        // WE CAN THEN CHECK FOR THIS BEFORE ATTEMPTING NEW ACTION
        character.isPerformingAction = isPerformingAction;
        character.canMove = canMove;
        character.canRotate = canRotate;

        // TELL THE SERVER/HOST WE PLAYED AN ANIMATION, AND PLAY THAT ANIMATION FOR EVERYBODY ELSE PRESENT
        character.characterNetworkManager.NotifyTheServerOfActionAnimationServerRpc(NetworkManager.Singleton.LocalClientId, targetAnimation, applyRootMotion);
    }

}
