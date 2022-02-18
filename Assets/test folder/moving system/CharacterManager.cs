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

        [Header("Manual Input: (Debug Purpose)")]
        [SerializeField] bool openDebugMod = false;
        [SerializeField] float Horizontal;
        [SerializeField] float Vertical;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (openDebugMod)
            {
                if (Horizontal != 0 || Vertical != 0)
                {
                    MovingControllerScript.MoveManager(Horizontal, Vertical);
                }
            }
            else
            {
                if (MovingInput.Horizontal != 0 || MovingInput.Vertical != 0)
                {
                    MovingControllerScript.MoveManager(MovingInput.Horizontal, MovingInput.Vertical);
                }
            }
        }
    }
}
