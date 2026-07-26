using BackEnd.Data.DB;
using BackEnd.Data.Entities.ContactUs;
using BackEnd.Data.Entities.ContactUs.Repository;
using BackEnd.Shared.CoreShared.Repository;

namespace BackEnd.Infrastructure.Repositories.ContactUs
{
    public class ContactUsRepository : BaseRepository<Data.Entities.ContactUs.ContactUs>, IContactUsRepository
    {
        public ContactUsRepository(Context context) : base(context)
        {
        }
    }
}
