using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{

    public static GameManager Instance = new GameManager();
    
    public TradingHub CurrentPlayerTradingHub;
    public int Fuel;
    public int Credits;
    public Boolean HasCargo;

    public List<TradingHub> TradingHubs;

    private float fuelEfficiency = 0.1F;
    private int fuelPrice = 2;

    public void NewGame()
    {
        Debug.Log("Starting new game session");
        TradingHubs = new List<TradingHub>();
        TradingHubs.Add(new TradingHub("The Crystalline Plains space port", -729, 298, 100, 90));
        TradingHubs.Add(new TradingHub("Ruins of Voga", -145, 361, 120, 100));
        TradingHubs.Add(new TradingHub("New Vesta Orbital Colony", 405, -934, 200, 180));
        TradingHubs.Add(new TradingHub("Halo World trading outpost of Choniea", 527, 395, 150, 140));
        TradingHubs.Add(new TradingHub("Mirror World of Pascia", -861, -290, 130, 120));
        TradingHubs.Add(new TradingHub("Gaxu Research Facility", 204, -243, 80, 60));
        TradingHubs.Add(new TradingHub("Gemi Terraforming Complex on Phurquar Kily", 886, -351, 90, 80));

        CurrentPlayerTradingHub = TradingHubs[0];
        Credits = 1000;
        Fuel = 100;
        HasCargo = false;
    }

    public void Buy(TradingHub tradingHub)
    {
        if (!HasCargo && Credits >= tradingHub.BuyPrice)
        {
            Credits -= tradingHub.BuyPrice;
            HasCargo = true;
        }
    }

    public void Sell(TradingHub tradingHub)
    {
        if (HasCargo)
        {
            Credits += tradingHub.SellPrice;
            HasCargo = false;
        }
    }

    public int GetDistance(TradingHub tradingHub) {
        Vector2 currentPosition = new Vector2(CurrentPlayerTradingHub.X, CurrentPlayerTradingHub.Y);
        Vector2 target = new Vector2(tradingHub.X, tradingHub.Y);
        return Mathf.CeilToInt(Vector2.Distance(currentPosition, target));
    }

    public int GetRequiredFuelForDistance(int distance) {
        return Mathf.FloorToInt(distance * fuelEfficiency);
    }

    public bool CanTravel(int distance) {
        int fuelNeed = GetRequiredFuelForDistance(distance);
        return Fuel >= fuelNeed;
    }

    public bool CanTravel(TradingHub tradingHub) {
        int distance = GetDistance(tradingHub);
        return CanTravel(distance);
    }

    public void BurnFuel(int distance) {
        int fuelNeed = GetRequiredFuelForDistance(distance);
        Fuel = Fuel - fuelNeed;
    }

    public bool Refuel() {

        if (Credits < fuelPrice) {
            return false;
        }

        if (Fuel >= 100) {
            return false;
        }

        int fuelNeed = 100 - Fuel;
        int canAfford = Mathf.CeilToInt(Credits / fuelPrice);
        int buyingFuel = Mathf.Min(fuelNeed, canAfford);
        Fuel += buyingFuel;

        int cost = buyingFuel * fuelPrice;
        Credits -= cost;

        return true;
    }

    public void GoToWorlMap()
    {
        SceneManager.LoadScene("WorldMap", LoadSceneMode.Single);
    }

    public void GoToTradeHub(TradingHub tradingHub)
    {
        CurrentPlayerTradingHub = tradingHub;
        SceneManager.LoadScene("TradeHub", LoadSceneMode.Single);
    }
}
