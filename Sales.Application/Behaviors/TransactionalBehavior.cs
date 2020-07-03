
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Sales.Application.Behaviors
{
    public class TransactionalBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public TransactionalBehavior()
        {
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = default(TResponse);

            if (!request
                .GetType()
                .CustomAttributes
               .Any(a => a.AttributeType == typeof(TransactionalAttribute)))
            {
                response = await next();
                return response;
            } 
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    response = await next();
                    transaction.Complete();

                }
                catch
                {

                }
                finally
                {
                    transaction.Dispose();
                }
            }
            return response;
        }
    }
}
