﻿namespace Infrastructure.MediatR.DocumentTypes
{
    public class GetDocumentTypeByIdResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}