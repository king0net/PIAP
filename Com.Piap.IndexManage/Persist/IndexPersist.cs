﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Com.Piap.IndexManage.Model;
using Com.Piap.IndexManage.IPersist;
using PetaPoco;

namespace Com.Piap.IndexManage.Persist {
    public class IndexPersist : IIndexPersist {
        private PetaPoco.Database db = new Database("database");
        private readonly string TableName = "Index";
        private readonly string PKName = "Code";

        public bool Create(Index index) {
            bool SUCESS = true;
            try {
                db.Insert(index);
            }
            catch {
                SUCESS = false;
            }
            return SUCESS;
        }

        public bool Modify(Index index) {
            bool SUCESS = true;
            try {
                db.Update(TableName, PKName, index);
            }
            catch {
                SUCESS = false;
            }
            return SUCESS;
        }

        public bool Remove(Index index) {
            bool SUCESS = true;
            try {
                index.Enable = false;
                index.Deleted = true;
                db.Update(TableName, PKName, index);
            }
            catch {
                SUCESS = false;
            }
            return SUCESS;
        }

        public bool Delete(Index index) {
            bool SUCESS = true;
            try {
                db.Delete<Index>(index);
            }
            catch {
                SUCESS = false;
            }
            return SUCESS;
        }

        public Index GetByCode(string code) {
            return db.Query<Index>("SELECT * FROM " + TableName + " WHERE " + PKName + "='" + code + "' ").SingleOrDefault<Index>();
        }

        public List<Index> GetAll() {
            return db.Query<Index>("SELECT * FROM " + TableName + " ").ToList<Index>();
        }
    }
}
