using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectState
{
    public Vector3 position;
    public bool isEnabled;

    public ObjectState(Vector3 position, bool isEnabled)
    {
        this.position = position;
        this.isEnabled = isEnabled;
    }
}

public class FloorController : MonoBehaviour
{
    private Dictionary<int, ObjectState> objectStates;

    private static readonly int FURNITURE_LAYER = 20;

    // Start is called before the first frame update
    void Start()
    {
        objectStates = new Dictionary<int, ObjectState>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject curObject = transform.GetChild(i).gameObject;
            objectStates.Add(curObject.GetInstanceID(),
                new ObjectState(
                    curObject.transform.position, curObject.activeSelf));
            Rigidbody2D curRb = curObject.GetComponent<Rigidbody2D>();
            if (curRb != null && curObject.layer == FURNITURE_LAYER)
            {
                curRb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }

    public void MakeFurnitureMovable()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject curObject = transform.GetChild(i).gameObject;
            Rigidbody2D curRb = curObject.GetComponent<Rigidbody2D>();
            if (curRb != null && curObject.layer == FURNITURE_LAYER)
            {
                curRb.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
    
    public Vector3 GetRelativeSpawnLocation(){
        foreach (Transform child in transform) {
            if (child.name == "SpawnPoint") {
                return child.transform.position;
            }
        }
        print("ERROR - could not find spawn point child object for floor.");
        return new Vector3(0,0,0);
    }

    public void ResetPositions()
    {
        for (int i = 0; i < transform.childCount; i++) {
            GameObject curObject = transform.GetChild(i).gameObject;
            if (objectStates.ContainsKey(curObject.GetInstanceID()))
            {
                ObjectState originalState = objectStates[
                    curObject.GetInstanceID()];
                curObject.transform.position = originalState.position;
                curObject.SetActive(originalState.isEnabled);
            }
            Rigidbody2D curRb = curObject.GetComponent<Rigidbody2D>();
            if (curRb != null && curObject.layer == FURNITURE_LAYER)
            {
                curRb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(obj);
        }
    }

    public void VoiceAnnounce()
    {
        AudioSource announement = GetComponent<AudioSource>();
        print(announement);
        if (announement != null) {
            announement.Play();
        }
    }

}
