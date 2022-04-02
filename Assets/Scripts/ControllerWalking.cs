using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerWalking : MonoBehaviour
{
    public GameObject RightHand;
    public GameObject LeftHandRay;
    public GameObject ForwardDirection;

    private Vector3 PositionPreviousFrameRightHand;
    private Vector3 PositionThisFrameRightHand;
    private Vector3 PlayerPositionPreviousFrame;
    private Vector3 PlayerPositionThisFrame;

    public float Speed = 350;
    public float HandSpeed;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPositionPreviousFrame = transform.position;
        PositionPreviousFrameRightHand = RightHand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float yRotation = LeftHandRay.transform.eulerAngles.y;
        ForwardDirection.transform.eulerAngles = new Vector3(0, yRotation, 0);

        PositionThisFrameRightHand = RightHand.transform.position;
        PlayerPositionThisFrame = transform.position;

        var playerDistanceMoved = Vector3.Distance(PlayerPositionThisFrame, PlayerPositionPreviousFrame);
        var rightHandDistanceMoved = Vector3.Distance(PositionThisFrameRightHand, PositionPreviousFrameRightHand);

        HandSpeed = Mathf.Abs(rightHandDistanceMoved - playerDistanceMoved);

        if (Time.timeSinceLevelLoad > 1f)
        {
            transform.position += ForwardDirection.transform.forward * HandSpeed * Speed * Time.deltaTime;
        }

        PlayerPositionPreviousFrame = PlayerPositionThisFrame;
        PositionPreviousFrameRightHand = PositionThisFrameRightHand;
    }
}