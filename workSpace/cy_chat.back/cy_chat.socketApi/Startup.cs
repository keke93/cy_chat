using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cy_chat.entity;
using cy_chat.entity.Common;
using cy_chat.mysqlDB.BaseModels;
using cy_chat.mysqlDB.Startup;
using cy_chat.utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace cy_chat.socketApi
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            CommonUtil.Init();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            #region 注册数据库
            var connection = BuildConnectionString(services, "ConnectionString");

            //var rsaKeys = BuildRsaKays(services, "RsaKeys");
            services.AddDbContext<MyDbConetxt>(options => options.UseMySql(connection.ToString()));

            services.AddDataAccess<MyDbConetxt>();
            #endregion

            #region
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(TokenManagementModel.Secret)),
                    ValidIssuer = TokenManagementModel.Issuer,
                    ValidAudience = TokenManagementModel.Audience,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            #endregion

            #region 注入swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "即时聊天系统相关接口", Version = "v1.0" });
                c.DocInclusionPredicate((docName, apiDesc) => apiDesc.GroupName == docName.ToUpper());
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "WebSite.xml");
                c.IncludeXmlComments(xmlPath);

                #region Jwt
                //开启权限小锁
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                //在header中添加token，传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传递)直接在下面框中输入Bearer {token}(注意两者之间是一个空格) \"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });


                #endregion
            });
            #endregion
        }

        private object BuildConnectionString(IServiceCollection services, string connectionName)
        {
            services.Configure<ConnectionString>(Configuration.GetSection(connectionName));

            var configureOptions = services.BuildServiceProvider().GetRequiredService<IConfigureOptions<ConnectionString>>();
            var connectionString = new ConnectionString();
            configureOptions.Configure(connectionString);

            return connectionString;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //允许跨域
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            #region 启用Swagger中间件
            // 启用Swagger中间件
            app.UseSwagger(c => c.RouteTemplate = "swagger/{documentName}/swagger.json");
            // 配置SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "V1");
            });
            #endregion

            app.UseRouting();
            //注册异常中间件
            app.UseMiddleware<ExceptionMiddlewares>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
