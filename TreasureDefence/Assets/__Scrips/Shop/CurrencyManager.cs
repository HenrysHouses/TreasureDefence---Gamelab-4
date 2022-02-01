using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public int money;
    public Text moneyText;     // Displaying the amount of gold mate.
    public Animator moneyBounceAnimation;


    private void Awake()
    {
      if(instance == null)
        {
            instance = this;
        }
      else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoneyToAdd(int CashIn)
    {
        money += CashIn;
        moneyText.text = ("Money: " + money);
        //moneyBounceAnimation.SetTrigger("Bounce");
        
    }

    public bool MoneyToSubtract(int CashOut)
    {
        if(money - CashOut < 0)
        {
            return false;
        }
        money -= CashOut;
        moneyText.text = ("Money: " + money);  

        return true;
    }
}
