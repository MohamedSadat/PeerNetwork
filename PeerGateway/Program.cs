
using PeerLib.Data;
using PeerLib.Services;

namespace PeerGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(policy => policy.AddPolicy("open", opt => opt.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            builder.Services.AddResponseCaching();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<AppModel>();
            builder.Services.AddScoped<MsgService>();
            builder.Services.AddScoped<NodeServices>();

            builder.Services.AddHealthChecks();

            var app = builder.Build();
            using (var serviceScope = app.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                var myappdep = services.GetRequiredService<AppModel>();

                //      myappdep.ContractPath = builder.Configuration.GetConnectionString("cn");
                //    myappdep.ConLog = builder.Configuration.GetConnectionString("cnservice");
                myappdep.Node.NodeAddress = builder.Configuration.GetValue<string>("NodeAddress");

            }

            // Configure the HTTP request pipeline.
        //    if (app.Environment.IsDevelopment())
           // {
                app.UseSwagger();
                app.UseSwaggerUI();
          //  }
            app.UseCors("open");
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapHealthChecks("/health").AllowAnonymous();
            app.UseResponseCaching();
            app.Run();
        }
    }
}