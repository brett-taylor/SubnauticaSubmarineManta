using Harmony;
using ProtoBuf;
using System;

namespace SubnauticaProtobufSaveLoadSystem.Patchers
{
    [HarmonyPatch(typeof(ProtobufSerializerPrecompiled))]
    [HarmonyPatch("GetKeyImpl")]
    public class ProtobufSerializerPrecompiledGetKeyImplPatcher
    {
        public static bool Prefix(Type key, int __result)
        {
            return true;
        }
    }

    [HarmonyPatch(typeof(ProtobufSerializerPrecompiled))]
    [HarmonyPatch("Serialize")]
    [HarmonyPatch(new Type[] { typeof(int), typeof(object), typeof(ProtoWriter) })]
    public class ProtobufSerializerPrecompiledSerializePatcher
    {
        public static bool Prefix(int num, object obj, ProtoWriter writer)
        {
            return true;
        }
    }

    [HarmonyPatch(typeof(ProtobufSerializerPrecompiled))]
    [HarmonyPatch("Deserialize")]
    [HarmonyPatch(new Type[] { typeof(int), typeof(object), typeof(ProtoReader) })]
    public class ProtobufSerializerPrecompiledDeserializePatcher
    {
        public static bool Prefix(object __result, int num, object obj, ProtoReader reader)
        {
            return true;
        }
    }
}

/*
public static bool Prefix(object __result, int num, object obj, ProtoReader reader)
{
    if (obj is Movement.MovementController)
    {
        Utilities.Log.Print("MovementController Deserialize called");
        Movement.MovementController movement = obj as Movement.MovementController;
        for (int i = reader.ReadFieldHeader(); i > 0; i = reader.ReadFieldHeader())
        {
            if (i == 1)
            {
                //movement.TestValue = reader.ReadInt32();
            }
        }
        __result = obj;
        return false;
    }

    return true;
}

public static bool Prefix(int num, object obj, ProtoWriter writer)
{
    if (obj is Movement.MovementController)
    {
        Utilities.Log.Print("Object: " + obj.ToString());
        Movement.MovementController movement = obj as Movement.MovementController;
        ProtoWriter.WriteFieldHeader(1, WireType.Variant, writer);
        //ProtoWriter.WriteInt32(movement.TestValue, writer);
        return false;
    }

    return true;
}

public static bool Prefix(Type key, int __result)
{
    if (key == typeof(Movement.MovementController))
    {
        Utilities.Log.Print("Tried to load a MovementController");
        __result = 2364376;
        return false;
    }

    return true;
}*/