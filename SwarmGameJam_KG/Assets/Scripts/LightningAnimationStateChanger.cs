using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningAnimationStateChanger : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] string currentState = "Lightning";

    void Start(){
        ChangeAnimationState("Lightning");
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
