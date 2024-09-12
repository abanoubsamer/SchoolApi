using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolWep.Core.SharedResources;


namespace SchoolWep.Core.Behavior
{
    public class ValidationBehiveor<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest: IRequest<TResponse>
    {

        private readonly IEnumerable<IValidator<TRequest>> _Validator;
        private readonly IStringLocalizer<SharedResource> _StringLocalizer;

        public ValidationBehiveor(IEnumerable<IValidator<TRequest>> Validator,
            IStringLocalizer<SharedResource> stringLocalizer)
        {
            _Validator=Validator;
            _StringLocalizer=stringLocalizer;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_Validator.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_Validator.Select(v => v.ValidateAsync(context, cancellationToken)));
                var Failuers=validationResults.SelectMany(r=>r.Errors).Where(f=>f !=null).ToList();
                if (Failuers.Count != 0) {
                    var msg = _StringLocalizer[SharedResourcesKey.Valdiation.ValidationError];
                    throw new ValidationException(msg, Failuers);
                 
                }
              
            }
            return await next();
        }
    }
}
