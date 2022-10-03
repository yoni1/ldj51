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
        }
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
            } else
            {
                Destroy(curObject);
            }
        }
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(obj);
        }
    }
}
