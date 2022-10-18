using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    public float Speed = 1f;
    CharacterController charactercontroller;

    // Start is called before the first frame update
    void Start()
    {
          charactercontroller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
      float right = 0f;
      float forward = 0f;
      if (Input.GetKey(KeyCode.D))
        right += Speed;
      if (Input.GetKey(KeyCode.A))
        right -= Speed;
      if (Input.GetKey(KeyCode.W))
        forward += Speed;
      if (Input.GetKey(KeyCode.S))
        forward -= Speed;

      Vector3 velocity = new Vector3(right, -1f, forward);
      Vector2 worldvelcotiy = transform.TransformVector(velocity);
      charactercontroller.Move(worldvelcotiy * Time.deltaTime);
    }
}
