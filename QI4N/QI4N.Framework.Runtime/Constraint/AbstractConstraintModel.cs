namespace QI4N.Framework.Runtime
{
    public abstract class AbstractConstraintModel
    {
        protected AbstractConstraintModel(ConstraintAttribute annotation)
        {
            this.Annotation = annotation;
        }

        public ConstraintAttribute Annotation { get; set; }

        public abstract ConstraintInstance NewInstance();

        //public void visitModel(ModelVisitor modelVisitor)
        //{
        //    modelVisitor.visit(this);
        //}
    }
}