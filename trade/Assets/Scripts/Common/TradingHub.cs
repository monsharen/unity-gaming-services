using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingHub
{

    public string Name  { get; set; }
    public float X  { get; set; }
    public float Y  { get; set; }
    public int BuyPrice;
    public int SellPrice;

    public TradingHub(string name, float x, float y, int buyPrice, int sellPrice) {
        this.Name = name;
        this.X = x;
        this.Y = y;
        this.BuyPrice = buyPrice;
        this.SellPrice = sellPrice;
    }
}
