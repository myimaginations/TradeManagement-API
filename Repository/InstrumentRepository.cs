using TradeManagement.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TradeManagement.Repository
{
    public class InstrumentRepository : IInstrumentRepository
    {
        private readonly IMongoCollection<Instrument> _instruments;

        public InstrumentRepository(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _instruments = database.GetCollection<Instrument>(settings.Value.InstrumentsCollectionName);
        }

        public async Task<List<Instrument>> GetAllInstrumentsAsync() =>
            await _instruments.Find(i => true).ToListAsync();

        public async Task<Instrument?> GetInstrumentByIdAsync(string id) =>
            await _instruments.Find(i => i.Id == id).FirstOrDefaultAsync();

        public async Task<Instrument> CreateInstrumentAsync(Instrument instrument)
        {
            await _instruments.InsertOneAsync(instrument);
            return instrument;
        }

        public async Task<bool> UpdateInstrumentAsync(string id, Instrument instrument)
        {
            var result = await _instruments.ReplaceOneAsync(i => i.Id == id, instrument);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteInstrumentAsync(string id)
        {
            var result = await _instruments.DeleteOneAsync(i => i.Id == id);
            return result.DeletedCount > 0;
        }
    }
}
