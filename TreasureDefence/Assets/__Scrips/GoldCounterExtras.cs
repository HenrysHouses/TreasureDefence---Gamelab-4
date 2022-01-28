using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCounterExtras : MonoBehaviour
{
    public int currentGold; //connected to the changing variable from on other script
    private int lastBalance;
    Animator bounceAnim;

    public Animator shineAnim;  //for shineing



    // Start is called before the first frame update
    void Start()
    {
        bounceAnim = gameObject.GetComponent<Animator>();
        lastBalance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(lastBalance < currentGold)
        {
            bounceAnim.SetTrigger("Bounce");
        }

        if (shineAnim != null)
        {
            // play Bounce but start at a quarter of the way though
            shineAnim.Play("Shine", 0, 0.25f);
        }

    }
    public void Bounce() //If we want to put it in the scripts where we gain coins, then we dont have to check the update always for the 
    {
        bounceAnim.SetTrigger("Bounce");
    }


}
