using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using InterviewApi.BusinessEntities;
using InterviewApi.BusinessEntities.Models.Model;
using InterviewApi.BusinessLogic.HttpBase;
using InterviewApi.BusinessLogic.Services.HttpWraper;
using InterviewApi.BusinessLogic.Services.PaymentNotification;
using InterviewApi.BusinessLogic.Services.Validation;
using InterviewApi.Common;
using InterviewApi.Common.MailBase;
using InterviewApi.Filters;
using InterviewApi.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace InterviewApi
{
    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Get configuration service
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();//User secrets

            services.AddDbContext<InterviewApiContext>(options =>
            {
                //options.UseLoggerFactory(GetLoggerFactory());
                options.UseSqlServer(Configuration["AppKeys:DefaultConnection"]);
            });

            //services.AddIdentity<User, Role>(cfg =>
            services.AddIdentity<User, Role>(cfg =>
            {
                //cfg.Password =new PasswordOptions
                //{
                //    RequireDigit = true,
                //};

                //cfg.Lockout = new LockoutOptions
                //{
                //    MaxFailedAccessAttempts = 5,
                //    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60),
                //};

                cfg.User = new UserOptions
                {
                    RequireUniqueEmail = true,
                };
            })
                .AddEntityFrameworkStores<InterviewApiContext>()
                .AddDefaultTokenProviders();

            // Add our Config object so it can be injected
            services.Configure<AppSecretsConfig>(option => Configuration.GetSection("AppKeys").Bind(option));

            services.AddMemoryCache();


            //This piece of code works
            // https://www.youtube.com/watch?v=ryPo5hYHSzM
            services.AddApiVersioning(cfg =>
            {
                cfg.DefaultApiVersion = new ApiVersion(1, 0);
                cfg.AssumeDefaultVersionWhenUnspecified = true;
                cfg.ReportApiVersions = true;
                cfg.ApiVersionReader = new HeaderApiVersionReader("X-Version"); //This works when u want to get version info from the headers only
                //cfg.ApiVersionReader = ApiVersionReader.Combine( //This works when you want to have more than one schema
                //    new HeaderApiVersionReader("X-Version"), //This works when u want to get version info from the headers OR 
                //    new QueryStringApiVersionReader("v")//When u want to get version info as a query string - perameter name is v
                //    );
            });


            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ValidateFilterAttribute));
                //options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAll"));//Add cors policy name here
                options.ReturnHttpNotAcceptable = true;
                //options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
                //options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                options.Filters.Add(new FlashActionLogs());
            })
               .AddNewtonsoftJson(opt =>
               {
                   opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                   opt.SerializerSettings.Formatting = Formatting.Indented;
                   opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                   opt.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
               })
               .SetCompatibilityVersion(CompatibilityVersion.Latest)
               //.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CompanyValidator>())
               .AddXmlDataContractSerializerFormatters()
                //.AddJsonOptions(opt => { opt.JsonSerializerOptions.PropertyNamingPolicy = null; })//Returns Json as Pascal case rather than camelcase
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCors(options => options.AddPolicy("AllowAll", builder =>
            {
                builder
                    .AllowAnyOrigin() //AllowAllOrigins;  
                    .WithMethods("GET", "PUT", "POST", "DELETE") //AllowSpecificMethods;  
                    .WithExposedHeaders("X-Pagination")
                    .AllowAnyHeader(); //AllowAllHeaders;  
            }));

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    // Use the default property (Pascal) casing
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                })
                .AddXmlSerializerFormatters();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Interswitch",
                    Version = "v1",
                    Description = "Interswitch",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact { Name = "Morgan Kamoga", Email = "maugust123@gmail.com", Url = new Uri("https://twitter.com/spboyer"), },
                    License = new OpenApiLicense { Name = "Use under LICX", Url = new Uri("https://example.com/license"), }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {new OpenApiSecurityScheme{ Reference=new OpenApiReference{Id="Bearer",Type=ReferenceType.SecurityScheme}}, new List<string>() }
                });

                c.ResolveConflictingActions(apiDescription => apiDescription.First());

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });

            // Add application services.
            DiContainer(services);
        }


        private static void DiContainer(IServiceCollection services)
        {
            services.AddSingleton<ReadConfig>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IHttpClientWrapper, HttpClientWrapper>();
            services.AddTransient<IInterSwitchAuth, InterswitchAuth>();
            services.AddTransient<ICustomerValidationService, CustomerValidationService>();
            services.AddTransient<IPaymentNotificationService, PaymentNotificationService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseApiErrorHandling();
                //This is valid code
                //app.UseHsts(options => options.MaxAge(365).IncludeSubdomains());//Harden security
                app.UseStatusCodePages();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto //Harden security
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();//goes here if needed

            app.UseXXssProtection(options => options.EnabledWithBlockMode());//Harden security
            app.UseXContentTypeOptions();//Harden security


            app.UseSerilogRequestLogging(opt => opt.EnrichDiagnosticContext = LogHelper.EnrichFromRequest);

            app.UseRouting();
            //Enable CORS policy "AllowCors" before UseMvc
            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("swagger/v1/swagger.json", "Interview");
                c.RoutePrefix = string.Empty;
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
