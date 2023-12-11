using System;
using System.Collections.Generic;
using UnityEngine;

public enum BonusType {
    BOMB
}

public class NetworkingBonus : MonoBehaviour
{
    public static event Action<BonusType> BonusEvent;

    private static Dictionary<int, BonusType> intToBonusDic;
    private static Dictionary<BonusType, int> bonusToIntDic;


    private void Start() {
        WebSocketClient.OnSocketMessage += OnSocketMessage;
        InitBonusDic();
    }

    private void InitBonusDic() {
        intToBonusDic = new Dictionary<int, BonusType>();
        bonusToIntDic = new Dictionary<BonusType, int>();

        AddToDic(0, BonusType.BOMB);
    }
    
    private void AddToDic(int intValue, BonusType bonusType) {
        intToBonusDic[intValue] = bonusType;
        bonusToIntDic[bonusType] = intValue;
    }

    private void OnSocketMessage(SocketMessage socketMessage) {
        if(socketMessage.op != SocketOP.BONUS_EVENT)
            return;

        BonusData d = socketMessage.d.ToObject<BonusData>();

        if(intToBonusDic.ContainsKey(d.bonus))
            BonusEvent.Invoke(intToBonusDic[d.bonus]);
        else 
            Debug.LogWarning("Unknown bonus code " + d.bonus);
    }

    public static void SendBonusEvent(BonusType bonusType) {
        int bonus = 0;

        WebSocketClient.Instance.SendMessage(
            SocketOP.BONUS_EVENT, 
            new { bonus });
    }
}
