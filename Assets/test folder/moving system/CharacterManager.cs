using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MainGame.PlayerInput;
namespace MainGame.Player
{
    public class CharacterManager : MonoBehaviour
    {
        [SerializeField] AbstractInputData MovingInput;
        [SerializeField] CharacterMovingController MovingControllerScript;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (MovingInput.Horizontal != 0 || MovingInput.Vertical != 0)
            {
                MovingControllerScript.MoveManager(MovingInput.Horizontal, MovingInput.Vertical);
            }
        }
    }
}
