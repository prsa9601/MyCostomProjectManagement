using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Shared.DataShared
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; protected set; }
        public DateTime CreationDate { get; protected set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
    }
}
