using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Alsing.Workspace
{
    public class EF4Workspace : IWorkspace
    {
        private readonly List<CommitAction> commitActions = new List<CommitAction>();
        private ObjectContext context;
        private Func<Type,object> selector;

        public EF4Workspace(ObjectContext context, Func<Type, object> selector)
        {
            this.context = context;
            this.selector = selector;
        }

        #region IWorkspace Members

        public IQueryable<T> MakeQuery<T>() where T : class
        {
            return GetObjectSet<T>();
        }

        public void Add<T>(T entity) where T : class
        {
            GetObjectSet<T>().AddObject(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            GetObjectSet<T>().DeleteObject(entity);
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public void AddCommitAction(CommitAction commitAction)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion

        private ObjectSet<T> GetObjectSet<T>() where T : class
        {
            return (ObjectSet<T>)selector(typeof(T));
        }
    }
}
