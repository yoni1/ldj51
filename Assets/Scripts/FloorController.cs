using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    private List<Vector3> objectPositions;
    private List<bool> objectStates;
    // Start is called before the first frame update
    void Start()
    {
        objectPositions = new List<Vector3>();
        objectStates = new List<bool>();
        for (int i = 0; i < transform.childCount; i++)
        {
            objectPositions.Add(transform.GetChild(i).position);
            objectStates.Add(transform.GetChild(i).gameObject.activeSelf);
        }
    }

    public void ResetPositions()
    {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).position = objectPositions[i];
            transform.GetChild(i).gameObject.SetActive(objectStates[i]);
        }
    }
}
