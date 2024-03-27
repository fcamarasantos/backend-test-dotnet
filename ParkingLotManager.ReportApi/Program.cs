using Microsoft.OpenApi.Models;
using ParkingLotManager.ReportApi.Interfaces;
using ParkingLotManager.ReportApi.Mappings;
using ParkingLotManager.ReportApi.Models;
using ParkingLotManager.ReportApi.REST;
using ParkingLotManager.ReportApi.Services;

var builder = WebApplication.CreateBuilder(args);

ConfigureMvc(builder);
ConfigureSwagger(builder);
ConfigureMappings(builder);

//builder.Services.AddSingleton<IVehicleQuery, ParkingLotManagerApiRest>();
builder.Services.AddSingleton<ParkingLotManagerWebApiService>();
builder.Services.AddSingleton<FlowManagementService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseAuthorization();
app.MapControllers();

app.Run();

static void ConfigureMvc(WebApplicationBuilder builder)
{
    builder.Services.AddControllers().ConfigureApiBehaviorOptions(x =>
    {
        x.SuppressModelStateInvalidFilter = true;
    });
}
static void ConfigureSwagger(WebApplicationBuilder builder)
{
    var linkedin = "https://www.linkedin.com/in/matheusarb/";
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(x =>
    {
        x.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Parking Lot Report API",
            Description = "A Report Api which generates specific reports with the given request",
            Contact = new OpenApiContact
            {
                Name = "Matheus Ribeiro",
                Email = "mat.araujoribeiro@gmail.com",
                Url = new Uri(linkedin)
            },
            License = new OpenApiLicense
            {
                Name = "Mit License"
            },
            Version = "v1"
        });

        var xmlFile = "ParkingLotManager.ReportApi.xml";
        var xmlPath = Path.Combine(Directory.GetCurrentDirectory(), xmlFile);
        x.IncludeXmlComments(xmlPath);
    });

}
static void ConfigureMappings(WebApplicationBuilder builder)
{
    builder.Services.AddAutoMapper(typeof(VehicleMapping));
}
