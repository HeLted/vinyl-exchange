namespace VinylExchange.Data.Common.Models
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;

    #endregion

    public abstract class BaseModel
    {
        public BaseModel()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
    }
}