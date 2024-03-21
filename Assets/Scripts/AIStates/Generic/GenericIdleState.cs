using System.Linq;
using UnityEngine;

public class GenericIdleState : StateMachineBehaviour
{
    UnitManager unit;
    UnitList list;
    float startingHealth;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unit = animator.GetComponentInParent<UnitManager>();
        list = GameObject.FindWithTag(unit.enemyTeamCount.ToString()).GetComponent<UnitList>();;
        unit.agent.ResetPath();

        startingHealth = unit.maxHealth;
        if (list.unitsInATeam.Count == 0)
        {
            unit.currentTarget = null;
            return;
        }
        unit.currentTarget = list.unitsInATeam[Random.Range(0, list.unitsInATeam.Count)];
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (unit.health <= 0.0f)
        {
            animator.SetTrigger("Dead");
            return;
        }

        // Don't continue if there is no target
        if (unit.currentTarget == null)
            return;

        // Immediately go to Walk state when threatened, or when there's an enemy
        if (unit.health < startingHealth || unit.currentTarget.activeInHierarchy == true)
        {
            GoIntoAction(animator);
        }
    }

    void GoIntoAction(Animator animator)
    {
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsIdle", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
