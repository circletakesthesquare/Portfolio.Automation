namespace API.Tests.Utilities
{
    public static class Endpoints
    {
        // Posts endpoints
        public static string GetPost(int id) => $"/posts/{id}";
        public static string CreatePost => "/posts";
        public static string UpdatePost(int id) => $"/posts/{id}";
        public static string DeletePost(int id) => $"/posts/{id}";
    }
}
