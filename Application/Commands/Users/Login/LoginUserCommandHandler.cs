using DTOs;
using MediatR;

namespace Application.Commands.Users.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, ValidationResult>
{
    public async Task<ValidationResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}