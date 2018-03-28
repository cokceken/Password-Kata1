using System;
using System.Reflection;
using Password.Domain.Contract.RepositoryContract;
using Password.Domain.UnitOfWork;

namespace Password.Repository.UnitOfWork
{
    public static class UnitOfWorkHelper
    {
        public static bool IsRepositoryMethod(MethodInfo methodInfo)
        {
            return IsRepositoryClass(methodInfo.DeclaringType);
        }

        public static bool IsRepositoryClass(Type declaringType)
        {
            return typeof(IRepository<,>).IsAssignableFrom(declaringType);
        }

        public static bool HasUnitOfWorkAttribute(MethodInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }
    }
}