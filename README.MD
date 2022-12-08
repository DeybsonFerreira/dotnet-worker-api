# DOTNET WORKER

## Comando

- dotnet new worker

```
CREATE TABLE Messages (
    [Id]  INT NOT NULL IDENTITY PRIMARY KEY,
	[Text] varchar(300),
    [Read] bit
);



```

# Packages

dotnet add package Microsoft.EntityFrameworkCore --version 7.0.0
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 7.0.0
dotnet add package Microsoft.Extensions.Configuration --version 7.0.0
# Scaffold

dotnet ef dbcontext scaffold "Data Source=(localdb)\localhost;Initial Catalog=MyApp" Microsoft.EntityFrameworkCore.SqlServer

Scaffold-DbContext "Data Source=(localdb)\localhost;Initial Catalog=MyApp" Microsoft.EntityFrameworkCore.SqlServer
