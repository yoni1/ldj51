using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZillaBrain : MonoBehaviour
{
    private int nextFloorToDestroy = 0;

    public ZillaChomper zillaR;
    public ZillaChomper zillaL;
    //public Collider2D ignoredCollider;

    public void Start()
    {
        //Physics2D.IgnoreCollision(
        //    zillaR.GetComponent<Collider2D>(), ignoredCollider);
        //Physics2D.IgnoreCollision(
        //    zillaL.GetComponent<Collider2D>(), ignoredCollider);
    }

    public void IncrementFloor()
    {
        nextFloorToDestroy++;
    }

    public int GetNextFloorToDestroy()
    {
        return nextFloorToDestroy;
    }

    public void Chomp()
    {
        zillaR.Chomp();
        zillaL.Chomp();
    }
}
