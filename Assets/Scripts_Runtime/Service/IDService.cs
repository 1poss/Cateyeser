using System;

namespace NJM {

    public class IDService {

        int roleIDRecord;
        int bulletIDRecord;
        int skillIDRecord;

        public IDService() {
            roleIDRecord = 0;
        }

        public int PickRoleID() {
            return ++roleIDRecord;
        }

        public int PickBulletID() {
            return ++bulletIDRecord;
        }

        public int PickSkillID() {
            return ++skillIDRecord;
        }

    }

}