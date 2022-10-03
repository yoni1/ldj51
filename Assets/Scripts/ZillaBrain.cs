using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZillaBrain : MonoBehaviour
{
    private int nextFloorToDestroy = 0;
    private float zillaOffset = 17.0f;
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

    public FloorController GetFloorController(int floorOffset = 0)  
    {
        string currFloorId = "Floor" + (GetNextFloorToDestroy() + floorOffset);
        //print("Found current floor " + currFloorId);
        return GameObject.Find(currFloorId).GetComponent<FloorController>();
    }

    public Vector3 GetNextSpawnLocation()
    {
        return GetFloorController(1).GetRelativeSpawnLocation();
    }


    public Vector3 GetCurrentSpawnLocation()
    {
        return GetFloorController(0).GetRelativeSpawnLocation();
    }

    public void Chomp()
    {
        GetComponent<AudioSource>().Play();
        if (!isChomping)
        {
            GetFloorController().MakeFurnitureMovable();
            resetZilla(false);
            isChomping = true;
            zillaR.Chomp();
            zillaL.Chomp();
        }
    }

    private float CalcNextFloorY(int floorOffset = 0){
        return - (nextFloorToDestroy + floorOffset ) * floorSize;
    }

    public void resetZilla(bool isToNextFloor = false)
    {
        isChomping = false;
        TimerRestart();
        zillaR.StopChomping();
        zillaL.StopChomping();

        zillaR.transform.SetLocalPositionAndRotation(
            new Vector3(zillaOffset, CalcNextFloorY(), zillaR.transform.position.z), Quaternion.identity);

        zillaL.transform.SetLocalPositionAndRotation(
            new Vector3(-zillaOffset, CalcNextFloorY(), zillaL.transform.position.z), Quaternion.identity);
        
        if (isToNextFloor) {
            nextFloorToDestroy ++;
        }
    }

    public bool isBuildingBotttom(){
        return GetFloorController().hasSceneExitDoor();
    }

}
