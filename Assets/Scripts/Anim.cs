using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    Vector3 dir;
    void Update()
    {
        transform.Translate(dir * Time.deltaTime);
    }

    // Update is called once per frame
    public void restAnim()
    {
        transform.position = Vector3.zero;
        dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), -1).normalized;
        
    }
}
