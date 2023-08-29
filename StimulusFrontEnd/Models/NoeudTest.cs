namespace StimulusFrontEnd.Models
{
    public class NoeudTest
    {
        private string name, description;
        private int id;
        private int? idParent;

        public NoeudTest(string name, string description, int id)
        {
            this.name = name;
            this.description = description;
            this.id = id;

        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public int? IdParent
        {
            get { return idParent; }
            set { idParent = value; }

        }
    }
}
