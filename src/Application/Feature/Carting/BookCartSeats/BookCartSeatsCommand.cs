using MediatR;

namespace Ticketing.Application.Feature.Carting.BookCartSeats;

public record BookCartSeatsCommand(Guid CartId) : IRequest<BookCartSeatsResponse>;
