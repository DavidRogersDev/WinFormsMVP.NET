using System;
using Autofac.Core;

namespace WinFormsMVP.NET.Autofac
{
    public interface IPresenterIdentificationStrategy
    {
        Service ServiceForPresenterName(string presenterName);
        Service ServiceForPresenterType(Type presenterType);
    }
}
