using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] Transform[] positions;
    [SerializeField] float objectSpeed;

    Transform nextPos;
    int nextPosIndex;

    private void Start()
    {
        nextPos = positions[0];
    }

    private void Update()
    {
        MoveGameObject();
    }

    void MoveGameObject()
    {
        if (transform.position == nextPos.position)
        {
            nextPosIndex++;
            if (nextPosIndex >= positions.Length)
            {
                nextPosIndex = 0;
            }
            nextPos = positions[nextPosIndex];
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, objectSpeed * Time.deltaTime);
        }

    }
}
