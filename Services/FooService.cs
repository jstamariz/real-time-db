using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using real_time_db.Db;
using real_time_db.Models;

namespace real_time_db.Services
{
    public class FooService
    {
        private readonly Semaphore _semaphore;
        private readonly FooContext _context;

        public FooService(FooContext context)
        {
            _context = context;
            _semaphore = new Semaphore(1, 1);
        }

        public void Write()
        {
            _semaphore.WaitOne();
            var random = new Random().Next();
            _context.Foos.Add(new Foo() { Bar = $"Bar {random}" });
            _context.SaveChanges();
            _semaphore.Release();
        }

        public Foo Read()
        {
            _semaphore.WaitOne();
            var barCount = _context.Foos.Count();
            var id = new Random().Next(barCount);
            var foo = _context.Foos.Find(id);
            _semaphore.Release();
            return foo;
        }
    }
}