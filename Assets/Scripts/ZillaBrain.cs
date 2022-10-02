using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZillaBrain : MonoBehaviour
{
    private int nextFloorToDestroy = 0;
    private float zillaOffset = 14.0f;
    private float zillaInitY = 1.087444f;
    private float floorSize = 9.2f;
    public ZillaChomper zillaR;
    public ZillaChomper zillaL;
    //public Collider2D ignoredCollider;

    private IEnumerator TimerCoroutine() {
        while (true) {
            yield return new WaitForSeconds(10.0f);
            Chomp();
        }
    }

    private void TimerRestart(){
        StopCoroutine( "TimerCoroutine");
        StartCoroutine("TimerCoroutine");
    }

    public void Start()
    {
        StartCoroutine("TimerCoroutine");

        //Physics2D.IgnoreCollision(
        //    zillaR.GetComponent<Collider2D>(), ignoredCollider);
        //Physics2D.IgnoreCollision(
        //    zillaL.GetComponent<Collider2D>(), ignoredCollider);
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


    public void resetZilla(bool isToNextFloor = false)
    {
        if (isToNextFloor) {
            nextFloorToDestroy ++;
        }

        TimerRestart();
        zillaR.StopChomping();
        zillaL.StopChomping();

        float newY = zillaInitY - nextFloorToDestroy * floorSize;
        Debug.Log(newY);

        zillaR.transform.SetLocalPositionAndRotation(
            new Vector3(zillaOffset, newY, zillaR.transform.position.z), Quaternion.identity);

        zillaL.transform.SetLocalPositionAndRotation(
            new Vector3(-zillaOffset, newY, zillaL.transform.position.z), Quaternion.identity);
    }

}
