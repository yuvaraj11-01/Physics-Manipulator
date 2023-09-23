using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionSensors : MonoBehaviour
{

    [SerializeField] Vector2 leftCeling, rightCeling;
    [SerializeField] float celingHeight;

    [SerializeField] Vector2 leftGround, rightGround;
    [SerializeField] float groundDepth;

    [SerializeField] LayerMask groundLayer;

    Vector3 GetLeftCelingPos() => transform.position + (Vector3)leftCeling;
    Vector3 GetRightCelingPos() => transform.position + (Vector3)rightCeling;

    Vector3 GetLeftGroundPos() => transform.position + (Vector3)leftGround;
    Vector3 GetRightGroundPos() => transform.position + (Vector3)rightGround;

    bool OnleftCeling()
    {
        var hit = Physics2D.Raycast(GetLeftCelingPos(), Vector2.up, celingHeight, groundLayer);
        if(hit.collider != null)
        {
            return true;
        }
        return false;
    }

    bool OnRightCeling()
    {
        var hit = Physics2D.Raycast(GetRightCelingPos(), Vector2.up, celingHeight, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }


    bool OnLeftGround()
    {
        var hit = Physics2D.Raycast(GetLeftGroundPos(), Vector2.down, groundDepth, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }


    bool OnrightGround()
    {
        var hit = Physics2D.Raycast(GetRightGroundPos(), Vector2.down, groundDepth, groundLayer);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    public bool IsCeiled()
    {
        return OnleftCeling() || OnRightCeling();
    }

    public bool IsGrounded()
    {
        return OnLeftGround() || OnrightGround();
    }


    private void OnDrawGizmosSelected()
    {
        if(OnleftCeling()) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;
        Gizmos.DrawLine(GetLeftCelingPos(), GetLeftCelingPos() + Vector3.up * celingHeight);

        if (OnRightCeling()) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;
        Gizmos.DrawLine(GetRightCelingPos(), GetRightCelingPos() + Vector3.up * celingHeight);

        if (OnLeftGround()) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;
        Gizmos.DrawLine(GetLeftGroundPos(), GetLeftGroundPos() + Vector3.down * groundDepth);

        if (OnrightGround()) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;
        Gizmos.DrawLine(GetRightGroundPos(), GetRightGroundPos() + Vector3.down * groundDepth);

    }


}
