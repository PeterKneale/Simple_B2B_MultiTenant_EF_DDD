using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Simple.Domain.Tenants;

namespace Simple.Infra.Database.Converters;

public class TenantNameConverter() : ValueConverter<TenantName, string>(name => name.Value, s => TenantName.Create(s));