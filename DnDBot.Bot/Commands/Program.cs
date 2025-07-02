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

/// <summary>
/// Classe principal que inicia e configura o bot do Discord.
/// </summary>
class Program
{
    private static DiscordSocketClient _cliente;
    private static InteractionService _interactionService;
    private static IServiceProvider _services;
    private static string _token = Environment.GetEnvironmentVariable("DISCORD_TOKEN");

    // Substitua pelo ID do servidor de testes onde os comandos slash serão registrados
    private static readonly ulong GUILD_ID = 1388541192806989834;

    /// <summary>
    /// Ponto de entrada principal do programa.
    /// </summary>
    public static Task Main(string[] args) => new Program().IniciarAsync();

    /// <summary>
    /// Inicializa o bot, configura serviços, eventos e registra comandos.
    /// </summary>
    public async Task IniciarAsync()
    {
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

        // Registro de serviços utilizados no bot
        _services = new ServiceCollection()
            .AddSingleton<RolagemDadosService>()
            .AddSingleton<FormatadorMensagemService>()
            .AddSingleton<RacasService>()
            .AddSingleton<ClassesService>()
            .AddSingleton<AntecedentesService>()
            .AddSingleton<AlinhamentosService>()
            .AddSingleton<FichaService>()
            .AddSingleton<DistribuicaoAtributosService>()
            .AddSingleton<DistribuicaoAtributosHandler>()
            .BuildServiceProvider();

        RegistrarEventos();

        if (string.IsNullOrWhiteSpace(_token))
        {
            Console.WriteLine("❌ Token não encontrado. Configure a variável de ambiente DISCORD_TOKEN.");
            return;
        }

        await _cliente.LoginAsync(TokenType.Bot, _token);
        await _cliente.StartAsync();

        // Carrega e registra os módulos de comando
        await _interactionService.AddModulesAsync(Assembly.GetExecutingAssembly(), _services);

        // Mantém o bot em execução indefinidamente
        await Task.Delay(-1);
    }

    /// <summary>
    /// Registra eventos como Ready e InteractionCreated.
    /// </summary>
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

    /// <summary>
    /// Loga mensagens no console com severidade e origem.
    /// </summary>
    private static Task LogAsync(LogMessage log)
    {
        Console.WriteLine($"[{log.Severity}] {log.Source}: {log.Message}");
        if (log.Exception != null)
            Console.WriteLine($"❗ Exceção: {log.Exception}");
        return Task.CompletedTask;
    }
}
