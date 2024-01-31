namespace AutoStop
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 666)
                CounterBtn.Text = $"Clicked {count} time";
                
            else
                CounterBtn.Text = $"Clicked {count} day";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
