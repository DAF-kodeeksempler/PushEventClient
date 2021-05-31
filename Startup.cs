using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNet.OData.Batch;
using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PushEventClient.Database;
using DataDistributor;

namespace PushEventClient
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

            //LOOK: Notice that we add the odata services here
            services.AddOData();
            services.AddODataQueryFilter();

            // Set serialization of json files to ignore refferences, so we don't get
            // into a recursion loop.
            services.AddControllersWithViews().AddNewtonsoftJson(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            // Add the program model and database stuff
            services.AddDbContext<DomainModel>(options =>
            {
                options.UseSqlite("Filename=TestDatabase.db");
            });

            services.AddControllers(mvcOptions => mvcOptions.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            // LOOK: We use odata routing also
            app.UseODataBatching();
            app.UseRouting();
            app.UseAuthorization();

            ODataBatchHandler odataBatchHandler = new DefaultODataBatchHandler();

            odataBatchHandler.MessageQuotas.MaxOperationsPerChangeset = 100;
            odataBatchHandler.MessageQuotas.MaxPartsPerBatch = 10;

            //LOOK: Add the Mvc, and also odata stuff
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Filter();
                routeBuilder.MapODataServiceRoute("odata", "odata/{odataMetadata}", model: GetEdmModel(), batchHandler: odataBatchHandler);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
        public IEdmModel GetEdmModel()
            {
                ODataModelBuilder builder = new ODataConventionModelBuilder();
                builder.ContainerName = "EventsContext";
                // notice that we use the event class, and not DAFEvents.
                // We do this to be compliant with det namespace, and structure of the
                // odata packet datafordeleren sends to us.
                builder.EntitySet<Event>("Events");
                var model = builder.GetEdmModel();
                return model;

            }
    }
}
