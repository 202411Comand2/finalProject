namespace ClusterService.BLL
{
    public class GetSearchClusterDto
    {
        public  int ClusterId { get; set; }
         
        public List<Info> InfoPosition { get; set; } = new List<Info>();


        public class Info 
        {
            public int SearchClusterId { get; set; }

            public string? SearchClusterKeyWord { get; set; }

            public Info(int searchClusterId, string? searchClusterKeyWord)
            {
                SearchClusterId = searchClusterId;
                SearchClusterKeyWord = searchClusterKeyWord;
            }
        } 
    }
}
