using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VirtualCameraController : MonoBehaviour
{
    public GameObject camConfiner0;
    public GameObject camConfiner1;
    public GameObject camConfiner2;
    public float floorOffset = 9.2f;

    private List<GameObject> vCamConfiners;
    private int currentCamIdx;

    // Start is called before the first frame update
    void Start()
    {
        vCamConfiners = new List<GameObject>()
        {
            camConfiner0,
            camConfiner1,
            camConfiner2
        };
        currentCamIdx = 0;

        foreach (GameObject confiner in vCamConfiners)
        {
            confiner.transform.GetChild(0).gameObject.SetActive(false);
        }

        camConfiner0.transform.GetChild(0).gameObject.SetActive(true);
        updateCamPriorities(currentCamIdx);
    }

    private void updateCamPriorities(int highPriorityCam)
    {
        for (int i = 0; i < vCamConfiners.Count; i++)
        {
            int idx = (highPriorityCam + i) % vCamConfiners.Count;
            CinemachineVirtualCamera vcam = vCamConfiners[idx].transform.
                GetChild(0).gameObject.GetComponent<CinemachineVirtualCamera>();
            vcam.m_Priority = 100 + vCamConfiners.Count - i;
        }
    }

    private int nextCamIdx(int curIdx)
    {
        return (curIdx + 1) % vCamConfiners.Count;
    }

    public void NextFloor()
    {
        print("Going to next floor, disabling cam: " + currentCamIdx);
        vCamConfiners[currentCamIdx].transform.GetChild(0).gameObject.
            SetActive(false);

        currentCamIdx = nextCamIdx(currentCamIdx);
        print("Current camIdx is now: " + currentCamIdx);
        vCamConfiners[currentCamIdx].transform.GetChild(0).gameObject.
            SetActive(true);
        print("Set that can to active");
        updateCamPriorities(currentCamIdx);

        print("Moving camera at index: " + nextCamIdx(currentCamIdx) + " cur position: " + vCamConfiners[nextCamIdx(currentCamIdx)].transform.position);
        // Move the camera after the next one to prepare it for the next switch
        vCamConfiners[nextCamIdx(currentCamIdx)].transform.Translate(new Vector3(0f, -3f * floorOffset, 0f));
        print("New position: " + vCamConfiners[nextCamIdx(currentCamIdx)].transform.position);
    }
}
