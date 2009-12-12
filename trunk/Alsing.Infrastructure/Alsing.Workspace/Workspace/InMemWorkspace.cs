namespace Alsing.Workspace
{
    using System.Collections.Generic;
    using System.Linq;

    public class InMemWorkspace : IWorkspace
    {
        private readonly List<object> addedEntities = new List<object>();

        private readonly List<CommitAction> commitActions = new List<CommitAction>();

        private readonly List<object> entities = new List<object>();

        private readonly List<object> removedEntities = new List<object>();

        public void Add<T>(T entity) where T : class
        {
            this.entities.Add(entity);
            this.addedEntities.Add(entity);
        }

        public void AddCommitAction(CommitAction commitAction)
        {
            this.commitActions.Add(commitAction);
        }

        public void ClearUoW()
        {
            this.addedEntities.Clear();
            this.removedEntities.Clear();
        }

        public void Commit()
        {
            foreach (CommitAction action in this.commitActions)
            {
                action();
            }
        }

        public void Dispose()
        {
        }

        public int GetAddedEntityCount<T>() where T : class
        {
            int insertedCount = this.MakeQueryForAddedEntities<T>().Count();
            return insertedCount;
        }

        public int GetCommitActionCount()
        {
            int commitActionCount = this.commitActions.Count;
            return commitActionCount;
        }

        public int GetRemovedEntityCount<T>() where T : class
        {
            int removedCount = this.MakeQueryForRemovedEntities<T>().Count();
            return removedCount;
        }

        public IQueryable<T> MakeQuery<T>() where T : class
        {
            return this.entities
                    .Where(entity => entity is T)
                    .Cast<T>()
                    .AsQueryable();
        }

        public IQueryable<T> MakeQueryForAddedEntities<T>() where T : class
        {
            return this.addedEntities
                    .Where(entity => entity is T)
                    .Cast<T>()
                    .AsQueryable();
        }

        public IQueryable<T> MakeQueryForRemovedEntities<T>() where T : class
        {
            return this.removedEntities
                    .Where(entity => entity is T)
                    .Cast<T>()
                    .AsQueryable();
        }

        public void Remove<T>(T entity) where T : class
        {
            this.entities.Remove(entity);
            this.removedEntities.Add(entity);
        }
    }
}