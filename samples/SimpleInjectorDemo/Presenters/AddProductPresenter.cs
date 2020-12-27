using SimpleInjectorDemo.Services;
using WinFormsMVP.NET;

namespace SimpleInjectorDemo.Presenters
{
    public class AddProductPresenter : Presenter<IAddProductView>
    {
        private readonly IOrdersService _ordersService;

        public AddProductPresenter(IAddProductView view, IOrdersService ordersService) 
            : base(view)
        {
            _ordersService = ordersService;

            View.AddProductEvent += View_AddProductEvent;
        }

        private void View_AddProductEvent(object sender, Services.DataTransferObjects.AddProductDto dto)
        {
            var opResult = _ordersService.AddOrder(dto);

            View.NotifyOpResult(opResult);
        }
    }
}
