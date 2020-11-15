using GalaSoft.MvvmLight.Ioc;


namespace Client.ViewModel
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
