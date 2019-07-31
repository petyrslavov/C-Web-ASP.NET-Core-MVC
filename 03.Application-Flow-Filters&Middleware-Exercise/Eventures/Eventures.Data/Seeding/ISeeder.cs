using System;
using System.Collections.Generic;
using System.Text;

namespace Eventures.Data.Seeding
{
    public interface ISeeder
    {
        void Seed(EventuresDbContext context);
    }
}
