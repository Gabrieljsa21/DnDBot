using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using DnDBot.Application.Services;
using Microsoft.Extensions.DependencyInjection; // Para Injeção de Dependência (DI)
using System;
using System.Threading.Tasks;

class Program
{
    private static DiscordSocketClient _cliente;                   // Cliente do Discord para conexão e eventos
    private static InteractionService _interactionService;         // Gerencia comandos slash e interações
    private static IServiceProvider _services;                      // Container para DI
    private static string _token = Environment.GetEnvironmentVariable("DISCORD_TOKEN"); // Token do bot
    private static readonly ulong GUILD_ID = 1388541192806989834;      // ID do Discord para registrar comandos


    public static Task Main(string[] args) => new Program().IniciarAsync();

    public async Task IniciarAsync()
    {
        var config = new DiscordSocketConfig
        {
            GatewayIntents = GatewayIntents.Guilds                 // Permite acesso aos servidores
                            | GatewayIntents.GuildMessages          // Receber mensagens dos canais
                            | GatewayIntents.MessageContent         // Ler o conteúdo das mensagens
        };

        _cliente = new DiscordSocketClient(config);                // Inicializa o cliente Discord
        _interactionService = new InteractionService(_cliente.Rest); // Inicializa serviço de interações (slash commands)

        // Registra serviços para Injeção de Dependência (aqui só o RolagemDadosService)

        _services = new ServiceCollection()
            .AddSingleton<RolagemDadosService>()
            .AddSingleton<FormatadorMensagemService>() 
            .BuildServiceProvider();


        RegistrarEventos(); // Agrupamento limpo dos Eventos do bot

        if (string.IsNullOrWhiteSpace(_token))                    // Valida se token está configurado
        {
            Console.WriteLine("Token não encontrado. Configure a variável de ambiente DISCORD_TOKEN.");
            return;
        }

        await _cliente.LoginAsync(TokenType.Bot, _token);          // Login do bot no Discord
        await _cliente.StartAsync();                                // Inicia a conexão

        await _interactionService.AddModulesAsync(typeof(Program).Assembly, _services); // Carrega os comandos



        await Task.Delay(-1);                                       // Mantém o programa rodando indefinidamente
    }

    /// <summary>
    /// Agrupa os eventos e handlers do bot.
    /// </summary>
    private void RegistrarEventos()
    {
        _cliente.Log += LogAsync;                                  // Evento para logs no console
        _cliente.Ready += ReadyAsync;                              // Evento disparado quando o bot conecta

        // Evento para processar comandos slash (interações)
        _cliente.InteractionCreated += async interaction =>
        {
            var contexto = new SocketInteractionContext(_cliente, interaction);
            await _interactionService.ExecuteCommandAsync(contexto, _services);
        };
    }
    /// <summary>
    /// Evento disparado quando o bot está pronto.
    /// </summary>
    private static async Task ReadyAsync()
    {
        Console.WriteLine($"✅ Bot conectado como {_cliente.CurrentUser}");

        // Registra os comandos imediatamente no servidor
        await _interactionService.RegisterCommandsToGuildAsync(GUILD_ID);

        Console.WriteLine("📦 Comandos registrados no servidor.");
    }

    /// <summary>
    /// Exibe logs do bot no console.
    /// </summary>
    private static Task LogAsync(LogMessage log)
    {
        Console.WriteLine(log);
        return Task.CompletedTask;
    }

    // Código legado de comandos por texto — mantido para referência
    /*
    private static async Task MessageReceivedAsync(SocketMessage mensagem)
    {
        if (mensagem.Author.IsBot) return;

        if (mensagem.Content.StartsWith("/roll"))
        {
            string[] partes = mensagem.Content.Split(' ', 2);
            if (partes.Length < 2)
            {
                await mensagem.Channel.SendMessageAsync("Uso correto: `/roll 1d20+5`");
                return;
            }

            var resultado = _servicoRolagemDados.Rolar(partes[1]);

            if (resultado == null)
            {
                await mensagem.Channel.SendMessageAsync("Formato inválido. Exemplo válido: `2d6+1`");
                return;
            }

            var (total, detalhes) = resultado.Value;
            await mensagem.Channel.SendMessageAsync($"🎲 Você rolou `{partes[1]}`: **{detalhes} = {total}**");
        }
    }
    */
}
