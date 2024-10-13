using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Store.Project.Core.Dtos.Baskets;
using Store.Project.Core.Entities;


namespace Store.Project.Core.Mapping.Basket
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
        }

        
    }
}
