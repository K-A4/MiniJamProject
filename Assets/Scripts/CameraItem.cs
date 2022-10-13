using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraItem : MonoBehaviour
{
    public float Angle = 10;

    public void Use(Vector3 origin, Vector3 direction)
    {
        var ghosts = Entities.LivingEnemies;
        Debug.DrawRay(origin, direction, Color.red, 1);
        for(int i = 0; i < ghosts.Count; i++)
        {
            Ghost ghost = ghosts[i];
            Vector3 ghostDir = ghost.transform.position - origin;
            if (Vector2.Angle(ghostDir, direction) <= Angle)
            {
                ghost.Show();
            }
        }
    }
}
