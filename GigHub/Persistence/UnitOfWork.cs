using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GigHub.Models;
using GigHub.Repositories;

namespace GigHub.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public GigRepository Gig { get; private set; }
        public GenreRepository Genre { get; private set; }
        public AttendanceRepository Attendance { get; private set; }
        public FollowingRepository Following { get; private set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Gig = new GigRepository(_context);
            Genre = new GenreRepository(_context);
            Attendance = new AttendanceRepository(_context);
            Following = new FollowingRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}