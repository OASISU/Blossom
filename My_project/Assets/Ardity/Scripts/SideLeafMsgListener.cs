using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnMessageArrived(string msg)
    {
        // 애니메이션이 재생 중이 아닐 때만 메시지 처리
        if (animator.GetBool("isAnimationPlaying") == false)
        {
            Debug.Log("Message Arrived: " + msg);

            // isTouched의 현재 값을 반전시켜 애니메이션 상태 변경
            bool isTouched = animator.GetBool("isTouched");
            animator.SetBool("isTouched", !isTouched);

            // 여기서 추가적인 상태 변경은 필요 없습니다.
        }
    }

    public void AnimationStart()
    {
        Debug.Log("Animation has started.");
        animator.SetBool("isAnimationPlaying", true);
    }

    public void AnimationEnd()
    {
        Debug.Log("Animation has ended.");
        animator.SetBool("isAnimationPlaying", false);
    }
}
