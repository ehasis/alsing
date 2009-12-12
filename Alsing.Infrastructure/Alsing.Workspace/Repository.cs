namespace Alsing.Workspace
{
    using System.Linq;

    public abstract class Repository<T> where T : class
    {
        protected readonly IWorkspace workspace;

        protected Repository(IWorkspace workspace)
        {
            this.workspace = workspace;
        }

        protected IQueryable<T> MakeQuery()
        {
            return this.workspace.MakeQuery<T>();
        }
    }
}