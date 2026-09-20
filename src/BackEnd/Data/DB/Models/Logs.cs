using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BackEnd.Data.DB.Models
{
    public class Logs
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string Exception { get; set; }
        public string Message { get; set; }

        public Logs(string exception, string message)
        {
            CreationDate = DateTime.Now;
            Exception = exception;
            Message = message;
        }

    }
}
