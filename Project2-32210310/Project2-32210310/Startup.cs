using JWTAuthentication.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
//using Project2_32210310.Authentication;
using Project2_32210310.Models;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.JsonPatch;

namespace JWTAuthentication

{

    public class Startup

    {

        public Startup(IConfiguration configuration)

        {

            Configuration = configuration;

        }



        public IConfiguration Configuration { get; }



        // This method gets called by the runtime. Use this method to add services to the container.   

        public void ConfigureServices(IServiceCollection services)

        {

            services.AddControllers();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("name=ConnectionStrings:ConnStr"));
            //services.AddControllers().AddNewtonsftJson;



            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v2", new OpenApiInfo

                {

                    Title = "Project 2-32210310",

                    Version = "v2"

                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()

                {

                    //Name = "Authorization",
                    Name = "Authorization",


                    Type = SecuritySchemeType.ApiKey,

                    Scheme = "Bearer",

                    BearerFormat = "JWT",

                    In = ParameterLocation.Header,

                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",

                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement {

        {

            new OpenApiSecurityScheme {

                Reference = new OpenApiReference {

                    Type = ReferenceType.SecurityScheme,

                        Id = "Bearer"

                }

            },

            new string[] {}

        }

    });

            });


            // For Entity Framework   

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnStr")));

            services.AddDbContext<_32210310Project2Context>(options => options.UseSqlServer(Configuration.GetConnectionString("ConnStr")),ServiceLifetime.Transient);



            // For Identity   

            services.AddIdentity<ApplicationUser, IdentityRole>()

                .AddEntityFrameworkStores<ApplicationDbContext>()

                .AddDefaultTokenProviders();



            // Adding Authentication   

            services.AddAuthentication(options =>

            {

                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            })



            // Adding Jwt Bearer   

            .AddJwtBearer(options =>

            {

                options.SaveToken = true;

                options.RequireHttpsMetadata = false;

                options.TokenValidationParameters = new TokenValidationParameters()

                {

                    ValidateIssuer = true,

                    ValidateAudience = true,

                    ValidAudience = Configuration["JWT:ValidAudience"],

                    ValidIssuer = Configuration["JWT:ValidIssuer"],

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))

                };

            });

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.   

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)

        {

            if (env.IsDevelopment())

            {

                app.UseDeveloperExceptionPage();

            }



            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>

            {

                endpoints.MapControllers();

            });

            app.UseSwagger();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "Project2-32210310"));


        }


    }

}