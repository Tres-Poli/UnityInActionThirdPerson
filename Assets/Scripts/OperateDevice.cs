using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperateDevice : MonoBehaviour
{
    public float Radius = 1.5f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var hitColliders = Physics.OverlapSphere(transform.position, Radius);

            foreach (var collider in hitColliders)
            {
                var direction = collider.transform.position - transform.position;
                if (Vector3.Dot(transform.forward, direction) > 0.5)
                {
                    collider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
