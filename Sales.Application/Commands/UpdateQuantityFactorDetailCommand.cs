using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.Commands
{
    public class UpdateQuantityFactorDetailCommand : IRequest<int>
    {
        public int FactorId { get; set; }
        public int FactorDetailId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
