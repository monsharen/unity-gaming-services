using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface WorldMapState
{
    public void OnCreate();
    public void OnUpdate();
    public void OnDestroy();
    public void OnClick(TradingHub tradingHub);
}
