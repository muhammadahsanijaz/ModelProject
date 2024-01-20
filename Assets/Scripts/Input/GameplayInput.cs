using UnityEngine;

namespace MoonKart
{
    interface IGameInput
    {
        public float MoveInput();
        public float SteerInput();
        public bool DriftInput();
        public bool PowerupInput(KeyCode keyCode);
        public void DebugInputLogs(InputKeys input);

    }

    public class InputKeys
    {
        public bool Accelerate;
        public bool Decelerate;
        public bool RightTurn;
        public bool LeftTurn;
        public bool Drift;

    }

    public class GameplayInput : GameInput, IGameInput
    {
        [SerializeField] private bool debugInputLogs;
        [SerializeField] private KeyCode[] PowerupKeyCodes;

        public InputKeys inputKeys;

        // GameService INTERFACE
        protected override void OnGameSet()
        {

        }

        protected override void OnGameCleared()
        {

        }

        protected override void OnTick()
        {
            base.OnTick();

            if (Context.LocalPlayer < 0)
                return;

        }

        // PRIVATE METHODS

        private void PollInput()
        {
            var input = new InputKeys();
            PollMovementInput(ref input);
            PollPowerupInput(ref input);
            //process input

            if (debugInputLogs)
            {
                DebugInputLogs(input);
            }
        }

        /// <summary>
        /// Collect all movement input
        /// </summary>
        private void PollMovementInput(ref InputKeys input)
        {
            if (MoveInput() > 0)
            {
                input.Accelerate = true;
                input.Decelerate = false;
            }
            else if (MoveInput() < 0)
            {
                input.Accelerate = false;
                input.Decelerate = true;
            }
            else
            {
                input.Accelerate = false;
                input.Decelerate = false;
            }

            if (SteerInput() > 0)
            {
                input.RightTurn = true;
                input.LeftTurn = false;
            }
            else if (SteerInput() < 0)
            {
                input.RightTurn = false;
                input.LeftTurn = true;
            }
            else
            {
                input.RightTurn = false;
                input.LeftTurn = false;
            }


            input.Drift = DriftInput() ? true : false;


            // input.Steering = GetSteering();
            // input.Accelerate = y > 0.1f;
            // input.Decelerate = y < -0.1f;
            // input.HandBrake = UnityEngine.Input.GetButton("Brake");
            // input.Horn = UnityEngine.Input.GetButton("Horn");
            // input.Nitro = UnityEngine.Input.GetButton("Nitro");


        }

        /// <summary>
        /// Collect all PoweupSpawn input
        /// </summary>
        private void PollPowerupInput(ref InputKeys input)
        {

            //for (int i = 0; i < PowerupKeyCodes.Length; i++)
            //{
            //    if (PowerupInput(PowerupKeyCodes[i]))
            //    {
            //        input.SetPowerupInput(i, true);
            //    }
            //    else
            //    {
            //        input.SetPowerupInput(i, false);
            //    }
            //}
        }

        // GameInput Interface Methods

        #region  MovementInputFunctions

        public float MoveInput()
        {
            return UnityEngine.Input.GetAxis("Vertical");
        }
        public float SteerInput()
        {
            return UnityEngine.Input.GetAxis("Horizontal");
        }
        public bool DriftInput()
        {
            return UnityEngine.Input.GetButton("Jump");
        }


        #endregion


        #region PowerupInputFunctions

        public bool PowerupInput(KeyCode inputKeyCode)
        {
            return UnityEngine.Input.GetKey(inputKeyCode);
        }


        #endregion
        public void DebugInputLogs(InputKeys input)
        {
            Debug.Log("Input Control Accelerate = " + input.Accelerate + " Decelerate = " + input.Decelerate +
                      "\n LeftTurn = " + input.LeftTurn + " RightTurn = " + input.RightTurn + " Drifting = " + input.Drift);
        }

    }
}
