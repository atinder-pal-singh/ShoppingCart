using Microsoft.EntityFrameworkCore;
using ShoppingCart.DbContexts;
using ShoppingCart.Extensions;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();
builder.Services.AddDbContext<ShoppingDbContext>(o => o.UseSqlite(builder.Configuration["ConnectionStrings:ShoppingDbConnectionString"]));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("RequireUserFromIndia", policy =>
    {
        policy.RequireRole("admin");
        policy.RequireClaim("country", "India");
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("TokenAuth",
        new()
        {
            Name = "admin",
            Description = "Token based authentication and authroization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "Bearer",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header
        });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new()
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "TokenAuth"
                }
            }, new List<string> ()
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    //app.UseExceptionHandler(message =>
    //{
    //    message.Run(async handler =>
    //    {
    //        handler.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    //        handler.Response.ContentType = "text/html";
    //        await handler.Response.WriteAsync("An unexpected problem happened.");
    //    });
    //});
}

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.RegisterProductEndPoints();
app.RegisterImageEndPoints();

//recreate and migrate the database on each run, for demo purposes
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<ShoppingDbContext>();
    context.Database.EnsureDeleted();
    context.Database.Migrate();
}

app.Run();
