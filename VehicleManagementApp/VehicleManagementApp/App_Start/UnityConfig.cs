using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using VehicleManagementApp.BLL;
using VehicleManagementApp.BLL.Contracts;
using VehicleManagementApp.Controllers;
using VehicleManagementApp.Models;
using VehicleManagementApp.Repository;
using VehicleManagementApp.Repository.Contracts;
using VehicleManagementApp.Repository.DatabaseContext;

namespace VehicleManagementApp
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<IOrganaizationManager, OrganaizationManager>();
            container.RegisterType<IOrganaizationRepository, OrganaizationRepository>();

            container.RegisterType<IDesignationManager, DesignationManager>();
            container.RegisterType<IDesignationRepository, DesignationRepository>();

            container.RegisterType<IVehicleTypeManager, VehicleTypeManager>();
            container.RegisterType<IVehicleTypeRepository, VehicleTypeRepository>();

            container.RegisterType<IVehicleManager, VehicleManager>();
            container.RegisterType<IVehicleRepository, VehicleRepository>();

            container.RegisterType<IDepartmentManager, DepartmentManager>();
            container.RegisterType<IDepartmentRepository, DepartmentRepository>();


            container.RegisterType<IEmployeeManager, EmployeeManager>();
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();

            container.RegisterType<ICommentManager, CommentManager>();
            container.RegisterType<ICommentRepository, CommentRepository>();

            container.RegisterType<IRequsitionManager, RequsitionManager>();
            container.RegisterType<IRequsitionRepository, RequsistionRepository>();

            container.RegisterType<IDivisionManager, DivisionManager>();
            container.RegisterType<IDivisionRepository, DivisionRepository>();

            container.RegisterType<IDistrictManager, DistrictManager>();
            container.RegisterType<IDistrictRepository, DistrictRepository>();

            container.RegisterType<IThanaManager, ThanaManager>();
            container.RegisterType<IThanaRepository, ThanaRepository>();

            container.RegisterType<IManagerManager, ManagerManager>();
            container.RegisterType<IManagerRepository, ManagerRepository>();

            container.RegisterType<DbContext, VehicleDatabaseContext>();

            container.RegisterType<IDriverStatusManager, DriverStatusManager>();
            container.RegisterType<IDriverStatusRepository, DriverStatusRepository>();

            container.RegisterType<IVehicleStatusManager, VehicleStatusManager>();
            container.RegisterType<IVehicleStatusRepository, VehicleStatusRepository>();

            //container.RegisterType<DbContext, VehicleDatabaseContext>();
            //container.RegisterType<IdentityDbContext< ApplicationUser >,ApplicationDbContext >();

            // container.RegisterType<ApplicationUserManager, ApplicationUserManager>(new HierarchicalLifetimeManager());
            //container.RegisterType<IdentityDbContext, IdentityDbContext>(new HierarchicalLifetimeManager());
            //container.RegisterType<ApplicationUserManager>(new HierarchicalLifetimeManager());
            //container.RegisterType<IdentityDbContext>(new HierarchicalLifetimeManager());

            container.RegisterType<AccountController>();
            container.RegisterType<ManageController>(new InjectionConstructor());
            container.RegisterType<ApplicationUserManager>();
            container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            container.RegisterType<UserManager<ApplicationUser>>();
            container.RegisterType<DbContext, ApplicationDbContext>();
           

            //container.RegisterType<AccountController>(
            //    new InjectionConstructor(
            //     container.Resolve<IEmployeeManager>())
            //     );

           
            //container.RegisterInstance(new EmployeeRegisterViewModel());
        }
    }
}