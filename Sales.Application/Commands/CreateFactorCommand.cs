using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sales.Application.Commands
{
    public class CreateFactorCommand : IRequest<int>
    {
        public int[] ProductIds { get; set; }


        //public CreateFactorCommand()
        //{
        //}
    }
}
