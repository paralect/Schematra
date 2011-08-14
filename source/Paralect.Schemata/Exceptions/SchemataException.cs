using System;

namespace Paralect.Schemata.Exceptions
{
    public class SchemataException : Exception
    {
        public SchemataException(String message, params object[] args) : base(String.Format(message, args)) {}
        public SchemataException(String message, Exception innerException, params object[] args) : base(String.Format(message, args), innerException) {}
    }

    public class TypeNotFoundException : SchemataException
    {
        public TypeNotFoundException(string message, params object[] args) : base(message, args) {}
        public TypeNotFoundException(string message, Exception innerException, params object[] args) : base(message, innerException, args) { }
    }

    public class TypeMismatchException : SchemataException
    {
        public TypeMismatchException(string message, params object[] args) : base(message, args) {}
        public TypeMismatchException(string message, Exception innerException, params object[] args) : base(message, innerException, args) { }
    }

    public class TypeNotTaggedException : SchemataException
    {
        public TypeNotTaggedException(string message, params object[] args) : base(message, args) {}
        public TypeNotTaggedException(string message, Exception innerException, params object[] args) : base(message, innerException, args) {}
    }

    public class CircularDependencyException : SchemataException
    {
        public CircularDependencyException(string message, params object[] args) : base(message, args) {}
        public CircularDependencyException(string message, Exception innerException, params object[] args) : base(message, innerException, args) { }
    }

    public class DuplicateFieldIndexException : SchemataException
    {
        public DuplicateFieldIndexException(string message, params object[] args) : base(message, args) { }
        public DuplicateFieldIndexException(string message, Exception innerException, params object[] args) : base(message, innerException, args) {}
    }

    public class DuplicateFieldNameException : SchemataException
    {
        public DuplicateFieldNameException(string message, params object[] args) : base(message, args) { }
        public DuplicateFieldNameException(string message, Exception innerException, params object[] args) : base(message, innerException, args) {}
    }

    public class DuplicateConstantIndexException : SchemataException
    {
        public DuplicateConstantIndexException(string message, params object[] args) : base(message, args) { }
        public DuplicateConstantIndexException(string message, Exception innerException, params object[] args) : base(message, innerException, args) { }
    }

    public class DuplicateConstantNameException : SchemataException
    {
        public DuplicateConstantNameException(string message, params object[] args) : base(message, args) { }
        public DuplicateConstantNameException(string message, Exception innerException, params object[] args) : base(message, innerException, args) { }
    }

    public class DuplicateTypeTagException : SchemataException
    {
        public DuplicateTypeTagException(string message, params object[] args) : base(message, args) { }
        public DuplicateTypeTagException(string message, Exception innerException, params object[] args) : base(message, innerException, args) { }
    }

    public class DuplicateTypeNameException : SchemataException
    {
        public DuplicateTypeNameException(string message, params object[] args) : base(message, args) { }
        public DuplicateTypeNameException(string message, Exception innerException, params object[] args) : base(message, innerException, args) { }
    }

    public class DuplicateTypeAliasException : SchemataException
    {
        public DuplicateTypeAliasException(string message, params object[] args) : base(message, args) { }
        public DuplicateTypeAliasException(string message, Exception innerException, params object[] args) : base(message, innerException, args) { }
    }
}