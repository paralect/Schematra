using System;

namespace Paralect.Schematra.Exceptions
{
    public class SchematraException : Exception
    {
        public SchematraException(String message, params object[] args) : base(String.Format(message, args)) {}
        public SchematraException(String message, Exception innerException, params object[] args) : base(String.Format(message, args), innerException) {}
    }

    public class TypeNotFoundException : SchematraException
    {
        public TypeNotFoundException(string message, params object[] args) : base(message, args) {}
        public TypeNotFoundException(string message, Exception innerException, params object[] args) : base(message, innerException, args) { }
    }

    public class TypeMismatchException : SchematraException
    {
        public TypeMismatchException(string message, params object[] args) : base(message, args) {}
        public TypeMismatchException(string message, Exception innerException, params object[] args) : base(message, innerException, args) { }
    }

    public class TypeNotTaggedException : SchematraException
    {
        public TypeNotTaggedException(string message, params object[] args) : base(message, args) {}
        public TypeNotTaggedException(string message, Exception innerException, params object[] args) : base(message, innerException, args) {}
    }

    public class CircularDependencyException : SchematraException
    {
        public CircularDependencyException(string message, params object[] args) : base(message, args) {}
        public CircularDependencyException(string message, Exception innerException, params object[] args) : base(message, innerException, args) { }
    }

    public class DuplicateFieldIndexException : SchematraException
    {
        public DuplicateFieldIndexException(string message, params object[] args) : base(message, args) { }
        public DuplicateFieldIndexException(string message, Exception innerException, params object[] args) : base(message, innerException, args) {}
    }

    public class DuplicateFieldNameException : SchematraException
    {
        public DuplicateFieldNameException(string message, params object[] args) : base(message, args) { }
        public DuplicateFieldNameException(string message, Exception innerException, params object[] args) : base(message, innerException, args) {}
    }

    public class DuplicateConstantIndexException : SchematraException
    {
        public DuplicateConstantIndexException(string message, params object[] args) : base(message, args) { }
        public DuplicateConstantIndexException(string message, Exception innerException, params object[] args) : base(message, innerException, args) { }
    }

    public class DuplicateConstantNameException : SchematraException
    {
        public DuplicateConstantNameException(string message, params object[] args) : base(message, args) { }
        public DuplicateConstantNameException(string message, Exception innerException, params object[] args) : base(message, innerException, args) { }
    }

    public class DuplicateTypeTagException : SchematraException
    {
        public DuplicateTypeTagException(string message, params object[] args) : base(message, args) { }
        public DuplicateTypeTagException(string message, Exception innerException, params object[] args) : base(message, innerException, args) { }
    }

    public class DuplicateTypeNameException : SchematraException
    {
        public DuplicateTypeNameException(string message, params object[] args) : base(message, args) { }
        public DuplicateTypeNameException(string message, Exception innerException, params object[] args) : base(message, innerException, args) { }
    }

    public class DuplicateTypeAliasException : SchematraException
    {
        public DuplicateTypeAliasException(string message, params object[] args) : base(message, args) { }
        public DuplicateTypeAliasException(string message, Exception innerException, params object[] args) : base(message, innerException, args) { }
    }
}