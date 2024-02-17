namespace AutoStop;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
        BtnLoginReg.Clicked += async (o, e) => await Navigation.PopAsync();
    }

    private void BtnLoginRegClick(object sender, EventArgs e)
    {

    }

    private void BtnSignUpRegClick(object sender, EventArgs e)
    {

    }
}