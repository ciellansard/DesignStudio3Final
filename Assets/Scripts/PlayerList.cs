using System.Collections.Generic;
using UnityEngine;

public static class PlayerList
{
    public static List<GameObject> Players = new List<GameObject>();

    public static void Register(GameObject player)
    {
        if (!Players.Contains(player))
            Players.Add(player);
    }

    public static void Unregister(GameObject player)
    {
        Players.Remove(player);
    }
}
