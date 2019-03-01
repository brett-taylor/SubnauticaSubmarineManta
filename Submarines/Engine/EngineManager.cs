using Submarines.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Submarines.Engine
{
    /**
     * Handles the state of the engine.
     * By default this comes set up with a similar engine style to the cyclops
     * with it having the ability to be off, slow, normal, fast and silent running mode.
     * 
     * Each engine state can have its own movement data.
     */
    public class EngineManager : MonoBehaviour
    {
        public EngineState EngineState { get; private set; } = EngineState.OFF;
        public MovementData CurrentMovementData { get; private set; } = null;
        private Dictionary<EngineState, MovementData> engineMovementData = new Dictionary<EngineState, MovementData>()
        {
            { EngineState.OFF, MovementData.Zero() }
        };

        private bool isPoweringUp = false;
        private bool isPoweringDown = false;

        public void Start()
        {
            CurrentMovementData = engineMovementData[EngineState];
        }

        public void SetNewEngineState(EngineState engineState, bool sendMessages = true)
        {
            if (engineState == EngineState)
            {
                return;
            }

            EngineState oldState = EngineState;
            EngineState = engineState;
            CurrentMovementData = GetCorrectMovementDataForEnginestate(EngineState);
            if (sendMessages == true && engineState != EngineState.OFF && oldState != EngineState.OFF)
            {
                SendMessage("OnEngineChangeState", oldState, SendMessageOptions.DontRequireReceiver);
            }
        }

        public IEnumerator PowerUp(EngineState engineStateToGoTo, float time)
        {
            if (isPoweringUp)
            {
                yield break;
            }

            isPoweringUp = true;
            SendMessage("OnEngineStartUp", engineStateToGoTo, SendMessageOptions.DontRequireReceiver);
            yield return new WaitForSeconds(time);
            SetNewEngineState(engineStateToGoTo, false);
            isPoweringUp = false;
        }

        public IEnumerator PowerDown(float time)
        {
            if (isPoweringDown)
            {
                yield break;
            }

            isPoweringDown = true;
            SendMessage("OnEnginePowerDown", null, SendMessageOptions.DontRequireReceiver);
            yield return new WaitForSeconds(time);
            SetNewEngineState(EngineState.OFF, false);
            isPoweringDown = false;
        }

        public bool IsPoweredUp()
        {
            return EngineState != EngineState.OFF;
        }

        public void SetMovementDataForEngineState(EngineState engineState, MovementData movementData)
        {
            if (engineMovementData.ContainsKey(engineState))
            {
                engineMovementData.Remove(engineState);
            }
            engineMovementData.Add(engineState, movementData);
        }

        public MovementData GetCorrectMovementDataForEnginestate(EngineState engineState)
        {
            return engineMovementData[engineState];
        }
    }
}
