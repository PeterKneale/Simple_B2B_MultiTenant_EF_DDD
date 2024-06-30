using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Simple.Domain.Users;

namespace Simple.Infra.Database.Converters;

public class EmailConverter() : ValueConverter<EmailAddress, string>(emailAddress => emailAddress.Value, s => EmailAddress.Create(s));