# Basic Sample

This sample shows the most basic usage of WinformsMVP.NET. It is using convention to bind the Presenters to the Views. To examine this, lets first start with the **MainView** form. This is the form that loads on startup.

## MainView

Note the name of the form: **Main**View  
Note the name of the presenter: **Main**Presenter

They are both prefixed with **Main**. The framework, when binding by convention, will look in the **Presenters** folder and try and find a Presenter with a same prefix as the form. (It works the same with UserControls).

There are a couple of other places that the framework will search for presenters. The actual code looks something like (the 4 options):

    "{namespace}.Logic.Presenters.{presenter}",
    "{namespace}.Presenters.{presenter}",
    "{namespace}.Logic.{presenter}",
    "{namespace}.{presenter}"

So, as an alternative (for example), you may put your Presenters inside a **Logic** folder.

You will also see that `MainView`:

1.  inherits from `MvpForm`, instead of `Form`.
2.  implements `IMainView`, which happens to be the closed generic type of the class which `MainPresenter` inherits from i.e. `Presenter<IMainView>`.

That 2nd point is a really important piece of glue between the Form and the Presenter as it permits you to call members (properties and methods) on the View.

And that completes the circle in terms of how you bind a View to a Presenter _by convention_.
