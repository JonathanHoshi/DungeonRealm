using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float trackingIntensity = 1;

    private Transform playerRef;

    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameManager.instance.PlayerRef.transform;
        this.transform.position = playerRef.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, playerRef.position, trackingIntensity);
    }
}
