
using AIxplorer.Server.Bootstrap;

WebApplicationOptions options = new WebApplicationOptions { Args = args };
CompositionRoot compositionRoot = new CompositionRoot();

var builder = compositionRoot.CreateBuilder(options);



var app = builder.Build();

MiddlewareConfigurator.Configure(app, app.Environment);



app.Run();
