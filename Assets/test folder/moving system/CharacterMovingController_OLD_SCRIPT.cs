using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MainGame.MainTools;

namespace MainGame.Player
{
    public class CharacterMovingController_OLD_SCRIPT : MonoBehaviour
    {
        [SerializeField] Vector2 deneme;


        #region debug varaibles
        [SerializeField] float d_characterAxis;
        [SerializeField] float d_dirAxis;
        [SerializeField] Vector2 d_CamDirection;
        [SerializeField] Vector2 d_axis;
        #endregion
        [SerializeField] CharacterHasObject characterHasObject;
        private bool _IsCharacterHasObject = false;
        public bool IsCharacterHasObject
        {
            get
            {
                return _IsCharacterHasObject;
            }
            set
            {
                _IsCharacterHasObject = value;
                characterHasObject.enabled = value;
            }
        }

        Transform MainCamera;

        [SerializeField] Vector2 Direction;

        Rigidbody _rigidbody;

        float speed = 10f;
        private void Awake()
        {
            MainCamera = Camera.main.transform;

            _rigidbody = gameObject.GetComponent<Rigidbody>();
        }
        private void Update()
        {

            print(a_Vector2toAngle(deneme));
        }
        public void MoveManager (float Horizontal, float Vertical)
        {
            Direction = wantedDirectionByCamera(Horizontal, Vertical);
            Move(Direction, Horizontal, Vertical);
        }

        void Move(Vector2 dir, float Horizontal, float Vertical)
        {
            if (IsCharacterHasObject)
            {
                //animasyon tree'ye x ve y inputlarını gir
                _rigidbody.velocity = speed * (transform.forward - transform.position);
            }
            else
            {
                float CharacterFacingTo = isCharacterFacingTo(dir);
                if (CharacterFacingTo > 0)
                {
                    //animasyon tree'ye x ekseni için +1 girilir
                    testAnimation(1f);
                }
                else if (CharacterFacingTo < 0)
                {
                    //animasyon tree'ye x ekseni için -1 girilir
                    testAnimation(-1f);
                }
                else
                {
                    //animasyon tree'ye y ekseni için +1 girilir
                    _rigidbody.velocity = speed * (transform.forward - transform.position);
                }
            }
        }

        Vector2 wantedDirectionByCamera(float Horizontal, float Vertical) {

            Vector2 CamDirection = a_Vector3To2OnFlat(MainCamera.forward - MainCamera.position);
            d_CamDirection = CamDirection;
            Vector2 axis = new Vector2(Horizontal, Vertical);
            d_axis = axis;
            return (CamDirection + axis).normalized;
        }

        //bitmemiş fonksiyon:
        bool CharacterHoldsAnObject()
        {
            return IsCharacterHasObject;
        }

        float isCharacterFacingTo(Vector2 dir)
        {
            Vector2 characterPos = a_Vector3To2OnFlat (transform.position);
            float characterAxis = Vector2.Angle(characterPos, a_Vector3To2OnFlat(transform.forward));
            d_characterAxis = characterAxis;
            float dirAxis = a_Vector2toAngle(dir);
            d_dirAxis = dirAxis;

            return dirAxis - characterAxis;
        }

        void testAnimation(float x)
        {
            transform.eulerAngles += Vector3.up * x;
        }
    }
}