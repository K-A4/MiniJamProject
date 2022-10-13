using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Entities
{
    public static List<Ghost> LivingEnemies { get; private set; } = new List<Ghost>();
    public static int DeadEnemies { get; private set; } = 0;

    public static void AddEnemy(Ghost enemy)
    {
        LivingEnemies.Add(enemy);
    }

    public static void RemoveEnemy(Ghost enemy)
    {
        LivingEnemies.Remove(enemy);
        DeadEnemies++;
    }
}
