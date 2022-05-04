using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Services.Analytics;

public class TradingHubMonoBehaviour : MonoBehaviour
{

    public TradingHub tradingHub;
    public WorldMapStateEngine stateEngine;
    public Text caption;
    public Text subtitle;
    public SpriteRenderer tradingHubSpriteRenderer;

    void Start() {
        caption.text = tradingHub.Name;
        int distance = GameManager.Instance.GetDistance(tradingHub);
        int fuelRequired = GameManager.Instance.GetRequiredFuelForDistance(distance);
        subtitle.text = GetSubtitleText();
    }

    void OnMouseDown() {
        AnalyticsService.Instance.CustomData("travel", new Dictionary<string, object>
        {
            { "origin", GameManager.Instance.CurrentPlayerTradingHub.Name },
            { "destination", tradingHub.Name },
            { "distance", GameManager.Instance.GetDistance(tradingHub) },
            { "within_reach", GameManager.Instance.CanTravel(tradingHub) }
        });

        stateEngine.OnClick(tradingHub);
    }

    private string GetSubtitleText() {
        int distance = GameManager.Instance.GetDistance(tradingHub);

        if (distance < 1) {
            return "You are here";
        }

        int fuelRequired = GameManager.Instance.GetRequiredFuelForDistance(distance);
        return "Distance: " + distance + "\n" + 
               "Fuel required: " + fuelRequired;
    }
}
