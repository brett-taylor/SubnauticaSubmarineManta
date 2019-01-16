using ProtoBuf;
using UnityEngine;

namespace Submarines.Serialization
{
    /**
     * Serialization for the Submarine.
     */
    public class Submarine
    {
        public static void Serialize(Submarines.Submarine submarine, ProtoWriter writer)
        {
        }

        public static void Deserialize(Submarines.Submarine submarine, ProtoReader reader)
        {
            for (int i = reader.ReadFieldHeader(); i > 0; i = reader.ReadFieldHeader())
            {
            }
        }
    }
}
