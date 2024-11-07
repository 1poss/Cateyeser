using System;
using System.Runtime.InteropServices;

namespace NJM {

    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct IDSignature {

        [FieldOffset(0)]
        public ulong value;

        [FieldOffset(0)]
        public EntityType entityType;

        [FieldOffset(4)]
        public int id;

        public IDSignature(EntityType entityType, int id) {
            this.value = 0;
            this.entityType = entityType;
            this.id = id;
        }

        public static bool operator ==(IDSignature a, IDSignature b) {
            return a.value == b.value;
        }

        public static bool operator !=(IDSignature a, IDSignature b) {
            return a.value != b.value;
        }

        public override bool Equals(object obj) {
            return obj is IDSignature signature && value == signature.value;
        }

        public override int GetHashCode() {
            return value.GetHashCode();
        }

    }

}