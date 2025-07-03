using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using DnDBot.Application.Services;
using DnDBot.Application.Services.Antecedentes;
using DnDBot.Application.Services.Distribuicao;
using DnDBot.Bot.Commands.Ficha;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

using DnDBot.Application.Data; // Namespace onde está o DbContext
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

class Program
{
    private static DiscordSocketClient _cliente;
    private static InteractionService _interactionService;
    private static IServiceProvider _services;
    private static string _token;

    private static readonly ulong GUILD_ID = 1388541192806989834;

    public static Task Main(string[] args) => new Program().IniciarAsync();

    public async Task IniciarAsync()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        _token = Environment.GetEnvironmentVariable("DISCORD_TOKEN");

        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.Guilds
                            | GatewayIntents.GuildMessages
                            | GatewayIntents.MessageContent
        };

        _cliente = new DiscordSocketClient(config);
        _interactionService = new InteractionService(_cliente.Rest);

        _cliente.Log += LogAsync;
        _interactionService.Log += LogAsync;

        // Registrar serviços com injeção de dependência
        var services = new ServiceCollection()

        .AddDbContext<DnDBotDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DnDBotDatabase")))

        .AddScoped<FichaService>()
        .AddScoped<RacasService>()
        .AddScoped<ClassesService>()
        .AddScoped<AntecedentesService>()
        .AddScoped<AlinhamentosService>()
        .AddScoped<DistribuicaoAtributosService>()
        .AddScoped<DistribuicaoAtributosHandler>()
        .AddSingleton<RolagemDadosService>()
        .AddSingleton<FormatadorMensagemService>()

        // Adicione GeracaoDeDadosService aqui
        .AddScoped<GeracaoDeDadosService>()

        .AddSingleton(_cliente)
        .AddSingleton(_interactionService)

        .AddSingleton<IConfiguration>(configuration)

        .BuildServiceProvider();

        _services = services;

        RegistrarEventos();

        if (string.IsNullOrWhiteSpace(_token))
        {
            Console.WriteLine("❌ Token não encontrado. Configure a variável de ambiente DISCORD_TOKEN.");
            return;
        }

        // Executa criação de tabelas e população de dados base antes de iniciar o bot
        await CriarBancoEDadosBaseAsync();

        await _cliente.LoginAsync(TokenType.Bot, _token);
        await _cliente.StartAsync();

        await _interactionService.AddModulesAsync(Assembly.GetExecutingAssembly(), _services);

        await Task.Delay(-1);
    }

    private async Task CriarBancoEDadosBaseAsync()
    {
        try
        {
            using var scope = _services.CreateScope();
            var geracaoService = scope.ServiceProvider.GetRequiredService<GeracaoDeDadosService>();

            Console.WriteLine("⏳ Criando tabelas no banco (se necessário)...");
            await geracaoService.CriarTabelasAsync();

            Console.WriteLine("⏳ Populando dados base...");
            await geracaoService.PopularDadosBaseAsync();

            Console.WriteLine("✅ Banco preparado com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Erro ao preparar banco de dados: {ex.Message}");
            throw;
        }
    }

    private void RegistrarEventos()
    {
        _cliente.Ready += async () =>
        {
            Console.WriteLine($"✅ Bot conectado como {_cliente.CurrentUser}");

            try
            {
                await _interactionService.RegisterCommandsToGuildAsync(GUILD_ID);
                Console.WriteLine("📦 Comandos slash registrados no servidor.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro ao registrar comandos: {ex.Message}");
            }
        };

        _cliente.InteractionCreated += async interaction =>
        {
            var contexto = new SocketInteractionContext(_cliente, interaction);
            var result = await _interactionService.ExecuteCommandAsync(contexto, _services);

            if (!result.IsSuccess)
                Console.WriteLine($"⚠️ Erro ao executar comando: {result.ErrorReason}");
        };
    }

    private static Task LogAsync(LogMessage log)
    {
        Console.WriteLine($"[{log.Severity}] {log.Source}: {log.Message}");
        if (log.Exception != null)
            Console.WriteLine($"❗ Exceção: {log.Exception}");
        return Task.CompletedTask;
    }
}
