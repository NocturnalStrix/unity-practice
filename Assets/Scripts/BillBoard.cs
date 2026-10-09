using UnityEngine;

public class BillBoard : MonoBehaviour
{
    public Transform cam;

    void LateUpdate()
    {
        transform.LookAt(transform.position+cam.forward);
    }
}
