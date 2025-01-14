﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jennings {
    public class CameraController : MonoBehaviour {

        public float easeMultiplier = 10;
        public Transform lookTarget;

        public float zoomValue = 10;
        Camera cam;

        // Start is called before the first frame update
        void Start()
        {
            cam = GetComponentInChildren<Camera>();
        }

        void Update()
        {
            zoomValue += Input.mouseScrollDelta.y * 2;

            zoomValue = Mathf.Clamp(zoomValue, 5, 50);

        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (lookTarget != null)
            {
                // ease 5% (or Time.deltaTime can be used too) of the way to lookTarget.position:
                transform.position = Vector3.Lerp(transform.position, lookTarget.position, Time.deltaTime * easeMultiplier);
            }

            if (cam != null)
            {

                cam.transform.localPosition = Vector3.Lerp(cam.transform.localPosition, new Vector3(0, 0, -zoomValue), Time.deltaTime * easeMultiplier);
            }

        }
    }
}
