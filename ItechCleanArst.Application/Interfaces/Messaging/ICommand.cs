
using ItechCleanArst.Domain.Shared;
using MediatR;

namespace ItechCleanArst.Application.Interfaces.Messaging;

public interface ICommand : IRequest<Result>
{ }

// public interface ICommand<TResponse> : IRequest<Result<TResponse>>
// { }
