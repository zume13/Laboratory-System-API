using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using SharedKernel.Constants;
using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;

namespace Laboratory_Management_API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Laboratory Management API",
                    Version = "v1",
                    Description = "API for the PDDL Diagnostics Clinic"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter your JWT token."
                });

                c.AddSecurityRequirement(document =>
                    new OpenApiSecurityRequirement
                    {
                        [new OpenApiSecuritySchemeReference("Bearer", document)] =
                            new List<string>()
                    });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,

                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)),

                        RoleClaimType = ClaimTypes.Role,
                        NameClaimType = ClaimTypes.NameIdentifier,

                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddRateLimiter(opt =>
            {
                opt.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                opt.AddPolicy(SystemConstants.RateLimits.perUser, contex =>
                {
                    var userId = contex.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? SystemConstants.RateLimits.unknown;

                    return RateLimitPartition.GetSlidingWindowLimiter(partitionKey: userId,
                        factory: _ => new SlidingWindowRateLimiterOptions
                        {
                            PermitLimit = 100,
                            Window = TimeSpan.FromMinutes(1),
                            SegmentsPerWindow = 6,
                            QueueLimit = 0
                        });

                });

                opt.AddPolicy(SystemConstants.RateLimits.anonymous, context =>
                {
                    var ip = context.Connection.RemoteIpAddress?.ToString() ?? SystemConstants.RateLimits.unknown;

                    return RateLimitPartition.GetSlidingWindowLimiter(
                        partitionKey: ip,
                        factory: _ => new SlidingWindowRateLimiterOptions
                        {
                            PermitLimit = 100,
                            Window = TimeSpan.FromMinutes(1),
                            SegmentsPerWindow = 6,
                            QueueLimit = 0
                        });
                });
            }
            );

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(SystemConstants.AuthPolicies.adminOnly, policy =>
                {
                    policy.RequireRole(SystemConstants.Roles.Admin);
                });

                opt.AddPolicy(SystemConstants.AuthPolicies.companyPersonnel, policy =>
                {
                    policy.RequireRole(SystemConstants.Roles.Admin, SystemConstants.Roles.ClinicalStaff);
                });
            });

            return services;
        }
    }
}