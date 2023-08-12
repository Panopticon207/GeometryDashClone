using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerTypeLibrary : ScriptableObject
{
    public List<PlayerController> playerTypes;


    public PlayerController GetPrefabFromItemType(PlayerController.PlayerType playerType)
    {
        foreach (var item in playerTypes)
        {
            if (item.playerType == playerType)
                return item;
        }

        return null;
    }
}
