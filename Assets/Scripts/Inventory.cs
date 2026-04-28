using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class Inventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI keyText;
    [SerializeField] private TextMeshProUGUI blueGemsText;
    private int coins;
    private int keys;
    private int blueGems;

    private void Start()
    {
        UpdateUI();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateUI();
    }

    public void AddKeys(int amount)
    {
        keys += amount;
        UpdateUI();
    }

    public void AddBlueGems(int amount)
    {
        blueGems += amount;
        UpdateUI();
    }
    public int GetCoins()
    {
        return coins;
    }

    public int GetKeys()
    {
        return keys;
    }

    public int GetBlueGems()
    {
        return blueGems;
    }

    private void UpdateUI()
    {
        coinsText.text = "x" + coins;
        keyText.text = "x" + keys;
        blueGemsText.text = "x" + blueGems;
    }
    


}
