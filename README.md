# SimpleIoc
## Build SimpleIoC
From the < Project > folder:
```
$ dotnet restore
$ dotnet build
```

## Test SimpleIoc
From the < Project > folder:
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