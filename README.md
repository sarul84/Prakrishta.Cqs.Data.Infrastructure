# Prakrishta.Cqs.Data.Infrastructure

![Screenshot](CQS.PNG)

![Screenshot](CQS-CommandFlow.PNG)

![Screenshot](CQS-QueryFlow.PNG)

To add all implementation class at oneshot with scrutor
```
services.Scan(
      x =>
      {
          var entryAssembly = Assembly.GetEntryAssembly();
          var referencedAssemblies = entryAssembly.GetReferencedAssemblies().Select(Assembly.Load);
          var assemblies = new List<Assembly> { entryAssembly }.Concat(referencedAssemblies);

          x.FromAssemblies(assemblies)
              .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                  .AsImplementedInterfaces()
                  .WithScopedLifetime()
              .AddClasses(classes => classes.AssignableTo(typeof(IAsyncCommandHandler<>)))
                  .AsImplementedInterfaces()
                  .WithScopedLifetime()
              .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                  .AsImplementedInterfaces()
                  .WithScopedLifetime()
              .AddClasses(classes => classes.AssignableTo(typeof(IAsyncQueryHandler<,>)))
                  .AsImplementedInterfaces()
                  .WithScopedLifetime();
      });
```
