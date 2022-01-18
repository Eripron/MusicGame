using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool isCanPressKey = true;

    // reference
    TimingManager theTimingManager;
    CameraController cam;
    Rigidbody rigid;

    // data
    bool canMove = true;
    bool isFalling = false;


    [SerializeField] LayerMask plateLayer;


    [Header("Move")]
    [SerializeField] float moveSpeed = 3;
    Vector3 dir     = new Vector3();
    Vector3 destPos = new Vector3();
    Vector3 originPos = new Vector3();

    [Header("Rotate")]
    [SerializeField] float spinSpeed = 180;
    [SerializeField] Transform fakeCube = null;
    [SerializeField] Transform realCube = null;

    Quaternion destRot  = new Quaternion();
    Vector3 rotDir      = new Vector3();


    [Header("Recoil")]
    [SerializeField] float recoilPosY = 0.25f;
    [SerializeField] float recoilSpeed = 1.5f;


    public Vector3 Destination
    {
        get
        {
            return destPos;
        }
    }


    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
        cam = FindObjectOfType<CameraController>();
        rigid = GetComponentInChildren<Rigidbody>();
        originPos = transform.position;
    }


    void Update()
    {
        CheckFalling();

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            if (canMove && isCanPressKey && !isFalling)
            {
                Calc();

                if (theTimingManager.CheckTiming())
                {
                    StartAction();
                }
            }
        }
    }

    void StartAction()
    {
        StartCoroutine(MoveCo());
        StartCoroutine(SpinCo());
        StartCoroutine(RecoilCo());
        StartCoroutine(cam.ZoomCam());
    }

    IEnumerator MoveCo()
    {
        // Vector3.Distance 보다 가벼움 
        while (Vector3.SqrMagnitude(transform.position - destPos) >= 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = destPos;
    }
    IEnumerator SpinCo()
    {
        canMove = false;

        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);
        destRot = fakeCube.rotation;

        while (Quaternion.Angle(realCube.rotation, destRot) > 0.1f)
        {
            realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destRot, spinSpeed * Time.deltaTime);
            yield return null;
        }

        realCube.rotation = destRot;
        canMove = true;
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


    void Calc()
    {
        // 방향 계산
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));
        // 이동 목표 값 계산 
        destPos = transform.position + new Vector3(dir.z, 0, dir.x);

        // 회전 축 결정 
        rotDir = new Vector3(-dir.x, 0f, dir.z);

        // RotateAround : 축 기준 왼손방향 회전 
        //fakeCube.RotateAround(transform.position, rotDir, spinSpeed);
        //destRot = fakeCube.rotation;
    }

    void CheckFalling()
    {
        if (!isFalling && canMove)
        {
            if (!Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, 1.0f, plateLayer))
                Falling();
        }
    }

    void Falling()
    {
        isFalling = true;
        rigid.useGravity = true;
        rigid.isKinematic = false;
    }


    public void ResetFalling()
    {
        isFalling = false;
        rigid.useGravity = false;
        rigid.isKinematic = true;

        transform.position = originPos;
        realCube.localPosition = new Vector3(0, 0, 0);
    }
}
