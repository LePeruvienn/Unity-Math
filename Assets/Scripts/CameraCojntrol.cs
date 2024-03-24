using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCojntrol : MonoBehaviour
{
    public Transform player;
    public float treshold;

    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 targetPos = (player.position + mousePos)/2f;

        targetPos.x = Mathf.Clamp(targetPos.x,-treshold + player.position.x, treshold + player.position.x);
        targetPos.y = Mathf.Clamp(targetPos.y, -treshold + player.position.y, treshold + player.position.y);
        targetPos.z = -10;

        this.transform.position = targetPos;
    }
}
