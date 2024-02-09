using Application;
using Authentication;
using Infrastructure;
using InternshipsAppApi;

const string CORS_POLICY = "cors_policy";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => options.AddPolicy(CORS_POLICY, builder =>
    builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()));

builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();
app.UseCors(CORS_POLICY);
app.UseAuthentication();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
