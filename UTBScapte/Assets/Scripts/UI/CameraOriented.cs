using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CameraOriented : MonoBehaviour {

    private Player player;

    private void Start()
    {
        player = Player.instance;
    }
    void Update()
    {
        Vector3 v = player.trackingOriginTransform.position - transform.position;
        v.x = v.z = 0.0f;
        transform.LookAt(player.trackingOriginTransform.position - v);
    }
}
