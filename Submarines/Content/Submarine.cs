﻿using UnityEngine;

namespace Submarines.Content
{
    /**
    * The main submarine component.
    */
    [ProtoBuf.ProtoContract]
    public class Submarine : SubRoot
    {
        public Transform SavedModulesRoot { get; set; }

        public new virtual void Awake()
        {
            modulesRoot = gameObject.transform;
        }   

        public new virtual void Start()
        {
        }

        public new virtual void FixedUpdate()
        {
        }

        public new virtual void Update()
        {
        }

        public new virtual void OnPlayerEntered(Player player)
        {
            BroadcastMessage("OnPlayerEnteredSubmarine", SendMessageOptions.DontRequireReceiver);
        }

        public new virtual void OnPlayerExited(Player player)
        {
            BroadcastMessage("OnPlayerExitedSubmarine", SendMessageOptions.DontRequireReceiver);
        }

        public virtual void EnterSubmarine()
        {
            Player.main.SetCurrentSub(this);
        }

        public virtual void LeaveSubmarine()
        {
            Player.main.SetCurrentSub(null);
        }

        public void OnSteeringStart()
        {
            BroadcastMessage("OnSteeringStarted", SendMessageOptions.DontRequireReceiver);
        }

        public void OnSteeringEnd()
        {
            BroadcastMessage("OnSteeringEnded", SendMessageOptions.DontRequireReceiver);
        }

        public override void OnTakeDamage(DamageInfo info)
        {

        }
    }
}
