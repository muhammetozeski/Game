using System.Collections;
using System.Collections.Generic;
using MainGame.PlayerInput;
using UnityEngine;
namespace MainGame.GameCamera
{
    public class CameraController : MonoBehaviour
    {
        
        [SerializeField] private CameraSettings _cameraSettings;
        [SerializeField] private AbstractInputData _input;
        [SerializeField] private Transform _playerBody;
        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _cameraCenter;
        void Start()
        {

            Cursor.lockState = CursorLockMode.Locked;
        }
        void Update()
        {
            float mouseX = _input.Horizontal * _cameraSettings.MouseSpeed * Time.deltaTime;
            float mouseY = _input.Vertical * _cameraSettings.MouseSpeed * Time.deltaTime;
            
            _playerBody.Rotate(Vector3.up * mouseX);
           _cameraCenter.Rotate(Vector3.left * mouseY);
        }
    }
}
