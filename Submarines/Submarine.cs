using UnityEngine;

/**
 * The main submarine component.
 */
namespace Submarines
{
    [ProtoBuf.ProtoContract]
    public class Submarine : SubRoot, ISerializationCallbackReceiver, IProtoEventListener
    {
        public Transform SavedModulesRoot;

        public new virtual void Awake()
        {
            modulesRoot = gameObject.transform;
        }   

        public new virtual void Start()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void Update()
        {
        }

        public new virtual void OnPlayerEntered(Player player)
        {
        }

        public new virtual void OnPlayerExited(Player player)
        {
        }

        public virtual void EnterSubmarine()
        {
            Player.main.SetCurrentSub(this);
        }

        public virtual void LeaveSubmarine()
        {
            Player.main.SetCurrentSub(null);
        }

        public new virtual void OnProtoSerialize(ProtobufSerializer serializer)
        {
        }

        public new virtual void OnProtoDeserialize(ProtobufSerializer serializer)
        {
            
        }

        public virtual void OnBeforeSerialize()
        {
        }

        public virtual void OnAfterDeserialize()
        {
        }
    }
}
