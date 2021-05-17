using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    //Canvas Scaler
    //Constant Pixel Size
    //Ui 요소의 크기가 변하지 않음
    //해상도가 바뀌어도 UI 크기가 변하지 않음
    //해상도에 따라 UI 크기가 너무 작아지는 문제점 발생
    //Scale With Screen Size
    //기준 해상도를 정하면 해상도가 변경되어도
    //UI 요소의 크기를 변경한다
    // 640 * 360이 가장 흔하다

    //Horizontal Layout Group
    //Control Child Size
    //자식들의 사이즈를 강제로 잡아 늘림
    //빈 공간을 없애준다
    //Use Child Scale

    //Child force Expand

    public enum State {
        Idle,
        Ready,
        Tracking,
    }

    //프로퍼티 - 바깥에서 보면 변수지만 안에서 보면 함수
    //사용할 때는 변수처럼 사용, 내부에서는 함수처럼 작동
    private State state {
        set {
            switch (value) {
                case State.Idle:
                    targetZoomSize = roundReadyZoomSize;
                    break;
                case State.Ready:
                    targetZoomSize = readyShotZoomSize;
                    break;
                case State.Tracking:
                    targetZoomSize = trackingZoomSize;
                    break;
            }
        }
    }
    private Transform target;

    public float smoothTime = 0.2f;
    private Vector3 lastMovingVelocity;
    private Vector3 targetPosition;

    private Camera cam;
    private float targetZoomSize = 14.5f;

    private const float roundReadyZoomSize = 25.0f;
    private const float readyShotZoomSize = 14.5f;
    private const float trackingZoomSize = 10f;

    private float lastZoomSpeed;
    

    private void Awake() {
        cam = GetComponentInChildren<Camera>();
        state = State.Idle;
    }

    private void Move() {
        targetPosition = target.transform.position;

        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition,
            ref lastMovingVelocity, smoothTime);

        transform.position = targetPosition;
    }

    private void Zoom() {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoomSize, ref lastZoomSpeed, smoothTime);

        cam.orthographicSize = smoothZoomSize;
    }

    private void FixedUpdate() {
        if(target != null) {
            Move();
            Zoom();
        }
    }

    public void Reset() {
        state = State.Idle;
    }

    public void SetTarget(Transform newTarget, State newState) {
        target = newTarget;
        state = newState;
    }
}
