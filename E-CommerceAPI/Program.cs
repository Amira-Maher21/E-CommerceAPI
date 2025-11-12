
using Autofac.Extensions.DependencyInjection;
using Autofac;
using AutoMapper;
using E_Commerce.Application;
using E_Commerce.Application.CQRS.CustomerManagement.Queries;
using E_Commerce.Application.Profiles;
using E_Commerce.Infrastructure;
using E_Commerce.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using E_CommerceAPI.Extentions;
using E_Commerce.Application.Helpers;
using Autofac.Core;

namespace E_CommerceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<StoreContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));




            builder.Services.AddMediatR(typeof(GETCustomersByIDQueryHandler).Assembly);
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));


             builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);


             


            var mapper = builder.Services.BuildServiceProvider().GetRequiredService<IMapper>();
            MapperHelper.Mapper = mapper;


 
              
 
  





             


            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
                        builder.RegisterModule(new DependencyInjection()));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
