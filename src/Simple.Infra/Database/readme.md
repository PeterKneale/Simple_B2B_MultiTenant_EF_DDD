https://learn.microsoft.com/en-us/ef/core/
```shell
dotnet ef dbcontext scaffold \
  "Username=postgres;Password=password;Database=db;Host=localhost;Port=5432;" \
  Npgsql.EntityFrameworkCore.PostgreSQL \
  --context Db \
  --context-dir Database \
  --output-dir Domain \
  --no-onconfiguring	
  
```
