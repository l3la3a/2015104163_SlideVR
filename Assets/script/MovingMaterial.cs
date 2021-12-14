using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingMaterial : MonoBehaviour
{

    Material mat;
    float f = 0f;
    public float speed = 0.003f;

    void Start()
    {
        mat = GetComponent<MeshRenderer>().material;    
    }

    // Update is called once per frame
    void Update()
    {
        float Offset = Time.time * speed;

        f += speed;
        mat.mainTextureOffset = new Vector2(Offset, 0);
    }
}
