using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    public int money;
    public Text moneyText;     // Displaying the amount of gold mate.


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoneyToAdd(int CashIn)
    {
        money += CashIn;
    }

    public bool MoneyToSubtract(int CashOut)
    {
        if(money - CashOut < 0)
        {
            return false;
        }
        return true;
    }
}
