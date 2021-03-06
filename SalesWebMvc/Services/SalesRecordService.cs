using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Data;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMvcContext _context;
        public SalesRecordService(SalesWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? mindate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if(mindate.HasValue)
            {
                result = result.Where(x => x.Date >= mindate.Value);
            }

            if(maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);


            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Departament)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<List<IGrouping<Departament,SalesRecord>>> FindByDateGroupingAsync(DateTime? mindate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;
            if (mindate.HasValue)
            {
                result = result.Where(x => x.Date >= mindate.Value);
            }

            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Date <= maxDate.Value);


            }

            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Departament)
                .OrderByDescending(x => x.Date)
                .GroupBy(x => x.Seller.Departament)
                .ToListAsync();
        }

    }
}
