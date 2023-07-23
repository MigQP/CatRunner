using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    Vector3 offset;

    public Transform camera2D_T;

    public Vector3 offset2D;

    public Camera camera3D;
    public Camera camera2D;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position;
        //camera3D.enabled = true;
        //camera2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Obtener posicion de player y generar offset para camaras

        Vector3 targetPos = player.position + offset;
        targetPos.x = 0;
        transform.position = new Vector3(targetPos.x, 4, targetPos.z);

        //transform.position = targetPos;

        camera2D_T.position = transform.position + offset2D;  

        if (Input.GetMouseButtonDown(1))
        {
            camera3D.enabled = !camera3D.enabled;
            camera2D.enabled = !camera2D.enabled;
            GameManager.inst.isGame2d = !GameManager.inst.isGame2d;
        }
    }
}
