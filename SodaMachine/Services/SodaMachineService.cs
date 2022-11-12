using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SodaMachine.Services
{
    internal class SodaMachineService : IHostedService
    {
        private readonly ILogger<SodaMachineService> _logger;
        private readonly SodaMachineState _machine;

        internal SodaMachineService(ILogger<SodaMachineService> logger, SodaMachineState state)
        {
            _logger = logger;
            _machine = state;
        }

        /// <inheritdoc/>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Service} is running.", nameof(SodaMachineService));

            SodaMachineInterface.Run(_machine).GetAwaiter().GetResult();
            
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Service} is stopping.", nameof(SodaMachineService));
            return Task.CompletedTask;
        }
    }
}
