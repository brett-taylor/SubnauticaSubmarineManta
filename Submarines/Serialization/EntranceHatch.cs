using ProtoBuf;
using UnityEngine;

namespace Submarines.Serialization
{
    /**
     * Serialization for the Submarine.
     */
    public class EntranceHatch
    {
        public static void Serialize(Content.EntranceHatch entranceHatch, ProtoWriter writer)
        {
        }

        public static void Deserialize(Content.EntranceHatch entranceHatch, ProtoReader reader)
        {
            for (int i = reader.ReadFieldHeader(); i > 0; i = reader.ReadFieldHeader())
            {
                if (i == 1)
                {

                }
            }
        }
    }
}
