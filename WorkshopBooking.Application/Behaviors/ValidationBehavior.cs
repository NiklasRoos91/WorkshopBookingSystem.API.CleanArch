using FluentValidation;
using MediatR;

namespace WorkshopBooking.Application.Behaviors
{
    // En generisk pipeline-behavior som körs för varje MediatR-request innan själva handlern körs
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> // Begränsar så att TRequest måste vara en MediatR-request
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators; // Håller alla validatorer för denna typ av request

        // Konstruktor som tar in alla validatorer som registrerats för TRequest
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators; // Sparar referensen till validatorerna
        }

        // Denna metod körs automatiskt av MediatR innan den faktiska handlern körs
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Om det inte finns några validatorer för denna request, gå direkt vidare till nästa steg i pipelinen
            if (!_validators.Any())
                return await next();

            // Skapar en valideringskontext som skickas till validatorerna
            var context = new ValidationContext<TRequest>(request);

            // Kör alla validatorer parallellt och samlar resultaten
            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            // Samlar ihop alla valideringsfel från alla validatorer
            var failures = validationResults
                .SelectMany(r => r.Errors) // Hämtar alla fel från varje valideringsresultat
                .Where(f => f != null) // Tar bara med icke-null fel
                .ToList();

            if (failures.Any())
            {
                // Extrahera bara felmeddelandena
                var failureMessages = failures.Select(f => f.ErrorMessage).ToList();

                // Försök att hitta en statisk metod 'Failure' på TResponse som tar en lista av strängar
                var failureMethod = typeof(TResponse).GetMethod("Failure", new[] { typeof(List<string>) });

                // Anropa metoden och returnera resultatet (kastar om det till rätt typ)
                return (TResponse)failureMethod.Invoke(null, new object[] { failureMessages });
            }

            // Om allt är OK, fortsätt till nästa steg i MediatR-pipelinen (alltså själva handlern)
            return await next();
        }
    }
}