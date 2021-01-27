using UnityEngine;
using UnityEngine.XR;
using uniwue.hci.vilearn;

public class XRController : MonoBehaviour {

    public float sensitivity = 0.2f;

    private float speed = 0.0f;
    private CharacterController characterController = default;
    private Transform rig = default;
    private Transform head = default;

    private GameState gameState;

    private void Start () {
        gameState = GameState.Instance;

        rig = gameState.GetPlayer().GetChild(0);
        head = gameState.GetPlayerCamera();
        characterController = gameState.GetPlayer().GetComponentInChildren<CharacterController>();
        //characterController.detectCollisions = false;

    }
	
	private void Update () {
        HandleHead();
        HandleHeight();
        CalculateMovement();
    }

    private void HandleHead()
    {
        //Store current
        Vector3 oldPosition = rig.position;
        Quaternion oldRotation = rig.rotation;

        // Rotation
        transform.eulerAngles = new Vector3(0.0f, head.rotation.eulerAngles.y, 0.0f);

        // Restore
        rig.position = oldPosition;
        rig.rotation = oldRotation;
        

    }

    private void CalculateMovement()
    {
        if (!gameState.isWalkingEnabled)
            return;

        // figure out movement orientation
        Vector3 orientationEuler = new Vector3(0, transform.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        bool buttonPressed = gameState.GetXrControllerInput(XRNode.RightHand).primary2DAxisButton;
        buttonPressed |= gameState.GetXrControllerInput(XRNode.LeftHand).primary2DAxisButton;
        buttonPressed |= Input.GetKey(gameState.desktopWalkForward);

        // if button pressed
        if (buttonPressed)
        {
            // set speed
            speed = 2.0f;

            // orientation
            movement += orientation * (speed * Vector3.forward) * Time.deltaTime;
        }
        else
        {
            speed = 0f;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            // set speed
            speed = 2.0f;

            // orientation
            movement += orientation * (speed * Vector3.forward) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            // set speed
            speed = -2.0f;

            // orientation
            movement += orientation * (speed * Vector3.forward) * Time.deltaTime;
        }

        // apply
        characterController.Move(movement);
    }

    private void HandleHeight()
    {
        // get the head in local space
        float headHeight = Mathf.Clamp(head.localPosition.y, 1, 2);
        characterController.height = headHeight;

        // cut in half
        Vector3 newCenter = Vector3.zero;
        newCenter.y = characterController.height / 2;
        newCenter.y += characterController.skinWidth;

        // move capsule in local space
        newCenter.x = head.localPosition.x;
        newCenter.z = head.localPosition.z;

        // rotate
        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

        // apply
        characterController.center = newCenter;
    }
}
