using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectState
{
    public Vector3 position;
    public Quaternion rotation;
    public bool isEnabled;

    public ObjectState(Vector3 position, Quaternion rotation, bool isEnabled)
    {
        this.position = position;
        this.rotation = rotation;
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
                    curObject.transform.position, curObject.transform.rotation, curObject.activeSelf));
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
    
    private Transform findChild(string childName)
    {
        foreach (Transform child in transform) {
            if (child.name == childName) {
                return child;
            }
        }
        return null;
    }
    
    public Vector3 GetRelativeSpawnLocation(){
        Transform child = findChild("SpawnPoint");
        if (child) {
            return child.transform.position;
        }
        print("ERROR - could not find child object for floor called SpawnPoint");
        return new Vector3(0,0,0);
    }

    public bool hasSceneExitDoor(){
        return findChild("SceneExitDoor") ? true : false;
    }

    public void SkipLevel(){
        Transform exitDoor = findChild("ExitDoor");
        if (!exitDoor)
        {
        exitDoor = findChild("SceneExitDoor");
        }
        exitDoor.GetComponent<ExitDoorCollider>().UseExitDoor();
    }

    public AudioSource GetNewMusic(){
        Transform music = findChild("Music");
        if (!music) {
            return null;
        }
        return music.GetComponent<AudioSource>();
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
                curObject.transform.rotation = originalState.rotation;
                curObject.SetActive(originalState.isEnabled);
            }
            Rigidbody2D curRb = curObject.GetComponent<Rigidbody2D>();
            if (curRb != null && curObject.layer == FURNITURE_LAYER)
            {
                curRb.bodyType = RigidbodyType2D.Kinematic;
                curRb.gravityScale = 0;
                curRb.velocity = Vector3.zero;
                curRb.Sleep(); // https://stackoverflow.com/questions/58914962/rigidbody-not-stopping-instantly-when-setting-its-velocity-to-0
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
        //print(announement);
        if (announement != null) {
            announement.Play();
        }
    }

}
