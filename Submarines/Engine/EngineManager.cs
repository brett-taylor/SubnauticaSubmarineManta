using Submarines.Movement;
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
        
        public void Start()
        {
            CurrentMovementData = engineMovementData[EngineState];
        }

        public void SetNewEngineState(EngineState engineState, bool skipEngineStartUpSequence, bool sendMessages)
        {
            if (engineState == EngineState)
            {
                SetNewEngineState(engineState, true, false);
                return;
            }

            EngineState oldState = EngineState;
            if (oldState == EngineState.OFF && skipEngineStartUpSequence == false) // Do startup
            {
                if (sendMessages == true)
                {
                    SendMessage("OnEngineStartUp", engineState, SendMessageOptions.DontRequireReceiver);
                }
            }
            else
            {
                if (engineState == EngineState.OFF)
                {
                    if (sendMessages == true)
                    {
                        SendMessage("OnEnginePowerDown", oldState, SendMessageOptions.DontRequireReceiver);
                    }
                }

                EngineState = engineState;
                CurrentMovementData = GetCorrectMovementDataForEnginestate(EngineState);
                if (sendMessages == true && engineState != EngineState.OFF && oldState != EngineState.OFF)
                {
                    SendMessage("OnEngineChangeState", oldState, SendMessageOptions.DontRequireReceiver);
                }
            }
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
