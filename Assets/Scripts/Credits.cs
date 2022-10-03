using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
     public float scrollSpeed = 0.5F;
     public Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
         rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         float offset = Time.time * scrollSpeed;
         //rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));        
        transform.position = new Vector3(0, offset, 0);
    }
}
