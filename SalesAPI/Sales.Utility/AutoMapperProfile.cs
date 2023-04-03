using AutoMapper;
using Sales.DTO;
using Sales.Model;
using Sales.Utility.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Role, RoleDTO>().ReverseMap();

            CreateMap<Menu, MenuDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();

            #region Report

            CreateMap<SaleDetail, ReportDTO>()
                .ForMember(x =>
                  x.RecordDate,
                 opt => opt.MapFrom(origin => origin.IdSalesNavigation.RecordDate.Value.ToString(Constants.DateTimeFormat.ddMMyyyy))
                  )
                .ForMember(x =>
                  x.DocumentNumber,
                 opt => opt.MapFrom(origin => origin.IdSalesNavigation.DocumentNumber)
                  )
                .ForMember(x =>
                  x.PaymentMethod,
                 opt => opt.MapFrom(origin => origin.IdSalesNavigation.PaymentType)
                  )
                .ForMember(x =>
                  x.Product,
                 opt => opt.MapFrom(origin => origin.IdProductNavigation.Name)
                  )
                .ForMember(x =>
                x.SalesTotal,
                 opt => opt.MapFrom(origin => Convert.ToString(origin.IdSalesNavigation.Total.Value, new CultureInfo(Constants.CultureInfoFormat.en_US)))
                )
                .ForMember(x =>
                x.Price,
                 opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo(Constants.CultureInfoFormat.en_US)))
                )
                .ForMember(x =>
                x.Total,
                 opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo(Constants.CultureInfoFormat.en_US)))
                );

            #endregion

            #region SalesDetail

            CreateMap<SaleDetail, SaleDetailDTO>()
                .ForMember(x =>
                x.ProductName,
               opt => opt.MapFrom(origin => origin.IdProductNavigation.Name)
               )
               .ForMember(x =>
                x.PriceText,
                  opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo(Constants.CultureInfoFormat.en_US)))
                )
            .ForMember(x =>
                x.TotalText,
                  opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo(Constants.CultureInfoFormat.en_US)))
                );

            CreateMap<SaleDetailDTO, SaleDetail>()
                 .ForMember(x =>
                x.Price,
                  opt => opt.MapFrom(origin => Convert.ToDecimal(origin.PriceText, new CultureInfo(Constants.CultureInfoFormat.en_US)))
                )
                .ForMember(x =>
                x.Total,
                  opt => opt.MapFrom(origin => Convert.ToDecimal(origin.TotalText, new CultureInfo(Constants.CultureInfoFormat.en_US)))
                );

            #endregion

            #region Sales

            CreateMap<Sale, SaleDTO>()
                .ForMember(x =>
                x.TotalText,
                 opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo(Constants.CultureInfoFormat.en_US)))
                )
                 .ForMember(x =>
                x.RecordDate,
                 opt => opt.MapFrom(origin => origin.RecordDate.Value.ToString("dd/MM/yyyy"))
                );

            CreateMap<SaleDTO, Sale>()
                .ForMember(x =>
                x.Total,
                  opt => opt.MapFrom(origin => Convert.ToDecimal(origin.TotalText, new CultureInfo(Constants.CultureInfoFormat.en_US)))
                );

            #endregion

            #region Product

            CreateMap<Product, ProductDTO>()
             .ForMember(x =>
               x.CategoryName,
               opt => opt.MapFrom(origin => origin.IdCategoryNavigation.Name)
               )
             .ForMember(x =>
               x.Price,
               opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo(Constants.CultureInfoFormat.en_US)))
               )
             .ForMember(x =>
               x.IsActive,
               opt => opt.MapFrom(origin => origin.IsActive == true ? 1 : 0)
               );


            CreateMap<ProductDTO, Product>()
             .ForMember(x =>
               x.IdCategoryNavigation,
               opt => opt.Ignore()
               )
             .ForMember(x =>
               x.Price,
                opt => opt.MapFrom(origin => Convert.ToDecimal(origin.Price, new CultureInfo(Constants.CultureInfoFormat.en_US)))
               )
             .ForMember(x =>
               x.IsActive,
                  opt => opt.MapFrom(origin => origin.IsActive == 1 ? true : false)
               );
            #endregion

            #region User
            // ขาออก
            CreateMap<User, UserDTO>()
               .ForMember(x =>
               x.RoleName,
               opt => opt.MapFrom(origin => origin.IdRoleNavigation.Name)
               )
               .ForMember(x =>
               x.IsActive,
               opt => opt.MapFrom(origin => origin.IsActive == true ? 1 : 0)
               );

            CreateMap<User, SessionDTO>()
                .ForMember(x =>
                x.RoleName,
                opt => opt.MapFrom(origin => origin.IdRoleNavigation.Name)
                );
            //ขาเข้า
            CreateMap<UserDTO, User>()
                .ForMember(x =>
                x.IdRoleNavigation,
                opt => opt.Ignore()
                )
               .ForMember(x =>
               x.IsActive,
               opt => opt.MapFrom(origin => origin.IsActive == 1 ? true : false)
               );

            #endregion

        }
    }
}
