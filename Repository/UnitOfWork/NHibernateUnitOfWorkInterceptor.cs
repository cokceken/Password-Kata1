using System.Reflection;
using Castle.DynamicProxy;
using NHibernate;
using IInterceptor = Castle.DynamicProxy.IInterceptor;

namespace Password.Repository.UnitOfWork
{
    public class NHibernateUnitOfWorkInterceptor : IInterceptor
    {
        private readonly ISessionFactory _sessionFactory;

        public NHibernateUnitOfWorkInterceptor(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public void Intercept(IInvocation invocation)
        {
            if (NHibernateUnitOfWork.Current != null || !RequiresDbConnection(invocation.MethodInvocationTarget))
            {
                invocation.Proceed();
                return;
            }

            try
            {
                NHibernateUnitOfWork.Current = new NHibernateUnitOfWork(_sessionFactory);
                NHibernateUnitOfWork.Current.BeginTransaction();

                try
                {
                    invocation.Proceed();
                    NHibernateUnitOfWork.Current.Commit();
                }
                catch
                {
                    try
                    {
                        NHibernateUnitOfWork.Current.Rollback();
                    }
                    catch
                    {
                        // ignored
                    }

                    throw;
                }
            }
            finally
            {
                NHibernateUnitOfWork.Current = null;
            }
        }

        private static bool RequiresDbConnection(MethodInfo methodInfo)
        {
            return UnitOfWorkHelper.HasUnitOfWorkAttribute(methodInfo) ||
                   UnitOfWorkHelper.IsRepositoryMethod(methodInfo);
        }
    }
}