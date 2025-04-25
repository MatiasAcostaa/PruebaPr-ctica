using FluentValidation.Results;

namespace PruebaPráctica.Application.Common.Exceptions;

public sealed class ValidationException(IEnumerable<ValidationFailure> errors) : Exception
{
    public IEnumerable<ValidationFailure> Errors { get; } = errors;
}