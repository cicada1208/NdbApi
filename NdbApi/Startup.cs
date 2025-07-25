using Lib.Api;
using Lib.Api.Configs;
using Lib.Api.Middlewares;
using Lib.Api.ModelBinders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace NdbApi
{
    public class Startup
    {
        public const string CychCorsPolicy = "CychCorsPolicy";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // �ϸ� Api �i�� big5
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var settings = Configuration.Get<AppSettings>();

            services.Configure<AppSettings>(Configuration);

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearerConfig(settings);

            services.AddCors(options =>
            {
                options.AddPolicy(CychCorsPolicy, builder =>
                {
                    builder
                        // �]�w���\��쪺�ӷ��A���h�Ӫ��ܥi�� `,` �j�}
                        //.WithOrigins("http://*.cych.org.tw", "https://*.cych.org.tw", "*")
                        //.SetIsOriginAllowedToAllowWildcardSubdomains()
                        .SetIsOriginAllowed(origin => true) // allow any origin
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                });
            });

            services
                .AddControllers(o =>
                {
                    o.AllowEmptyInputInBodyModelBinding = true;
                    o.ModelBinderProviders.Insert(0, new StringBinderProvider());
                })
            .AddJsonOptions(o =>
            {
                // System.Text.Json: �x����N Newtonsoft Json.NET ���ѨM���
                // AddJsonOptions: System.Text.Json �z�L�U�C�]�w��i�� JsonResult �ഫ���夺�e

                // Use the default property casing
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
                o.JsonSerializerOptions.DictionaryKeyPolicy = null;

                // ���\�򥻩ԤB�^��Τ�������r������r��
                //o.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs);
                o.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

                o.JsonSerializerOptions.WriteIndented = true;

                //o.JsonSerializerOptions.IgnoreNullValues = true;
            });
            //.AddNewtonsoftJson(o =>
            //{
            //    // AddNewtonsoftJson: �w�]�Y�i�� JsonResult �ഫ���夺�e

            //    // Use the default property casing
            //    //o.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //    o.UseMemberCasing();

            //    o.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "NdbApi", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSingleton<ApiUtilLocator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // �i�d�I���U�Ӥ����n�餤�Y�^�����B�z�ҥ~���p�C
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "NdbApi v1");
            });

            app.UseLoggerMiddleware();

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(CychCorsPolicy);

            app.UseAuthentication(); // �����ҡB�A���v

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
