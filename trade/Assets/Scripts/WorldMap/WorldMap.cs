using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WorldMap : MonoBehaviour
{

    public Transform player;
    public Transform tradingHubPrefab;
    public Sprite[] tradingHubSprites;
    public static List<TradingHub> tradingHubs;
    private WorldMapStateEngine stateEngine;

    void Start()
    {
        InitStateEngine();
        LoadWorldMap();
        PositionPlayer();
        PositionCamera();
    }

    void Update()
    {
        stateEngine.OnUpdate();
    }

    private void PositionPlayer() 
    {
        TradingHub tradingHub = GameManager.Instance.CurrentPlayerTradingHub;
        player.position = new Vector2(tradingHub.X, tradingHub.Y);
    }

    private void PositionCamera() 
    {
        Camera.main.transform.position = new Vector3(player.position.x, player.position.y, -10);
    }

    private void InitStateEngine()
    {
        stateEngine = new WorldMapStateEngine();
        stateEngine.RegisterState(StateIdentifier.IDLE, new IdleWorldMapState(player, stateEngine));
        stateEngine.RegisterState(StateIdentifier.TRAVEL, new TravelWorldMapState(stateEngine));

        stateEngine.SetState(StateIdentifier.IDLE);
    }

    private void LoadWorldMap() {
        foreach (TradingHub tradingHub in GameManager.Instance.TradingHubs) {
            InstantiateTradingHub(tradingHub);
        }
    }

    private void InstantiateTradingHub(TradingHub tradingHub) {
        Transform tradignHubTra = Instantiate(tradingHubPrefab, new Vector2(tradingHub.X, tradingHub.Y), Quaternion.identity);
        TradingHubMonoBehaviour script = tradignHubTra.GetComponent<TradingHubMonoBehaviour>();
        script.tradingHub = tradingHub;
        script.stateEngine = stateEngine;
        script.tradingHubSpriteRenderer.sprite = tradingHubSprites[Random.Range(0, tradingHubSprites.Length)];
    } 
}
