using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Persistence.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gigs = new GigRepository(_context);
            Genres = new GenreRepository(_context);
            Attendances = new AttendanceRepository(_context);
            Followings = new FollowingRepository(_context);
        }

        public IAttendanceRepository Attendances { get; }
        public IFollowingRepository Followings { get; }
        public IGenreRepository Genres { get; }
        public IGigRepository Gigs { get; }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}