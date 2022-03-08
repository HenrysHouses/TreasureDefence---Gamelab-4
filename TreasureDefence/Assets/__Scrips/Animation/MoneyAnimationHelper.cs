using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyAnimationHelper : MonoBehaviour
{
    Renderer animationRenderer;
    Animator animator;

    public bool animate;
    bool isAnimating;
    [SerializeField] float timeOffset;
    [SerializeField] float animationTime;

    void Start()
    {
        animationRenderer = GetComponent<Renderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(animate)
        {
            if(!isAnimating)
                StartCoroutine(animateCoin());
            else
                animationRenderer.material.SetFloat("_CurrentTime", Time.timeSinceLevelLoad + timeOffset);
        }
    }


    IEnumerator animateCoin()
    {
        isAnimating = true;
        animationRenderer.material.SetFloat("_AnimationStartTime", Time.timeSinceLevelLoad);
        yield return animationRenderer.material.GetFloat("_AnimationStartTime") != 0;
        animationRenderer.material.SetInt("_Animate", 1);
        animator.SetBool("CoinBounce", true);
        yield return new WaitForSeconds(animationTime);
        isAnimating = false;
        animate = false;
        animationRenderer.material.SetInt("_Animate", 0);
                animationRenderer.material.SetFloat("_CurrentTime", 0);
        animator.SetBool("CoinBounce", false);
    }
}
