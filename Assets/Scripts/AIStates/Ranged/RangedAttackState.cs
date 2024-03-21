using DesignPatterns.ObjectPool;
using UnityEngine;

public class RangedAttackState : StateMachineBehaviour
{
    UnitManager unit;
    ProjectileLauncher launcher;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        unit = animator.GetComponentInParent<UnitManager>();
        launcher = animator.GetComponent<ProjectileLauncher>();
        unit.agent.ResetPath();
        unit.agent.speed = 0.0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (unit.health <= 0.0f)
        {
            animator.SetTrigger("Dead");
            return;
        }

        if (unit.currentTarget == null || unit.currentTarget == null)
        {
            animator.SetBool("IsIdle", true);
            animator.SetBool("IsAttacking", false);
            return;
        }

        unit.transform.LookAt(unit.currentTarget.transform);
        float distance = Vector3.Distance(unit.agent.transform.position, unit.currentTarget.transform.position);
        if (distance <= unit.range)
        {
            animator.SetBool("IsAttacking", true);
            animator.SetBool("IsWalking", false);
        }
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
