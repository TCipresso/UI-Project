using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EcoManager : MonoBehaviour
{
    public static EcoManager Instance;

    [SerializeField] private TextMeshProUGUI currencyText;
    public int Currency { get; private set; }
    public int StartingCash = 280;




    void Awake()
    {

        Instance = this;
       //DontDestroyOnLoad(gameObject);/// use for multiple scenes if needed
        Currency = StartingCash;
        UpdateCurrencyUI();
    }

    public void AddCurrency(int amount)
    {
        Currency += amount;
        UpdateCurrencyUI();
    }

    private void UpdateCurrencyUI()
    {
        if (currencyText != null)
        {
            currencyText.text = Currency.ToString();
        }
    }
}