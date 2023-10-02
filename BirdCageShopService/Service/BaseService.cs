using AutoMapper;
using BirdCageShopInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdCageShopService.Service
{
    public class BaseService
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitOfWork _unitOfWork;
        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;   
            _mapper = mapper;
        }   
    }
}
