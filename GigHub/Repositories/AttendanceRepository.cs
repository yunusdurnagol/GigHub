﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using GigHub.Models;

namespace GigHub.Repositories
{
    public class AttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Attendance GetAttendance(int gigId, string userId)
        {
            return _context.Attendances
                  .SingleOrDefault(a => a.GigId == gigId && a.AttendeeId == userId);
        }


        public IEnumerable<Attendance> GetFutureAttendances(string userId)
        {
            return _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Gig.DateTime > DateTime.Now)
                .Include(a => a.Gig)
                .ToList();
        }
    }
}