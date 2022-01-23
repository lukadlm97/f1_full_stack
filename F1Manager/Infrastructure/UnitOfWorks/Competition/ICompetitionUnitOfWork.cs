using Domain.RacingChampionship;
using System;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWorks.Competition
{
    public interface ICompetitionUnitOfWork : IDisposable
    {
        IRacingChampionshipRepository CompetitionRepository { get; set; }

        Task<int> Commit();
    }
}