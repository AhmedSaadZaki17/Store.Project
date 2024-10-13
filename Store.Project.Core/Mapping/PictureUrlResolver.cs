using AutoMapper;
using AutoMapper.Execution;
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
    public class PictureUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public PictureUrlResolver(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(! string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configuration["BASEURL"]}{source.PictureUrl}";
            }
            return string.Empty ;
        }
    }
}
