# Attribute Binding Sample

This sample is mostly the same as the basic sample, except it demonstrates an alternative way of binding a View to a Presenter; namely, attribute binding.

# How It Works

Using attibute binding to bind a View to a Presenter requires a tiny bit od ceremony. All that is required is to decorate the View class with an attribute from the framework called PresnterBinding, passing the type of the presenter to that attribute e.g.    
    
```csharp
    PresenterBinding(typeof(OrdersPresenter))]
    public partial class Main : MvpForm, IMainView
```

There are a couple of things which this enables:    
1. the presenter can be named anything and does not need to follow any naming convention; and
2. the presenter can be in another project. The framework does not need to try and locate it. By passing the type in, it knows exactly what type is to be bound to the View.

# Typing a View Abstraction to a Model
This sample also demonstrates how to type a view abstraction to a model.    
  
As you can see above, the Main form implements the `IMainView` interface. Unlike the Basic sample, here `IMainView` inherits from `IView<MainModel>` (rather than simply `IView`). **WinformsMVP.NET** provides a generic version of `IView<TModel>` which facilitates this.

This enables you to keep state out of the Presenter, which is not meant to maintain the state of whatever view it is working with.