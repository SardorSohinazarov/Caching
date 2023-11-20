var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDistributedSqlServerCache(options =>
{
    options.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DistributedSqlServerCacheDB;";
    options.DefaultSlidingExpiration = TimeSpan.FromSeconds(20);
    options.ExpiredItemsDeletionInterval = TimeSpan.FromMinutes(5);
    options.SchemaName = "dbo";
    options.TableName = "Caches";
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
