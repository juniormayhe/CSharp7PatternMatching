## C# 7.0 Pattern matching ##

Pattern matching is a feature that allows you to check if an object is compatible with a specific type. 
There are different kinds of pattern matching and this sample here is about **type pattern**.

Let's check pattern matching types:

### Type pattern

Allows you to check if the type (pattern) of an object is compatible with a specific type.
If object is not null, it can be casted and assigned to a new variable of the specific type.
A code is worth a thousand words:

```
if (o is Person p) 
	Console.WriteLine($"Hello {p.FirstName}!");
```

The other kinds of pattern matching are:

### Constant pattern

Allow you to check if object corresponds to a **constant value**, null, int or whatever.

```
if (o is null) //you are checking if o corresponds to null, but you could stick with ==
	Console.WriteLine("it's a const pattern = null");
  
if (o is 444) //you are checking if o corresponds constant integer 444, you could stick with ==
	Console.WriteLine("it's a const pattern = (int) 444");
```

### Variable pattern

If a source object is not null, you can project its value/reference into a new object (declared with `var`) within an expression.

Also the new object's type will have the underlying type from the original object. So, you could figure new object type using `GetType()`:
```
if (sourceObject is var newObject) //always resolves "true" and projects source object (whether it's null or not) to a new object
	Console.WriteLine($"it's a var pattern with the type {newObject?.GetType()?.Name}"); //if object is not null, you can check its type
```
Notice that if source Object is null, `.GetType` would throw a `NullReferenceException`, that you could avoid using null conditional operator `?`, introduced in C# 6.

```
newObject.GetType().Name //without null conditinal would throw NullReferenceException if source object is null
newObject?.GetType()?.Name //with null conditional operator
```

When to use var pattern? There are some rare cases you would use such as avoiding multiple calls to high load or expensive methods.

```
//bad code: it is evaluating twice an expensive method call
while(sourceObject.ExpensiveMethod() != null)
  result = sourceObject.ExpensiveMethod();
```


```
//better approach: it is evaluating an expensive method call just ONE time with inline assignment into a temp variable
Foo temp;
while(temp = sourceObject.ExpensiveMethod() != null)
  result = temp;

//another better approach: the same as previous code but using var pattern matching
//expensive method output is projected into temp variable and we loop until temp is null
while(sourceObject.ExpensiveMethod() is var temp && temp != null)
  result = temp;
  
//if it's possible, it's better for reading to test result of an expensive method using Type pattern
//expensive method output is projected into a temp variable of type Foo ONLY IF output was NOT NULL!
while(sourceObject.ExpensiveMethod() is Foo temp)
  result = temp;
```

### Get me to the sample

Here's a quick sample of Tuple's new syntax in C# 7.0. We are using a switch statement with **type** pattern matching. 
Of course you could use it with **const pattern** or **var pattern** as well.

> **Note:** In projects older that 4.6 you will need to install System.ValueTuple on Nuget Manager:

```
object[] shapes = {
	new Circle(20),
	new Rectangle(200,20),
	new Rectangle(40,40),
	new Triangle(50, 100),
	null};

Console.WriteLine("C# 7.0 Pattern matching\n");
Console.WriteLine("We will loop an array of shapes, try to cast each object as a specific Class.");
Console.WriteLine("If cast is successful, we'll assign each object \"o\" to its suitable class instance\n");
foreach (object o in shapes) { 
	switch (o)
	{
		case Circle circle:
			Console.WriteLine($"Type pattern: circle.Radius {circle.Radius}");
			break;
		case Triangle triangle:
			Console.WriteLine($"Type pattern: triangle.Height {triangle.Height} x triangle.Base {triangle.Base} Triangle");
			break;
		case Rectangle rectangle when rectangle.Height == rectangle.Width:
			Console.WriteLine($"Type pattern: rectangle.Width {rectangle.Width} x rectangle.Height {rectangle.Height} Square!");
			break;
		case Rectangle rectangle:
			Console.WriteLine($"Type pattern: rectangle.Width {rectangle.Width} x rectangle.Height {rectangle.Height} Rectangle");
			break;
		case null:
			Console.WriteLine("null is a constant pattern");
			break;
		default:
			Console.WriteLine("type unknown");
			break;
	}//switch

}//foreach

//set o1 into someobject within an if expression
object o1 = new Foo { MyProperty = 1 };
if (o1 is var someObject)
	Console.WriteLine($"Var pattern: o1 is not null and a var pattern was set with underlying type {someObject?.GetType()?.Name}. Member value: {(someObject as Foo)?.MyProperty}");

//set o2 into someobject within an if expression
object o2 = null;
if (o2 is var someObject2)
	Console.WriteLine($"Var pattern: o2 is null and a var pattern cannot be set. There is no underlying type: {someObject2?.GetType()?.Name}");

Console.WriteLine("\nPress any key to continue...");
Console.ReadKey();
```
