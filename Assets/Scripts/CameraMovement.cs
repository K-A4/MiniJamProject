using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform trackTransform;
    public Transform PlayerTransform;
    public float LerpSpeed;
    public float MoveSpeed;
    public float Distance;
    //[SerializeField] private float Speed;
    private Vector3 prevPos;
    private float Yangle = 0;
    private float Xangle = 0;
    private Quaternion previousRotation;
    private Vector3 Pos;

    private void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Distance = (PlayerTransform.position - transform.position).magnitude;
        previousRotation = transform.rotation;
        Cursor.lockState =  CursorLockMode.Locked;
    }

    private void Update()
    {
        //var deltaMove = trackTransform.position - prevPos;
        //Pos += deltaMove.normalized * Time.deltaTime * Speed;
        //Pos = Vector3.Lerp(Pos, transform.position, Time.deltaTime * 10);
        //transform.position = Pos + trackTransform.position;

        //transform.position = Vector3.Lerp(transform.position, trackTransform.position, Time.deltaTime * LerpSpeed);
        var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X") , Input.GetAxisRaw("Mouse Y"));
        var joyDelta = new Vector2(Input.GetAxisRaw("AxisX"), Input.GetAxisRaw("AxisY"));
        Distance -= Input.GetAxisRaw("Mouse ScrollWheel") * 10;
        if (mouseDelta == Vector2.zero)
        {
            mouseDelta += joyDelta;
        }
        var rot = Quaternion.LookRotation(PlayerTransform.position - transform.position, Vector3.up);
        var playerF = PlayerTransform.forward;
        var delta = ((PlayerTransform.rotation * Quaternion.Inverse(previousRotation)).eulerAngles.y);
        delta = delta < 180.0f ? delta : delta - 360.0f;

        Xangle += mouseDelta.x;
        
        Yangle -= mouseDelta.y;
        Yangle = Mathf.Clamp(Yangle, 0, 90);
        var rotDelta = Quaternion.Euler(Yangle, Xangle, 0);
        //rotDelta = rotDelta * rot;
        //var rotToPlayer = PlayerTransform.rotation * Quaternion.Inverse(transform.rotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotDelta, Time.deltaTime * LerpSpeed);
        //transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * LerpSpeed / 10.0f);
        //transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * LerpSpeed / 2.0f);
        // Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, rotToPlayer.eulerAngles.y, 0) * transform.rotation,  LerpSpeed * Time.deltaTime);
        var toCamera = transform.position - PlayerTransform.position;
        var deltaRot = transform.rotation * Quaternion.Inverse(previousRotation);
        //angle += deltaRot.eulerAngles.y;
        //print(deltaRot);
        transform.position = Vector3.Slerp(transform.position, trackTransform.position - (transform.forward * Distance), MoveSpeed * Time.deltaTime);
        //transform.position = PlayerTransform.position + (Quaternion.Euler(0, angle, 0) * toCamera.normalized * Distance);
        previousRotation = transform.rotation;
    }
}