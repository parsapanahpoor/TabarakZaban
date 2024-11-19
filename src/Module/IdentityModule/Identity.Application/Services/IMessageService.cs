using System;

namespace Identity.Application.Services;

public interface IMessageService
{
    Task SendActivationLink(string? senderAddress , string? targetAddress , string? subject , string message);
}
