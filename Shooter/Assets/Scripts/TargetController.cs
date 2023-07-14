using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if (FindAnyObjectByType<PlayerController>().PlayerModel.IsGameOn)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            transform.position = mousePosition;
        }
    }
}
