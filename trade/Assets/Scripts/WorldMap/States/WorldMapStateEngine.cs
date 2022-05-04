using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMapStateEngine
{

    private Dictionary<StateIdentifier, WorldMapState> states;

    private WorldMapState currentState = new InitState();

    public WorldMapStateEngine() {
        states = new Dictionary<StateIdentifier, WorldMapState>();
    }

    public void RegisterState(StateIdentifier identifier, WorldMapState state) {
        states.Add(identifier, state);
    }

    public void SetState(StateIdentifier identifier) {
        WorldMapState newState;
        if (states.TryGetValue(identifier, out newState)) {
            currentState.OnDestroy();
            currentState = newState;
            currentState.OnCreate();
        } else {
            Debug.Log("failed to find state with identifier " + identifier);
        }
    }

    public void OnUpdate() {
        currentState.OnUpdate();
    }

    public void OnClick(TradingHub tradingHub) {
        currentState.OnClick(tradingHub);
    }

    private class InitState : WorldMapState {
        public void OnCreate() {
            Debug.Log("Init state");
        }

        public void OnUpdate() {

        }

        public void OnDestroy() {

        }

        public void OnClick(TradingHub tradingHub) {

        }
    }
}
