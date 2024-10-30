using System;

namespace Cateyeser {

    public class IDService {

        int roleIDRecord;

        public IDService() {
            roleIDRecord = 0;
        }

        public int PickRoleID() {
            return ++roleIDRecord;
        }

    }

}