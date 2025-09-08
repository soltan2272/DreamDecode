using DreamDecode.Application;
using DreamDecode.Domain.User.Entities;
using DreamDecode.Domain.User.Enums;
using DreamDecode.Infrastructure;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructure(builder.Configuration); // from Infrastructure
builder.Services.AddApplication(); // from Application
builder.Services.AddControllers();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", p => p.RequireRole(Roles.Admin.ToString()));
    options.AddPolicy("UserOnly", p => p.RequireRole(Roles.User.ToString()));
});
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");


    c.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
await SeedDefaultsAsync(app.Services); // seed roles & default admin

app.Run();

// local method: seed roles & an admin
static async Task SeedDefaultsAsync(IServiceProvider sp)
{
    using var scope = sp.CreateScope();
    var roles = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var users = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    foreach (var r in new[] { Roles.Admin.ToString(), Roles.User.ToString() })
        if (!await roles.RoleExistsAsync(r)) await roles.CreateAsync(new IdentityRole(r));

    var adminEmail = "admin@dreams.com";
    var admin = await users.FindByEmailAsync(adminEmail);
    if (admin is null)
    {
        admin = new ApplicationUser { Email = adminEmail, UserName = adminEmail, FullName = "Dreams Admin" };
        await users.CreateAsync(admin, "Admin#12345");
        await users.AddToRoleAsync(admin, Roles.Admin.ToString());
    }
}