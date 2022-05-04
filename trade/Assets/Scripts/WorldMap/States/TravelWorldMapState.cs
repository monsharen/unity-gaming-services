using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelWorldMapState : WorldMapState
{
    private WorldMapStateEngine stateEngine;

    public TravelWorldMapState(WorldMapStateEngine stateEngine) {
        this.stateEngine = stateEngine;
    }

    public void OnCreate() {
        Debug.Log("Travel state");
    }

    public void OnUpdate() {

    }

    public void OnDestroy() {

    }

    public void OnClick(TradingHub tradingHub) {
    }

}
