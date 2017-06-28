using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPopupAni : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
    void OnStateEnter(Animator animator, AnimatorStateInfo state_info, int layer_index)
    {
        // 텍스트 애니메이션 매니저에게 애니메이션이 시작중임을 알릴필요가 있다.
        // 만약 연속적으로 애니메이션이 발생해야할 상황이면 다른 유휴 팝업애니 객체에게 일을 시켜야한다.
        //animator.transform.parent.GetComponent<TxtEffectMgr>().YourchildWorking(animator);
    }


	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}


	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
    void OnStateExit(Animator animator, AnimatorStateInfo state_info, int layer_index)
    {
        //GameObject.Find("DamageText").GetComponent<Animator>().SetBool("Do", false);
        animator.SetBool("Do", false);
        animator.transform.parent.GetComponent<TxtEffectMgr>().YourchildFinish(animator);
        animator.gameObject.SetActive(false);
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
