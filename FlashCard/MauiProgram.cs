using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace FlashCard;

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
			});

		builder.Services.AddDbContext<DatabaseConfig>();

		builder.Services.AddTransient<DeckViewModel>();
		builder.Services.AddTransient<CreateCardViewModel>();
		builder.Services.AddTransient<DetailViewModel>();

		builder.Services.AddTransient<MainPage>();
		builder.Services.AddTransient<DetailPage>();
		builder.Services.AddTransient<CreateCardPage>();

		var dbContext = new DatabaseConfig();
		dbContext.Database.EnsureCreated();
		dbContext.Dispose();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
