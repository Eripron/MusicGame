using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool canMove = true;


    [Header("Move")]
    [SerializeField] float moveSpeed = 3;
    Vector3 dir     = new Vector3();
    Vector3 destPos = new Vector3();

    [Header("Rotate")]
    [SerializeField] float spinSpeed = 360;
    Vector3 rotDir      = new Vector3();
    Quaternion destRot  = new Quaternion();

    [SerializeField] Transform fakeCube = null;
    [SerializeField] Transform realCube = null;

    [Header("Recoil")]
    [SerializeField] float recoilPosY = 0.25f;
    [SerializeField] float recoilSpeed = 1.5f;

    TimingManager theTimingManager;

    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();     
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            if(canMove && theTimingManager.CheckTiming())
            {
                StartAction();
            }
        }
    }

    void StartAction()
    {
        // 방향 계산
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));
        // 이동 목표 값 계산 
        destPos = transform.position + new Vector3(-dir.x, 0, dir.z);

        // 회전 축 결정 
        rotDir = new Vector3(-dir.z, 0f, -dir.x);
        // RotateAround : 축 기준 왼손방향 회전 
        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);
        destRot = fakeCube.rotation;

        StartCoroutine(MoveCo());
        StartCoroutine(SpinCo());
        StartCoroutine(RecoilCo());
    }


    IEnumerator MoveCo()
    {
        canMove = false;

        // Vector3.Distance 보다 가벼움 
        while (Vector3.SqrMagnitude(transform.position - destPos) >= 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = destPos;
        canMove = true;
    }

    IEnumerator SpinCo()
    {
        while(Quaternion.Angle(realCube.rotation, destRot) > 0.5f)
        {
            realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destRot, spinSpeed * Time.deltaTime);
            yield return null;
        }

        realCube.rotation = destRot;
    }

    IEnumerator RecoilCo()
    {
        while(realCube.position.y < recoilPosY)
        {
            realCube.position += new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }
        while(realCube.position.y > 0)
        {
            realCube.position -= new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        realCube.localPosition = Vector3.zero;
    }

}
