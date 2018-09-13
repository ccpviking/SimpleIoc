# SimpleIoc
## Build SimpleIoC
From the < Project > folder:
```
$ dotnet restore
$ dotnet build
```

## Test SimpleIoc
From the < Project >/simple_ioc_test/ folder:
```
$ dotnet restore
$ dotnet build
$ dotnet test
```

## Singleton Registration
```
_ioc.Register<ITestObject, TestObject>(new TestObject(), LifecycleScope.Singleton);
```
* Current implementation allows for objects to only be singleton instances

## Transient Registration
```
_ioc.Register<ITestObject, TestObject>(() => new TestObject(), LifecycleScope.Transient);
```
* Current implementation allows for factories to be transient or singleton

## Notes:
* I have not hooked this up with the middleware to auto use this ioc, so the InfoController shows that it currently accesses an ApplicationIoc object.
* Still working on parameter injection

## ToDo:
* **[Done]** Write an IoC (Inversion of Control) container, also known as a Dependency Injection container.
* **[Done]** The container must allow you to register types.
	* Example: container.Register<ICalculator, Calculator>()
* The container must be aware and control object lifecycle for transient objects (a new instance is created for each request), and singleton objects (the same instance is returned for each request).
	* Example: container.Register<ICalculator, Calculator>(LifecycleType.Transient), or container.Register<ICalculator, Calculator>(LifecycleType.Singleton)
* The default lifecycle for an object should be transient
* **[Done]** You must be able to resolve registered types through the container
	* Example: container.Resolve<ICalculator>()
* **[Done]** If you try to resolve a type that the container is unaware of it should throw an informative exception
* **[In Progress]** When resolving from the container for a registered type, if that type has constructor arguments which are also registered types the container should inject the instances into the constructor appropriately (this is where dependency injection applies).
	* Example Constructor: public UsersController(ICalculator calculator,  IEmailService emailService). If all 3 types for the controller, calculator, and email service are registered you should be able to resolve an instance of the UsersController.
* **[Done]** You must write tests for all of this behavior using xUnit.
* **[Done]** With a simple new ASP.NET MVC Web Application, wire things up so your Controllers can be constructed using your containers. There are many well documented ways to accomplish this with other containers.
* **[Done]** You must use git for source control and push your code to github.com. Please send me the link to your repository at least 1 day before the second interview.
* [:)] General Suggestion: Don't let the overall tasks overwhelm you. Break everything into smaller pieces that build up to the larger solution.
* [:)] Be prepared to answer a question along the lines of… How would your code change if given the requirement to add a new lifecycle (ThreadStatic for instance)? Would you be required to add new code, or modify existing code?