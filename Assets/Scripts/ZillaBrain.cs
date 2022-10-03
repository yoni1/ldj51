using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZillaBrain : MonoBehaviour
{
    private int nextFloorToDestroy = 0;
    private float zillaOffset = 15.0f;
    private float floorSize = 9.2f;
    public ZillaChomper zillaR;
    public ZillaChomper zillaL;
    private bool isChomping;
    //public Collider2D ignoredCollider;

    private IEnumerator TimerCoroutine() {
        while (true) {
            yield return new WaitForSeconds(10.0f);
            Chomp();
        }
    }

    private void TimerRestart(){
        StopCoroutine("TimerCoroutine");
        StartCoroutine("TimerCoroutine");
    }

    public void Start()
    {
        isChomping = false;
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
        if (!isChomping)
        {
            isChomping = true;
            zillaR.Chomp();
            zillaL.Chomp();
        }
    }


    public void resetZilla(bool isToNextFloor = false)
    {
        if (isToNextFloor) {
            nextFloorToDestroy ++;
        }

        isChomping = false;
        TimerRestart();
        zillaR.StopChomping();
        zillaL.StopChomping();

        float newY = - nextFloorToDestroy * floorSize;
        Debug.Log(newY);

        zillaR.transform.SetLocalPositionAndRotation(
            new Vector3(zillaOffset, newY, zillaR.transform.position.z), Quaternion.identity);

        zillaL.transform.SetLocalPositionAndRotation(
            new Vector3(-zillaOffset, newY, zillaL.transform.position.z), Quaternion.identity);
    }

}
