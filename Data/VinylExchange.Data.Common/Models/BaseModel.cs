namespace VinylExchange.Data.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseModel
    {
        public BaseModel()
        {
            Id = Guid.NewGuid();
        }

        [Key] public Guid Id { get; set; }
    }
}