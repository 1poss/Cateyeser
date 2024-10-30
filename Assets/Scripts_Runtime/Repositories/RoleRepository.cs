using System;
using System.Collections.Generic;

namespace Cateyeser {

    public class RoleRepository {

        Dictionary<int, RoleEntity> all;

        RoleEntity[] tempAll;

        public RoleRepository() {
            all = new Dictionary<int, RoleEntity>();

            tempAll = new RoleEntity[100];
        }

        public void Add(RoleEntity role) {
            all.Add(role.id, role);
        }

        public bool TryGet(int id, out RoleEntity role) {
            return all.TryGetValue(id, out role);
        }

        public void Remove(int id) {
            all.Remove(id);
        }

        public void Foreach(Action<RoleEntity> action) {
            foreach (var role in all.Values) {
                action.Invoke(role);
            }
        }

        public int TakeAll(out RoleEntity[] roles) {
            if (all.Count > tempAll.Length) {
                tempAll = new RoleEntity[all.Count];
            }
            all.Values.CopyTo(tempAll, 0);
            roles = tempAll;
            return all.Count;
        }

    }
}