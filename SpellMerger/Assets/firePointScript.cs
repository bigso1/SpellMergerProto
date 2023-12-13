using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firePointScript : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 mousePos;
    private Vector3 myRot;
    private float rotZ;
    private Plane plane;
    void Start()
    {
        cam = Camera.main;
        plane = new Plane(Vector3.forward, player.position);
        // player = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.right = GetDirection();
    }

    public Vector3 GetDirection()
    {
        var ray = cam.ScreenPointToRay(Input.mousePosition);
        if(!plane.Raycast(ray, out var hit)) return Vector3.zero;
        mousePos = ray.GetPoint(hit);
        var playerPos = player.position;
        return (mousePos - playerPos).normalized;
    }
    
}
