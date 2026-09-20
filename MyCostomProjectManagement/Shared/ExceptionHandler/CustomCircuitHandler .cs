using BackEnd.Data.DB;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.EntityFrameworkCore;

namespace MyCostomProjectManagement.Shared.ExceptionHandler
{
    public class CustomCircuitHandler : CircuitHandler
    {
        private readonly ILogger<CustomCircuitHandler> _logger;
        private readonly IDbContextFactory<Context> _dbContextFactory;

        public CustomCircuitHandler(ILogger<CustomCircuitHandler> logger, IDbContextFactory<Context> dbContextFactory)
        {
            _logger = logger;
            _dbContextFactory = dbContextFactory;
        }

        //public override Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
        //{
        //    _logger.LogInformation("🟢 Circuit با شناسه {CircuitId} باز شد", circuit.Id);
        //    return Task.CompletedTask;
        //}

        public override async Task OnCircuitOpenedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("🟢 Circuit با شناسه {CircuitId} باز شد", circuit.Id);
                // عملیات دیگر
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در OnCircuitOpenedAsync");
                await using var db = await _dbContextFactory.CreateDbContextAsync();
                db.Logs.Add(new BackEnd.Data.DB.Models.Logs(ex.ToString(), ex.Message));
                await db.SaveChangesAsync();
            }
        }

        public override Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _logger.LogInformation("🔴 Circuit با شناسه {CircuitId} بسته شد", circuit.Id);
            return Task.CompletedTask;
        }

        public override Task OnConnectionDownAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _logger.LogWarning("📉 اتصال Circuit {CircuitId} قطع شد", circuit.Id);
            return Task.CompletedTask;
        }

        public override Task OnConnectionUpAsync(Circuit circuit, CancellationToken cancellationToken)
        {
            _logger.LogInformation("📈 اتصال Circuit {CircuitId} برقرار شد", circuit.Id);
            return Task.CompletedTask;
        }
    }
}
