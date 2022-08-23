using CaseItau.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CaseItau.API.Domain.Common.Interfaces
{
    public interface IitauDbContext
    {
        DbSet<Fundo> Fundos { get; set; }
        DbSet<TipoFundo> TipoFundos { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
