using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_worker.Data
{
    [Table("Messages")]
    public partial class Messages
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }
        public string Text { get; private set; }
        public bool Read { get; private set; }

        public Messages(int id, string text, bool read)
        {
            this.Id = id;
            this.Text = text;
            this.Read = Read;
        }

    }
}