using Harmony;
using ProtoBuf;
using System;

namespace Submarines.Patchers
{
    [HarmonyPatch(typeof(ProtobufSerializerPrecompiled))]
    [HarmonyPatch("GetKeyImpl")]
    public class ProtobufSerializerPrecompiledGetKeyImplPatcher
    {
        public static bool Prefix(Type key, int __result)
        {
            if (key == typeof(Submarine))
            {
                __result = 2364377;
                return false;
            }

            if (key == typeof(PointsOfInterest.EntranceHatch))
            {
                __result = 2364379;
                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(ProtobufSerializerPrecompiled))]
    [HarmonyPatch("Serialize")]
    [HarmonyPatch(new Type[] { typeof(int), typeof(object), typeof(ProtoWriter) })]
    public class ProtobufSerializerPrecompiledSerializePatcher
    {
        public static bool Prefix(ProtobufSerializerPrecompiled __instance, int num, object obj, ProtoWriter writer)
        {
            if (obj is Submarine)
            {
                Serialization.Submarine.Serialize(obj as Submarines.Submarine, writer);
                return false;
            }

            if (obj is PointsOfInterest.EntranceHatch)
            {
                Serialization.EntranceHatch.Serialize(obj as PointsOfInterest.EntranceHatch, writer);
                return false;
            }

            return true;
        }
    }

    [HarmonyPatch(typeof(ProtobufSerializerPrecompiled))]
    [HarmonyPatch("Deserialize")]
    [HarmonyPatch(new Type[] { typeof(int), typeof(object), typeof(ProtoReader) })]
    public class ProtobufSerializerPrecompiledDeserializePatcher
    {
        public static bool Prefix(ProtobufSerializerPrecompiled __instance, object __result, int num, object obj, ProtoReader reader)
        {
            if (obj is Submarine)
            {
                Serialization.Submarine.Deserialize(obj as Submarine, reader);
                __result = obj;
                return false;
            }

            if (obj is PointsOfInterest.EntranceHatch)
            {
                Serialization.EntranceHatch.Deserialize(obj as PointsOfInterest.EntranceHatch, reader);
                __result = obj;
                return false;
            }

            return true;
        }
    }
}
