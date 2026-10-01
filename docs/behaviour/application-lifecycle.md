# Application Lifecycle Behaviour

## Metadata
- Purpose: Describe host startup, request readiness, and shutdown boundaries.
- Scope: `Program`, `Startup`, dependency registration, store creation, middleware, and framework disposal.
- Primary source areas: `Program.cs`, `Startup.cs`, `ServiceCollectionExtensions.cs`.
- Related documents: [Startup Flow](../flows/startup.md), [Architecture](../architecture.md), [Configuration](../configuration.md), [Build And Deployment](../build-and-deployment.md).

## Startup
`Program.Main` calls `CreateHostBuilder(args).Build().Run()`. The default host loads configuration and environment, then invokes `Startup.ConfigureServices`. Services bind application settings, register Nuci middleware dependencies, and register singleton repository, service, text, and logging components.

`Startup.Configure` resolves `DataStoreSettings`, calls `CreateStoreIfMissing`, then registers exception handling, scanner protection, request logging, development diagnostics, HTTPS redirection, CORS, default/static files, routing, authorisation, and controller endpoints in that order.

## Ready State
The process is ready for HTTP requests after store initialisation and endpoint mapping complete. A missing store is created as an empty JSON array. Existing store content is not validated or repaired during startup.

## Request And Shutdown
Requests enter the middleware pipeline and route to controller actions. There is no background process or scheduled operation. Host shutdown and dependency disposal are framework-managed; the repository defines no explicit flush or cleanup callback. Integration fixture disposal demonstrates the test-only cleanup path for temporary files.
