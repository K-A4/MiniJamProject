using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CameraItem item;

    public float MoveSpeed;
    public GameObject PhotoMask;

    private void Start()
    {
        
    }

    private void Update()
    {
        Vector3 inputVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        inputVec = Vector3.ClampMagnitude(inputVec, 1.0f);
        var moveVec = transform.right * inputVec.x + transform.up * inputVec.z;
        transform.position += moveVec * Time.deltaTime * MoveSpeed;

        //var lookDirection = Input.mousePosition - new Vector3( Screen.height / 2, Screen.width / 2, 0);
        Vector3 lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //Debug.DrawRay(transform.position, mouseDir);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            item.Use(transform.position, lookDirection);
        }

        var angle =Mathf.Atan2(lookDirection.y, lookDirection.x);
        PhotoMask.transform.rotation = Quaternion.Euler(0, 0, 45 +180+ (angle * Mathf.Rad2Deg));
    }
}
