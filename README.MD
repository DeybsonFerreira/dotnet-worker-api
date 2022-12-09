# DOTNET WORKER - WITH RABBIT MQ - Scaffold SQL


# Packages

dotnet add package Microsoft.EntityFrameworkCore --version 7.0.0

dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.0

dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.0

dotnet add package Microsoft.EntityFrameworkCore.Tools --version 7.0.0

dotnet add package Microsoft.Extensions.Configuration --version 7.0.0

dotnet add package RabbitMQ.Client --version 6.4.0

dotnet add package Newtonsoft.Json --version 13.0.2

# Scaffold

dotnet ef dbcontext scaffold "Data Source=(localdb)\localhost;Initial Catalog=MyApp" Microsoft.EntityFrameworkCore.SqlServer

Scaffold-DbContext "Data Source=(localdb)\localhost;Initial Catalog=MyApp" Microsoft.EntityFrameworkCore.SqlServer



```
CREATE TABLE Messages (
    [Id]  INT NOT NULL IDENTITY PRIMARY KEY,
	[Text] varchar(300),
    [Read] bit
);
```