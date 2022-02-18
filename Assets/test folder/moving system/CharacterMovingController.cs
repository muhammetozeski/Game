using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MainGame.MainTools;

namespace MainGame.Player
{
    public class CharacterMovingController : MonoBehaviour
    {
        [Header("Debug Purpose:")]
        [SerializeField] bool _characterShouldTurnRight;
        [SerializeField] float _characterShouldTurnToThisDegree;
        [SerializeField] float _differenceBetweenAngles;

        Transform _Camera;
        private void Awake()
        {
            _Camera = Camera.main.transform;
        }
        public void MoveManager(float horizontal, float vertical)
        {
            float characterShouldTurnToThisDegree = desiredCameraAngle(_Camera, horizontal, vertical);
            characterShouldTurnToThisDegree = Mathf.Round(characterShouldTurnToThisDegree);
            _characterShouldTurnToThisDegree = characterShouldTurnToThisDegree;
            float differenceBetweenAngles =
                (characterShouldTurnToThisDegree - Mathf.Abs(transform.eulerAngles.y)) -
                (characterShouldTurnToThisDegree - Mathf.Abs(transform.eulerAngles.y)) % 1
                ;
            _differenceBetweenAngles = differenceBetweenAngles;
            bool characterShouldTurnRight = differenceBetweenAngles < 180 && differenceBetweenAngles > 0
                || differenceBetweenAngles < -180 && differenceBetweenAngles > -360;
            _characterShouldTurnRight = characterShouldTurnRight;
            if (!(Mathf.Abs(differenceBetweenAngles) == 0))
            {
                testAnimations(characterShouldTurnRight);
            }
        }

        float desiredCameraAngle(Transform _camera, float horizontal, float vertical)
        {
            float ac; //angle of camera
            ac = _camera.eulerAngles.y;
            ac = Mathf.Abs(ac);

            float desiredAngle = ac +
                a_Vector2toAngle(horizontal, vertical);
            return desiredAngle % 360;
        }

        void testAnimations(bool turnRight)
        {
            if (turnRight)
                transform.Rotate(Vector3.up);
            else
                transform.Rotate(-Vector3.up);
        }

        //test purpose:
        private void Update()
        {
            print(_Camera.eulerAngles);

        }
    }
}
