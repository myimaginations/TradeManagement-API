using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstrumentsController : ControllerBase
    {
        private readonly TradeService _tradeService;

        public InstrumentsController(TradeService tradeService)
        {
            _tradeService = tradeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Instrument>>> GetAll()
        {
            var instruments = await _tradeService.GetAllInstrumentsAsync();
            return Ok(instruments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Instrument>> GetById(string id)
        {
            var instrument = await _tradeService.GetInstrumentByIdAsync(id);
            if (instrument == null) return NotFound($"Instrument with id {id} not found.");
            return Ok(instrument);
        }

        [HttpPost]
        public async Task<ActionResult<Instrument>> Create(Instrument instrument)
        {
            var created = await _tradeService.CreateInstrumentAsync(instrument);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Instrument instrument)
        {
            var updated = await _tradeService.UpdateInstrumentAsync(id, instrument);
            if (!updated) return NotFound($"Instrument with id {id} not found.");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var deleted = await _tradeService.DeleteInstrumentAsync(id);
            if (!deleted) return NotFound($"Instrument with id {id} not found.");
            return NoContent();
        }
    }
}
