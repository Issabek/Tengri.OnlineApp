using System;
using System.Linq;
using System.Collections.Generic;
using LiteDB;

namespace Tengri.DAL
{
    public class LiteDbEntity
    {
        private string ConnectionDb { get; set; }

        public LiteDbEntity(string pathToDb)
        {
            if (string.IsNullOrEmpty(pathToDb))
                throw new Exception("Path is wrong");
            else
                ConnectionDb = pathToDb;
        }

        public List<T> getCollection<T>()
        {
            using (var db = new LiteDatabase(ConnectionDb))
            {
                if (db.GetCollection<T>(typeof(T).Name).FindAll().ToList() == null)
                    throw new Exception("Object is null");
                List<T> myObj = db.GetCollection<T>(typeof(T).Name).FindAll().ToList(); ;

                return myObj;

            }
        }

        public bool userCreate<T>(T data, out string message)
        {
            try
            {
                using (var db = new LiteDatabase(ConnectionDb))
                {
                    var collection = db.GetCollection<T>(typeof(T).Name);
                    collection.Insert(data);
                }
                message = "Success";
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

        public bool update<T>(T data, out string message)
        {
            try
            {
                using (var db = new LiteDatabase(ConnectionDb))
                {
                    var collection = db.GetCollection<T>(typeof(T).Name);
                    collection.Update(data);
                }
                message = "Success";
                return true;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }
        }

    }
}