using UnityEngine;

public class animatorBoolHelper : MonoBehaviour
{
    [SerializeField] string NameOfBool; 
    [SerializeField] Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        if(!animator)
            animator = GetComponent<Animator>();
    }

    public void setAnimatorBool(bool state)
    {
        animator.SetBool(NameOfBool, state);
    }
}
