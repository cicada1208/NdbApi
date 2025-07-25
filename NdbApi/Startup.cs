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
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); // 使該 Api 可用 big5
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
                        // 設定允許跨域的來源，有多個的話可用 `,` 隔開
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
                // System.Text.Json: 官方取代 Newtonsoft Json.NET 的解決方案
                // AddJsonOptions: System.Text.Json 透過下列設定亦可於 JsonResult 轉換中文內容

                // Use the default property casing
                o.JsonSerializerOptions.PropertyNamingPolicy = null;
                o.JsonSerializerOptions.DictionaryKeyPolicy = null;

                // 允許基本拉丁英文及中日韓文字維持原字元
                //o.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CjkUnifiedIdeographs);
                o.JsonSerializerOptions.Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

                o.JsonSerializerOptions.WriteIndented = true;

                //o.JsonSerializerOptions.IgnoreNullValues = true;
            });
            //.AddNewtonsoftJson(o =>
            //{
            //    // AddNewtonsoftJson: 預設即可於 JsonResult 轉換中文內容

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
                // 可攔截接下來中介軟體中擲回的未處理例外狀況。
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

            app.UseAuthentication(); // 先驗證、再授權

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
