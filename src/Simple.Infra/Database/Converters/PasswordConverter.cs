using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Simple.Domain.Users;

namespace Simple.Infra.Database.Converters;

public class PasswordConverter() : ValueConverter<Password, string>(password => password.Hashed, s => Password.Create(s));