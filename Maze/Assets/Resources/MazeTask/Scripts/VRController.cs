using UnityEngine;

public class VRController : MonoBehaviour {

    public float m_Sensitivity = 0.2f;

    //public SteamVR_Action_Boolean m_MovePress = null;
    //public SteamVR_Action_Vector2 m_MoveValue = null;

    private float m_Speed = 0.0f;

    private CharacterController m_CharacterControler = null;
    private Transform m_CameraRig = null;
    private Transform m_Head;

    private void Awake()
    {
        m_CharacterControler = GetComponent<CharacterController>();
    }

    private void Start () {
        m_CameraRig = transform.GetChild(0);// SteamVR_Render.Top().origin;
        m_Head = GetComponentInChildren<Camera>().transform;//SteamVR_Render.Top().head;
	}
	
	private void Update () {
        HandleHead();
        HandleHeight();
        CalculateMovement();
    }

    private void HandleHead()
    {
        //Store current
        Vector3 oldPosition = m_CameraRig.position;
        Quaternion oldRotation = m_CameraRig.rotation;

        // Rotation
        transform.eulerAngles = new Vector3(0.0f, m_Head.rotation.eulerAngles.y, 0.0f);

        // Restore
        m_CameraRig.position = oldPosition;
        m_CameraRig.rotation = oldRotation;
        

    }

    private void CalculateMovement()
    {
        // figure out movement orientation
        Vector3 orientationEuler = new Vector3(0, transform.eulerAngles.y, 0);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        //// if not moving
        //if(m_MovePress.GetStateUp(SteamVR_Input_Sources.Any))
        //{
        //    m_Speed = 0;
        //}
        //
        //// if button pressed
        //if (m_MovePress.state)
        //{
        //    // set speed
        //    m_Speed = 2.0f;
        //
        //    // orientation
        //    movement += orientation * (m_Speed * Vector3.forward) * Time.deltaTime;
        //
        //}

        if (Input.GetKey(KeyCode.UpArrow))
        {
            // set speed
            m_Speed = 2.0f;

            // orientation
            movement += orientation * (m_Speed * Vector3.forward) * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            // set speed
            m_Speed = -2.0f;

            // orientation
            movement += orientation * (m_Speed * Vector3.forward) * Time.deltaTime;
        }

        // apply
        m_CharacterControler.Move(movement);
    }

    private void HandleHeight()
    {
        // get the head in local space
        float headHeight = Mathf.Clamp(m_Head.localPosition.y, 1, 2);
        m_CharacterControler.height = headHeight;

        // cut in half
        Vector3 newCenter = Vector3.zero;
        newCenter.y = m_CharacterControler.height / 2;
        newCenter.y += m_CharacterControler.skinWidth;

        // move capsule in local space
        newCenter.x = m_Head.localPosition.x;
        newCenter.z = m_Head.localPosition.z;

        // rotate
        newCenter = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCenter;

        // apply
        m_CharacterControler.center = newCenter;
    }
}
