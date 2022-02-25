using UnityEngine;

public class HoverAnimTrigger : MonoBehaviour
{
    [SerializeField] string NameOfBool; 
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void setAnimatorBool(bool state)
    {
        animator.SetBool(NameOfBool, state);
    }
}
