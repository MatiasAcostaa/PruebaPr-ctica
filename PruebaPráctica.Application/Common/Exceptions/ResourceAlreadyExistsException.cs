namespace PruebaPráctica.Application.Common.Exceptions;

public sealed class ResourceAlreadyExistsException() : Exception("The resource you attempted to create already exists.");