using System;

namespace NJM {

    public class IDService {

        int roleIDRecord;
        int bulletIDRecord;

        public IDService() {
            roleIDRecord = 0;
        }

        public int PickRoleID() {
            return ++roleIDRecord;
        }

        public int PickBulletID() {
            return ++bulletIDRecord;
        }

    }

}