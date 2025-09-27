using TradeManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeManagement.Repository
{
    public interface IInstrumentRepository
    {
        Task<List<Instrument>> GetAllInstrumentsAsync();
        Task<Instrument?> GetInstrumentByIdAsync(string id);
        Task<Instrument> CreateInstrumentAsync(Instrument instrument);
        Task<bool> UpdateInstrumentAsync(string id, Instrument instrument);
        Task<bool> DeleteInstrumentAsync(string id);
    }
}
