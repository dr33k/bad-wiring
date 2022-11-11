using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Vector3 electronPosition;
    // Start is called before the first frame update
    void Start()
    {
      electronPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position.Set(electronPosition.x,electronPosition.y,electronPosition.z);
    }
}
