using BackEnd.Shared.DataShared;

namespace BackEnd.Data.Entities.FAQ
{
    public class FAQ : BaseEntity
    {
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Sequense { get; set; }
        public bool IsActive { get; set; }
        
        public FAQ(string question, string answer, int sequense, bool isActive)
        {
            Question = question;
            Answer = answer;
            Sequense = sequense;
            IsActive = isActive;
        }
        public void Edit(string question, string answer, int sequense, bool isActive)
        {
            Question = question;
            Answer = answer;
            Sequense = sequense;
            IsActive = isActive;
        }

    }
}
