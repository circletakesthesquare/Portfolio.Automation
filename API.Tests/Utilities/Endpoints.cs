namespace API.Tests.Utilities
{
    public static class Endpoints
    {
        // Posts endpoints
        public static string GetPost(int id) => $"/posts/{id}";
        public static string CreatePost => "/posts";
        public static string UpdatePost(int id) => $"/posts/{id}";
        public static string DeletePost(int id) => $"/posts/{id}";


        // Comments endpoints
        public static string GetComment(int id) => $"/comments/{id}";
        public static string GetAllComments => "/comments";
        public static string GetCommentsForPost(int postId) => $"/posts/{postId}/comments";
        public static string GetCommentsByPostId(int postId) => $"/comments?postId={postId}";
        public static string GetCommentsByEmail(string email) => $"/comments?email={email}";
        public static string CreateComment => "/comments";
        public static string UpdateComment(int id) => $"/comments/{id}";
        public static string DeleteComment(int id) => $"/comments/{id}";



        // Albums endpoints
        public static string GetAlbum(int id) => $"/albums/{id}";
        public static string GetAllAlbums => "/albums";
        public static string GetAlbumsByUserId(int userId) => $"/albums?userId={userId}";
        public static string CreateAlbum => "/albums";
        public static string UpdateAlbum(int id) => $"/albums/{id}";
        public static string DeleteAlbum(int id) => $"/albums/{id}";
    }
}
