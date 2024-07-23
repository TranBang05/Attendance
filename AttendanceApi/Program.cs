using System.Text;
using AttendanceApi.Dto.Mapper;
using AttendanceApi.Models;
using AttendanceApi.Service;
using AttendanceApi.Service.Impl;
using AttendanceApi.Services.gRPC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddOData(opt => opt
    .Select()
    .Expand()
    .Filter()
    .OrderBy()
.Count()
.SetMaxTop(100)
    );
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });


builder.Services.AddAuthorization(options =>
{
   
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("Teacher", policy => policy.RequireRole("Teacher"));
    options.AddPolicy("Student", policy => policy.RequireRole("Student"));

    
    options.AddPolicy("View", policy => policy.RequireClaim("Permission", "View"));
    options.AddPolicy("Edit", policy => policy.RequireClaim("Permission", "Edit"));
    options.AddPolicy("Create", policy => policy.RequireClaim("Permission", "Create"));
    options.AddPolicy("Delete", policy => policy.RequireClaim("Permission", "Delete"));
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<ITeacherService, TeacherSerivce>();
builder.Services.AddScoped<IStudentsService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IRoleService, RoleService>();


builder.Services.AddDbContext<AttendanceContext>(
              options => options.UseSqlServer(builder.Configuration.GetConnectionString("LoadDb")));

builder.Services.AddAutoMapper(typeof(MyMapper).Assembly);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<SubjectGPCService>();
app.MapGrpcService<RoleRPCService>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
