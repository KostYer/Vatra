using System;
using UnityEngine;

namespace DebugDraw
{
    public class PointGizmos : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.2f);
        }
    }
}