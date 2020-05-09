using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DBContext
{
    public class InitializerComposite<T> : IDatabaseInitializer<T> where T : DbContext
    {
        private readonly System.Collections.Generic.List<IDatabaseInitializer<T>> initializers;
        public InitializerComposite(params IDatabaseInitializer<T>[] databaseInitializers)
        {
            this.initializers = new System.Collections.Generic.List<IDatabaseInitializer<T>>();
            this.initializers.AddRange(databaseInitializers);
        }
        public void InitializeDatabase(T context)
        {
            foreach (IDatabaseInitializer<T> current in this.initializers)
            {
                current.InitializeDatabase(context);
            }
        }
        public void AddInitializer(IDatabaseInitializer<T> databaseInitializer)
        {
            this.initializers.Add(databaseInitializer);
        }
        public void RemoveInitializer(IDatabaseInitializer<T> databaseInitializer)
        {
            this.initializers.Remove(databaseInitializer);
        }
    }
}
