using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAnimationStateChanger : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string currentState = "Ice";

    void Start(){
        ChangeAnimationState("Ice");
    }

    public void ChangeAnimationState(string newState, float speed = 1)
    {
        animator.speed = speed;
        if (currentState == newState){
            return;
        }
        currentState = newState;
        animator.Play(currentState);

    }
}
