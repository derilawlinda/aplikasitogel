using AplikasiTog.DAL.Models;
using GenericCodes.Core.DbContext;
using GenericCodes.Core.Repositories;
using GenericCodes.Core.UnitOfWork;
using GenericCodes.CRUD.WPF.UIServices;
using System.Data.Entity;
using AplikasiTog.DAL;
using AplikasiTog.Services.Interfaces;
using AplikasiTog.ViewModels.Users;
using AplikasiTog.ViewModels.Transactions;
using AplikasiTog.Services;
using Microsoft.Practices.ServiceLocation;
using Unity;
using System;
using System.Collections.Generic;
using Unity.Lifetime;

namespace AplikasiTog.ViewModels
{
    public class ViewModelLocator
    {
        static ViewModelLocator()
        {

            UnityContainer container = new UnityContainer();

            #region register DataContext & UnitOfWork
            Database.SetInitializer<TogelContext>
                                    (new System.Data.Entity.NullDatabaseInitializer<TogelContext>());

            container.RegisterType<ApplicationDbContext, TogelContext>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            #endregion

            #region Register Repositories
            container.RegisterType<IRepository<User>, Repository<User>>();
            container.RegisterType<IRepository<Transaction>, Repository<Transaction>>();

            #endregion

            #region Register App Services
            container.RegisterType<IDialogService, DialogService>();
            #endregion

            #region Register Business Services
            container.RegisterType<IUsersInterface, UsersService>();
            container.RegisterType<ITransactionsInterface, TransactionsService>();
            //container.RegisterType<IService<Category>, Service<Category>>();
            #endregion

            #region Register ViewModels

            container.RegisterType<MainViewModel>();

            container.RegisterType<UserViewModel>();
            container.RegisterType<UserSearchViewModel>();
            container.RegisterType<UserAddEditViewModel>();

            container.RegisterType<TransactionViewModel>();
            container.RegisterType<TransactionSearchViewModel>();
            container.RegisterType<TransactionAddEditViewModel>();
            container.RegisterType<TransactionUserAutocompleteViewModel>();



            #endregion

            UnityServiceLocator locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
        public UserViewModel Users
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UserViewModel>();
            }
        }


        public TransactionViewModel Transactions
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TransactionViewModel>();
            }
        }

      
        public TransactionUserAutocompleteViewModel TransactionUserAutocompletes
        {
            get
            {
                return ServiceLocator.Current.GetInstance<TransactionUserAutocompleteViewModel>();
            }
        }

        public class UnityServiceLocator : ServiceLocatorImplBase, IDisposable
        {
            private IUnityContainer container;

            /// <summary>
            /// Initializes a new instance of the <see cref="UnityServiceLocator"/> class for a container.
            /// </summary>
            /// <param name="container">The <see cref="IUnityContainer"/> to wrap with the <see cref="IServiceLocator"/>
            /// interface implementation.</param>
            public UnityServiceLocator(IUnityContainer container)
            {
                this.container = container;
                container.RegisterInstance<IServiceLocator>(this, new ExternallyControlledLifetimeManager());
            }

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            /// <filterpriority>2</filterpriority>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1816:CallGCSuppressFinalizeCorrectly",
                Justification = "Object is not finalizable, no reason to call SuppressFinalize")]
            public void Dispose()
            {
                if (container != null)
                {
                    container.Dispose();
                    container = null;
                }
            }

            /// <summary>
            /// When implemented by inheriting classes, this method will do the actual work of resolving
            ///             the requested service instance.
            /// </summary>
            /// <param name="serviceType">Type of instance requested.</param><param name="key">Name of registered service you want. May be null.</param>
            /// <returns>
            /// The requested service instance.
            /// </returns>
            protected override object DoGetInstance(Type serviceType, string key)
            {
                if (container == null) throw new ObjectDisposedException("container");
                return container.Resolve(serviceType, key);
            }

            /// <summary>
            /// When implemented by inheriting classes, this method will do the actual work of
            ///             resolving all the requested service instances.
            /// </summary>
            /// <param name="serviceType">Type of service requested.</param>
            /// <returns>
            /// Sequence of service instance objects.
            /// </returns>
            protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
            {
                if (container == null) throw new ObjectDisposedException("container");
                return container.ResolveAll(serviceType);
            }
        }
    }
}
