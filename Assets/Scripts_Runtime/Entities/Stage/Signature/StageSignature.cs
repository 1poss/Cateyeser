using System;
using System.Runtime.InteropServices;

namespace NJM {

    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct StageSignature {

        [UnityEngine.HideInInspector]
        [FieldOffset(0)]
        public uint value;

        [FieldOffset(0)]
        public ushort chapter;

        [FieldOffset(2)]
        public sbyte stage;

        [FieldOffset(3)]
        public sbyte sub_stage;

        public StageSignature(uint value) {
            chapter = 0;
            stage = 0;
            sub_stage = 0;
            this.value = value;
        }

        public StageSignature(ushort chapter, sbyte stage, sbyte sub_stage) {
            value = 0;
            this.chapter = chapter;
            this.stage = stage;
            this.sub_stage = sub_stage;
        }

        public static bool operator ==(StageSignature a, StageSignature b) {
            return a.value == b.value;
        }

        public static bool operator !=(StageSignature a, StageSignature b) {
            return a.value != b.value;
        }

        public override bool Equals(object obj) {
            return obj is StageSignature signature && value == signature.value;
        }

        public override int GetHashCode() {
            return (int)value;
        }

    }

}