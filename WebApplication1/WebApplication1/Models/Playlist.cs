﻿namespace WebApplication1.Models
{
    public class Playlist
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Song>? Songs { get; set; }
    }
}
