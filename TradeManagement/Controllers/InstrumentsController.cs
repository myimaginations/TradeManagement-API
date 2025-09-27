using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TradeManagement.Models;
using TradeManagement.Services;

namespace TradeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Trader")]
    public class InstrumentsController : ControllerBase
    {
        private readonly ITradeService _tradeService;

        public InstrumentsController(ITradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Trader,User")]
        public async Task<IActionResult> GetAllInstruments()
        {
            var instruments = await _tradeService.GetAllInstrumentsAsync();
            return Ok(instruments);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Trader,User")]
        public async Task<IActionResult> GetInstrumentById(string id)
        {
            var instrument = await _tradeService.GetInstrumentByIdAsync(id);
            if (instrument == null) return NotFound();
            return Ok(instrument);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInstrument([FromBody] Instrument instrument)
        {
            var created = await _tradeService.CreateInstrumentAsync(instrument);
            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInstrument(string id, [FromBody] Instrument instrument)
        {
            var success = await _tradeService.UpdateInstrumentAsync(id, instrument);
            if (!success) return NotFound();
            return Ok(instrument);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstrument(string id)
        {
            var success = await _tradeService.DeleteInstrumentAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
