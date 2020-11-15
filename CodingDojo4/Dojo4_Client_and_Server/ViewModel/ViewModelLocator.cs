using GalaSoft.MvvmLight.Ioc;


namespace Server.ViewModel
{
    public class ViewModelLocator
    {

        public ViewModelLocator()
        {
                    
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main => SimpleIoc.Default.GetInstance<MainViewModel>();

    }
}
