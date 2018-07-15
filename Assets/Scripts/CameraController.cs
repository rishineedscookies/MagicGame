using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private GameObject player;

    public Vector3 offset = new Vector3(0f, 0f, 0f);

    public float maximumX = 90f;
    public float minimumX = -90f;
    public float maximumY = 360f;
    public float minimumY = -360f;

    public float sensitivityX = 10f;
    public float sensitivityY = 10f;

    private float rotationX = 0f;
    private float rotationY = 0f;



	// Use this for initialization
	void Start () {

        Cursor.lockState = CursorLockMode.Locked;

        if(Input.GetKey(KeyCode.Escape)) {

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

	}
	
	// Update is called once per frame
	void Update () {



        rotationX += Input.GetAxis("Mouse Y") * sensitivityX;
        rotationY += Input.GetAxis("Mouse X") * sensitivityY;

        rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
       

        player.transform.localEulerAngles = new Vector3(0, rotationY, 0);
        transform.eulerAngles = new Vector3(-rotationX, rotationY, 0);

        transform.position = player.transform.position + offset;



	}
}
