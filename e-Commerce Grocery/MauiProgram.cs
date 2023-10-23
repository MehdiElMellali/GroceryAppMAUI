using CommunityToolkit.Maui;
using e_Commerce_Grocery.Interfaces;
using e_Commerce_Grocery.Pages;
using e_Commerce_Grocery.Services;
using e_Commerce_Grocery.ViewModels;
using Microsoft.Extensions.Logging;
using static System.Net.WebRequestMethods;

namespace e_Commerce_Grocery;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
			.UseMauiCommunityToolkit();

		builder.Services.AddSingleton<IPlatformHttpMessageHandler>(sp =>
		{
#if ANDROID
		return new Platforms.Android.Resources.AndroidHttpMessageHandler();
#else
            return new Platforms.iOS.IosHttpMessageHandler();
#endif
        });

		builder.Services.AddHttpClient(Constants.AppConstants.httpClientName, httpClient =>
		{
			var baseAdress = DeviceInfo.Platform == DevicePlatform.Android
							? "https://10.0.2.2:12345" : "https://localhost:12345";
            httpClient.BaseAddress = new Uri(baseAdress);
        })
			.ConfigureHttpMessageHandlerBuilder(configureBuilder =>
			{
                IPlatformHttpMessageHandler platformHttpMessageHandler = configureBuilder.Services.GetRequiredService<IPlatformHttpMessageHandler>();

				configureBuilder.PrimaryHandler = platformHttpMessageHandler.GetHttpMessageHandler();
			});

        builder.Services.AddSingleton<CategoryService>();
        builder.Services.AddTransient<OffersService>();
        builder.Services.AddSingleton<ProductsService>();

        builder.Services.AddSingleton<HomePageViewModel>();
        builder.Services.AddSingleton<CartViewModel>();

        builder.Services.AddSingleton<HomePage>();
        builder.Services.AddTransient<CartPage>();
        builder.Services.AddTransient<CategoriesPage>();

        builder.Services.AddTransientWithShellRoute<CategoryProductsPage, CategoryProductsViewModel>(nameof(CategoryProductsPage));
#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
