using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
     public float scrollSpeed = 0.8F;
     public float stopY = 0.0F;
     public Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
         rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.deltaTime * scrollSpeed;
        if (transform.position.y <= stopY)
            transform.position += new Vector3(0, offset, 0);
    }
}
