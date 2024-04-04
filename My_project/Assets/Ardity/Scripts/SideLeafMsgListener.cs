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
        // �ִϸ��̼��� ��� ���� �ƴ� ���� �޽��� ó��
        if (animator.GetBool("isAnimationPlaying") == false)
        {
            Debug.Log("Message Arrived: " + msg);

            // isTouched�� ���� ���� �������� �ִϸ��̼� ���� ����
            bool isTouched = animator.GetBool("isTouched");
            animator.SetBool("isTouched", !isTouched);

            // ���⼭ �߰����� ���� ������ �ʿ� �����ϴ�.
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
