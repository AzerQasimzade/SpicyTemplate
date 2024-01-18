using Microsoft.EntityFrameworkCore;
using SpicyTemplate.DAL;

namespace SpicyTemplate.Services
{
    public class _LayoutService
    {
        private readonly AppDbContext _context;

        public _LayoutService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Dictionary<string,string>> GetSettingAsync()
        {
            Dictionary<string, string> setting = await _context.Settings.ToDictionaryAsync(s => s.Key, s => s.Value);
            return setting;
        }
    }
}
