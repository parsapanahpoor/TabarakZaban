namespace Identity.Application.User.Commands.ResetPassword;

public record ResetPasswordDto(
    string UserId , 
    string Token , 
    string Password ,
    string ConfirmPassword);

