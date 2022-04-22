using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public int money;
    public Text moneyText;     // Displaying the amount of gold mate.
    public TextMeshPro VrMoneyText;
    public Animator moneyBounceAnimation;
    public MoneyAnimationHelper animationHelper;
    public int startMoney;

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
        SetMoney(startMoney);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("Money Added");
            money += 10;
            if(moneyText)
                moneyText.text = ("Money: " + money);
            VrMoneyText.text = money.ToString();
        // if(moneyBounceAnimation)
        //     if(moneyBounceAnimation.gameObject.activeInHierarchy)
        //         moneyBounceAnimation.Play("BounceAnimation");
            animationHelper.animate = true;
        }
    }

    public void AddMoney(int CashIn)
    {
        money += Mathf.FloorToInt(CashIn * GameManager.instance.moneyMultiplier);
        VrMoneyText.text = money.ToString();
        if(moneyText)
            moneyText.text = ("Money: " + money);
        // if(moneyBounceAnimation)
        //     if(moneyBounceAnimation.gameObject.activeInHierarchy)
        //         moneyBounceAnimation.Play("BounceAnimation");
        animationHelper.animate = true;
    }

    public bool SubtractMoney(int CashOut)
    {
        // Easy way to multiply costs by 10. Change this later.
        int val = CashOut * 10;
        
        if(money - val < 0)
        {
            return false;
        }
        money -= val;
        if(moneyText)
            moneyText.text = ("Money: " + money);  
        VrMoneyText.text = money.ToString();

        return true;
    }

    public void SetMoney(int value = 20)
    {
        money = value;
        
        VrMoneyText.text = money.ToString();
        if(moneyText)
            moneyText.text = ("Money: " + money);
        // if(moneyBounceAnimation)
        //     if(moneyBounceAnimation.gameObject.activeInHierarchy)
        //         moneyBounceAnimation.Play("BounceAnimation");
        animationHelper.animate = true;
    }
}
