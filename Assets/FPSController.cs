using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float Speed = 1f;
    public float Jump = 10f;
    public GameObject Block; 

    CharacterController charactercontroller;
    Camera headCamera;
    Vector3 velocity = Vector3.zero;
    float bodyAngle = 0f;
    float headAngle = 0f;
    // Start is called before the first frame update
    void Start()
    {
          charactercontroller = GetComponent<CharacterController>();
          headCamera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        bodyAngle += 10f * Input.GetAxis("Mouse X");
        headAngle += 10f * Input.GetAxis("Mouse Y");
        headAngle = Mathf.Clamp(headAngle, -90f, 90f);

        transform.rotation = Quaternion.Euler(0f, bodyAngle, 0f);
        headCamera.transform.localRotation = Quaternion.Euler(headAngle, 0f, 0f);

        float right = 0f;
        float forward = 0f;
        float up = 0f;
        if (Input.GetKey(KeyCode.D))
            right += Speed;
        if (Input.GetKey(KeyCode.A))
            right -= Speed;
        if (Input.GetKey(KeyCode.W))
            forward += Speed;
        if (Input.GetKey(KeyCode.S))
            forward -= Speed;
        if (charactercontroller.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
                up += Jump;
        }
        else
        {
            up = velocity.y - 9.82f * Time.deltaTime;
        }

        right = Mathf.Lerp(velocity.x, right, 5f * Time.deltaTime);
        forward = Mathf.Lerp(velocity.z, forward, 5f * Time.deltaTime);

        velocity = new Vector3(right, up, forward);
        Vector3 worldveloctiy = transform.TransformVector(velocity);
        charactercontroller.Move(worldveloctiy * Time.deltaTime);

        RaycastHit hit;
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        if (Physics.Raycast(headCamera.transform.position, headCamera.transform.forward, out hit, 100, layerMask));
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Instantiate<GameObject>(Block, hit.transform.position + hit.normal, hit.transform.rotation);
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                Instantiate<GameObject>(Block, hit.point + 0.5f * hit.normal, Quaternion.LookRotation(hit.normal, Vector3.up);
            }

            if (Input.GetKeyDown(KeyCode.Z))
                Destroy(hit.transform.gameObject);
        }
    }
}
