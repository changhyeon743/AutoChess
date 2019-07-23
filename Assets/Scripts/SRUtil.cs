using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SRUtil
{
    public static bool Detect(Camera sight, float aspect, CharacterController cc)
    {
        sight.aspect = aspect;
        Plane[] ps = GeometryUtility.CalculateFrustumPlanes(sight);
        return GeometryUtility.TestPlanesAABB(ps, cc.bounds);
    }

    public static void SRMove(
        CharacterController cc,
        Transform self,
        Vector3 targetPos,
        float moveSpeed,
        float rotateSpeed,
        float fallSpeed
    )
    {
        Vector3 deltaMove = Vector3.MoveTowards(
            self.transform.position,
            targetPos,
            Time.deltaTime * moveSpeed
            ) - self.position;

        //transform.position += deltaMove;
        //deltaMove.y = -fallSpeed * Time.deltaTime;
        cc.Move(deltaMove);
        //Debug.Log("RUN");
        //transform.position += manager.marker.position - transform.position;

        Vector3 diff = targetPos - self.position;
        diff.y = 0;
        //Debug.Log(diff);
        if (diff != Vector3.zero)
        {
            self.rotation = Quaternion.RotateTowards(
                self.rotation,
                Quaternion.LookRotation(diff),
                rotateSpeed * Time.deltaTime);
        }

    }
}