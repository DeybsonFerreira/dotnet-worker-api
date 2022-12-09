namespace app.Common.Models
{
    public class MessageModel
    {
        public int Id { get; private set; }
        public string Text { get; private set; }
        public DateTime Created { get => DateTime.Now; }

        public MessageModel(int id, string text)
        {
            this.Id = id;
            this.Text = text;
        }
    }
}