﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Notes
 */

namespace RC3.Unity
{
    /// <summary>
    /// 
    /// </summary>
    public class Zoom : MonoBehaviour
    {
        [SerializeField]
        private float _sensitivity = 1.0f;

        [SerializeField]
        private float _stiffness = 5.0f;
        

        private float _distance = 50.0f;
        private float _minDistance = 1.0f;
        [SerializeField]private float _maxDistance = 100.0f;
        

        /// <summary>
        /// 
        /// </summary>
        void Start()
        {
            _distance = -transform.localPosition.z;
        }


        /// <summary>
        /// 
        /// </summary>
        private void LateUpdate()
        {
            var wheel = Input.GetAxis("Mouse ScrollWheel");

          
            _distance -= wheel * _sensitivity * _distance;
            _distance = Mathf.Clamp(_distance, _minDistance, _maxDistance);

            var p = transform.localPosition;
            p.z = Mathf.Lerp(p.z, -_distance, Time.deltaTime * _stiffness);

            if (Mathf.Abs(wheel) > 0)
            {
                if (GetComponent<SaveCamera>())
                {
                    GetComponent<SaveCamera>().RestoreView = false;
                }
            }
            if (GetComponent<SaveCamera>())
            {
                if (!GetComponent<SaveCamera>().RestoreView)
                {
                    transform.localPosition = p;
                }
            }
            else
            {
                transform.localPosition = p;
            }

          
        }
    }
}
