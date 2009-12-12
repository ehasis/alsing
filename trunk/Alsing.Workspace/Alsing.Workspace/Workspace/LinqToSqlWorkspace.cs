namespace Alsing.Workspace
{
    using System.Collections.Generic;
    using System.Data.Linq;
    using System.Linq;

    public class LinqToSqlWorkspace : IWorkspace
    {
        private readonly List<CommitAction> commitActions = new List<CommitAction>();

        private readonly DataContext context;

        public LinqToSqlWorkspace(DataContext context)
        {
            this.context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            this.context.GetTable<T>().InsertOnSubmit(entity);
        }

        public void AddCommitAction(CommitAction commitAction)
        {
            this.commitActions.Add(commitAction);
        }

        public void Commit()
        {
            this.context.SubmitChanges(ConflictMode.FailOnFirstConflict);

            foreach (CommitAction action in this.commitActions)
            {
                action();
            }
        }

        public void Dispose()
        {
        }

        public IQueryable<T> MakeQuery<T>() where T : class
        {
            return this.context.GetTable<T>();
        }

        public void Remove<T>(T entity) where T : class
        {
            this.context.GetTable<T>().DeleteOnSubmit(entity);
        }
    }
}