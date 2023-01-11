using CompanyEmployees.OAuth.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityServer()
    .AddInMemoryApiScopes(InMemoryConfig.GetApiScopes())
    .AddInMemoryApiResources(InMemoryConfig.GetApiResources())
    .AddInMemoryIdentityResources(InMemoryConfig.GetIdentityResources())
    .AddTestUsers(InMemoryConfig.GetUsers())
    .AddInMemoryClients(InMemoryConfig.GetClients())
    .AddDeveloperSigningCredential(); // not something we want to use in production environment

builder.Services.AddControllersWithViews();

var app = builder.Build();

IWebHostEnvironment env = app.Environment;

if (env.IsDevelopment())
    app.UseDeveloperExceptionPage();

app.UseStaticFiles(); // to Configure UI
app.UseRouting(); // to Configure UI

app.UseIdentityServer();

app.UseAuthorization(); //to Configure UI

app.UseEndpoints(endpoints =>{ // to Configure UI
  endpoints.MapDefaultControllerRoute();
});

//app.MapGet("/", () => "Hello World!");

app.Run();
