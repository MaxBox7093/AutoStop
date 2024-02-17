namespace AutoStop
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BtnSignUp.Clicked += async (o, e) => await Navigation.PushAsync(new RegistrationPage());
        }
        
        private void BtnLoginClick(object sender, EventArgs e)
        {

        }

        private void BtnSignUpClick(object sender, EventArgs e)
        {

        }
    }
}
