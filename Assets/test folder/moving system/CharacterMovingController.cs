using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MainGame.MainTools;

namespace MainGame.Player
{
    public class CharacterMovingController : MonoBehaviour
    {
        Transform _Camera;
        private void Awake()
        {
            _Camera = Camera.main.transform;
        }
        public void MoveManager(float horizontal, float vertical)
        {
            float characterShouldTurnToThisDegree = desiredCameraAngle(_Camera, horizontal, vertical);
            float differenceBetweenAngles =characterShouldTurnToThisDegree - Mathf.Abs (transform.eulerAngles.y%360);

            bool characterShouldTurnRight = differenceBetweenAngles < 180 && differenceBetweenAngles > 0;
            if (!(Mathf.Abs(differenceBetweenAngles) > 0 && Mathf.Abs(differenceBetweenAngles) < 1))
            {
                testAnimations(characterShouldTurnRight);
            }
        }

        float desiredCameraAngle(Transform _camera, float horizontal, float vertical)
        {
            float ac; //angle of camera
            ac = _camera.eulerAngles.y;
            ac = Mathf.Abs(ac);
            ac %=360;

            float desiredAngle = ac + 
                a_Vector2toAngle(horizontal, vertical);
            return desiredAngle%360;
        }

        void testAnimations(bool turnRight)
        {
            if (turnRight)
                transform.eulerAngles += Vector3.up;
            else
                transform.eulerAngles += -Vector3.up;
        }
    }
}
