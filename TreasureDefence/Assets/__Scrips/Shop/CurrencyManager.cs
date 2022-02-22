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
        Debug.Log("Money Cheat on K button Active");
      if(instance == null)
        {
            instance = this;
        }
      else
        {
            Destroy(gameObject);
        }
        AddMoney(20);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Money Added");
            money += 10;
            moneyText.text = ("Money: " + money);
            moneyBounceAnimation.Play("BounceAnimation");
        }
    }

    public void AddMoney(int CashIn)
    {
        money += CashIn;
        moneyText.text = ("Money: " + money);
        moneyBounceAnimation.Play("BounceAnimation");
        
    }

    public bool SubtractMoney(int CashOut)
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
