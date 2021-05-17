using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public enum State {
        Idle,
        Ready,
        Tracking,
    }

    private State state {
        set {
            switch (value) {
                case State.Idle:
                    break;
                case State.Ready:
                    break;
                case State.Tracking:
                    break;
            }
        }
    }
    private Transform target;

    private void Awake() {
        state = State.Idle;
    }
}
