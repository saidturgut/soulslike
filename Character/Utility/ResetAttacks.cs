using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAttacks : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<Character>().attackFlag = false;

        if (animator.GetComponent<PlayerRanged>())
        {
            if (animator.GetComponent<PlayerRanged>().reloading) { animator.GetComponent<PlayerRanged>().reloading = false; }
        }
        if (animator.GetComponent<AIRanged>())
        {
            if (animator.GetComponent<AIRanged>().reloading) { animator.GetComponent<AIRanged>().reloading = false; }
        }
    }
}