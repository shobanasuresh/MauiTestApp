namespace MauiTestApp;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
	}

	private async void OnCounterClicked(object sender, EventArgs e)
	{
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            if (await RequestLocationPermission() == PermissionStatus.Granted)
            {
                // permission granted
            }
        });
        return;
    }
    public async Task<PermissionStatus> RequestLocationPermission()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

        if (status == PermissionStatus.Granted)
        {
            await DisplayAlert("Permission", "Location permission has already been granted. Uninstall app. Re-deploy and test again", "OK");
            return status;
        }

        if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
        {
            // Prompt the user to turn on in settings
            // On iOS once a permission has been denied it may not be requested again from the application
            return status;
        }

        if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
        {
            // Prompt the user with additional information as to why the permission is needed
        }

        return await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
    }

}

