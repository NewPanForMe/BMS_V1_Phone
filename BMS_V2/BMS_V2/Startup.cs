using System.Text;
using BMS_V2_Db;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Ys.Tools.Config;

namespace BMS_V2;

public class Startup
{
    public IConfiguration _configuration { get; set; }

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    // 该方法由运行时调用，使用该方法向DI容器添加服务
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigin", builder =>
            {
                builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); ;
            });
        });
        // 注入Jwt
        services.AddAuthentication(option =>
        {
            //认证middleware配置
            option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(config =>
        {
            config.TokenValidationParameters = new TokenValidationParameters
            {
                //Token颁发机构
                ValidIssuer = TokenConfig.Instance.IsUser,
                //颁发给谁
                ValidAudience = TokenConfig.Instance.Audience,
                //这里的key要进行加密
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConfig.Instance.Key)),
                //是否验证Token有效期，使用当前时间与Token的Claims中的NotBefore和Expires对比
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };
            config.SaveToken = true;
        });

        services.AddMemoryCache();

        services.Configure<FormOptions>(x =>
        {
            x.MultipartHeadersLengthLimit = 300000000;//文件最大300M
        });

        //注入数据库
        RegisterDb(services);

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    // 该方法由运行时调用，使用该方法配置HTTP请求管道
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime appLifetime)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseRouting();
        //UseCors 必须放在 之后 UseRouting 和之前 UseAuthorization。
        //这是为了确保 CORS 标头包含在已授权和未经授权的调用的响应中。
        app.UseCors("AllowAllOrigin");
        app.UseAuthentication();
        app.UseAuthorization();

        // app.UseMiddleware<JwtVersionMiddleWare>();

        app.UseEndpoints(x =>
        {
            x.MapControllers();
            x.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        });
    }

    /// <summary>
    /// 注入数据库
    /// </summary>
    /// <param name="service"></param>
    private void RegisterDb(IServiceCollection service)
    {
        service.Configure<DbConfig>(_configuration.GetSection("DbConfig"));
        _configuration.Bind("DbConfig", DbConfig.Instance);
        //已经注入，可以直接使用
        service.AddDbContext<BmsV2DbContext>(opt =>
        {
            opt.UseSqlServer(DbConfig.Instance.SqlServer);
        });
    }



}