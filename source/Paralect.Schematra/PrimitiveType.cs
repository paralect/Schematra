namespace Paralect.Schematra
{
    public class PrimitiveType : Type
    {
        public PrimitiveType(TypeContext typeContext) : base(typeContext)
        {

        }

        public override void Build()
        {
            // do nothing
        }

        protected PrimitiveType CreateInternal()
        {
            var primitiveType = new PrimitiveType(_typeContext);
            CopyInternal(primitiveType);
            return primitiveType;
        }
    }
}