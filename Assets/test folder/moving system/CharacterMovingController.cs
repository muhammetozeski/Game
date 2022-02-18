using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MainGame.MainTools;

namespace MainGame.Player
{
    public class CharacterMovingController : MonoBehaviour
    {
        [SerializeField] Animator animator;

        [Header("Debug Purpose:")]
        [SerializeField] bool _characterShouldTurnRight;
        [SerializeField] float _characterShouldTurnToThisDegree;
        [SerializeField] float _differenceBetweenAngles;

        Transform _Camera;

        #region Animator Parameters
        int AnimatorParameterID_X;
        int AnimatorParameterID_Y;
        #endregion

        private void Awake()
        {
            this.animator = gameObject.GetComponent<Animator>();
            _Camera = Camera.main.transform;

            signAllAnimatorParameters();
        }
        void signAllAnimatorParameters()
        {
            AnimatorParameterID_X = Animator.StringToHash("X");
            AnimatorParameterID_Y = Animator.StringToHash("Y");
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
            else
                animator.SetFloat(AnimatorParameterID_Y, vertical);
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
                animator.SetFloat(AnimatorParameterID_X, 1);
            // transform.Rotate(Vector3.up);
            else
                animator.SetFloat(AnimatorParameterID_X, -1);
              //  transform.Rotate(-Vector3.up);
        }
    }
}
