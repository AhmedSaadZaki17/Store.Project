﻿using AutoMapper;

using Microsoft.Extensions.Configuration;
using Store.Project.Core.Dtos.Products;
using Store.Project.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Project.Core.Mapping
{
    // 
    public class ProductProfile : Profile
    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.BrandName, options => options.MapFrom(s => s.Brand.Name))
                .ForMember(d => d.TypeName, options => options.MapFrom(s => s.Type.Name))
               // .ForMember(d => d.PictureUrl, options => options.MapFrom(s => $"{configuration["BASEURL"]}{s.PictureUrl}"))
               .ForMember(d => d.PictureUrl, options => options.MapFrom(new PictureUrlResolver(configuration)))
                ;



            CreateMap<ProductType, TypeBrandDto>();
            CreateMap<ProductBrand, TypeBrandDto>();
        }
    }
}
