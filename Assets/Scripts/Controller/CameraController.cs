using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform player = null;
    [SerializeField] float followSpeed = 15f;

    Vector3 playerDistance = new Vector3();

    [SerializeField] float zoomDistance = -1.25f;
    float hitDistance = 0f;

    void Start()
    {
        playerDistance = transform.position - player.position;     
    }

    void LateUpdate()
    {
        Vector3 t_destPos = player.position + playerDistance + (hitDistance * transform.forward);
        transform.position = Vector3.Lerp(transform.position, t_destPos, followSpeed * Time.deltaTime);
    }

    // 임시로 사용 
    WaitForSeconds waitSeconds = new WaitForSeconds(0.15f);

    public IEnumerator ZoomCam()
    {
        hitDistance = zoomDistance;

        yield return waitSeconds;

        hitDistance = 0.0f;
    }
}
