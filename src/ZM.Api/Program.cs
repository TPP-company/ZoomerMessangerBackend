using ZM.Infrastructure.RoutePrefix;
using ZM.Application;
using ZM.Infrastructure;
using ZM.Infrastructure.Persistence;
using ZM.Api.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opts =>
{
    opts.Conventions.Add(new RoutePrefixConvention());
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddExceptionHandler<ExceptionHandler>();

builder.Services.AddCors(p => p.AddPolicy("corspolisy", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();
app.UseExceptionHandler(opt => { });

Seeder.Seed(app.Services);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("corspolisy");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHub<P2PChatHub>("/hubs/chats/p2p");

app.MapControllers();

app.Run();
