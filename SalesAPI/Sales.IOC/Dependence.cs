using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.BLL.Services;
using Sales.BLL.Services.Contract;
using Sales.DAL.DBContext;
using Sales.DAL.Repository;
using Sales.DAL.Repository.Contract;
using Sales.Utility;
using Sales.Utility.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sales.IOC
{
    public static class Dependence
    {
        public static void InjectDependence(this IServiceCollection services, IConfiguration configuration) //IServiceCollection ต้องใส่ this นำหน้าไม่งั้นที่อื่นไม่สามารถเรียกใช้ได้
        {
            services.AddDbContext<DbsalesContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(Constants.AppSettings.ConnectionStringSqlServer));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ISaleRepository, SaleRepository>();

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISaleService, SaleService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<IMenuService, MenuService>();
        }
    }
}
