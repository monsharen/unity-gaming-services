using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class IdleWorldMapState : WorldMapState
{

    private float speed = 200f;

    private Transform player;
    private WorldMapStateEngine stateEngine;
    private TradingHub clickedTradingHub;

    public IdleWorldMapState(Transform player, WorldMapStateEngine stateEngine) {
        this.player = player;
        this.stateEngine = stateEngine;
    }

    public void OnCreate() {
        Debug.Log("Idle state");
        LeanTween.scale(player.gameObject, new Vector2(0.2f, 0.2f), 0.1f);
    }

    public void OnUpdate() {

    }

    public void OnDestroy() {

    }

    public void OnClick(TradingHub tradingHub) {

        if (!GameManager.Instance.CanTravel(tradingHub)) {
            Debug.Log("too far");
            return;
        }

        clickedTradingHub = tradingHub;

        Vector2 target = new Vector2(tradingHub.X, tradingHub.Y);
        float distance = Vector2.Distance(player.position, target);
        float travelTime = distance / speed;

        GameManager.Instance.BurnFuel(Mathf.FloorToInt(distance));
        RotatePlayerTowards(target);
        LTDescr d = LeanTween.move(player.gameObject, target, travelTime);

        if (d != null){ 
           d.setOnComplete( DestinationReached );
        }

        float zoomTime = travelTime / 4;

        LeanTween.scale(player.gameObject, new Vector2(0.5f, 0.5f), zoomTime)
            .setEase(LeanTweenType.easeInSine);
        LeanTween.scale(player.gameObject, new Vector2(0.2f, 0.2f), zoomTime)
            .setEase(LeanTweenType.easeOutCubic)
            .setDelay(zoomTime*3);
        stateEngine.SetState(StateIdentifier.TRAVEL);
    }

    private void RotatePlayerTowards(Vector2 target) {
        Vector2 direction = target - (Vector2) player.position;
        player.transform.right = direction;
    }

    private void DestinationReached() {
        GameManager.Instance.GoToTradeHub(clickedTradingHub);
    }
}
