namespace TastyTellusBackend.Model
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string Intro { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; } // formatera på bästa sätt? med eller utan ing. separat?
        public int NumOfLikes { get; set; } = 0;
        public string SourceURL { get; set; }

        //public string[] Comments { get; set; } = null!; // ? LIST

        //public string[] Ingredients { get; set; } // TODO: add ingredients  LIST
        //public int NumOfServings { get; set; }
    }
}