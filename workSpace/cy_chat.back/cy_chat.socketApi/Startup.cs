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

            #region ע�����ݿ�
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

            #region ע��swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo() { Title = "��ʱ����ϵͳ��ؽӿ�", Version = "v1.0" });
                c.DocInclusionPredicate((docName, apiDesc) => apiDesc.GroupName == docName.ToUpper());
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//��ȡӦ�ó�������Ŀ¼�����ԣ����ܹ���Ŀ¼Ӱ�죬������ô˷�����ȡ·����
                var xmlPath = Path.Combine(basePath, "WebSite.xml");
                c.IncludeXmlComments(xmlPath);

                #region Jwt
                //����Ȩ��С��
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                //��header�����token�����ݵ���̨
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT��Ȩ(���ݽ�������ͷ�н��д���)ֱ���������������Bearer {token}(ע������֮����һ���ո�) \"",
                    Name = "Authorization",//jwtĬ�ϵĲ�������
                    In = ParameterLocation.Header,//jwtĬ�ϴ��Authorization��Ϣ��λ��(����ͷ��)
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
            //�������
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            #region ����Swagger�м��
            // ����Swagger�м��
            app.UseSwagger(c => c.RouteTemplate = "swagger/{documentName}/swagger.json");
            // ����SwaggerUI
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v1/swagger.json", "V1");
            });
            #endregion

            app.UseRouting();
            //ע���쳣�м��
            app.UseMiddleware<ExceptionMiddlewares>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
