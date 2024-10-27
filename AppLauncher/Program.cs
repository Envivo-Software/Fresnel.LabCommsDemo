using LabCommsModel.Design1;
using Envivo.Fresnel.Bootstrap.WebServer;
using Envivo.Fresnel.Features;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.AddFresnel(opt =>
{
    opt
    .WithModelAssemblyFrom<Sample>()
    .WithFeature(Feature.UI_DoodleMode, FeatureState.On)
    .WithDefaultFileLogging();

    // Register your own dependencies here:
    builder.Services.AddSingleton<LabCommsModel.Design1.Dependencies.SampleRepository>();
    builder.Services.AddSingleton<LabCommsModel.Design2.Dependencies.SampleRepository>();
});

var app = builder.Build();

app.UseFresnel();

// Antiforgery must come at the end.
// See https://github.com/dotnet/aspnetcore/issues/49654#issuecomment-1654754907
app.UseHttpsRedirection();
app.UseAntiforgery();

await app.RunAsync();