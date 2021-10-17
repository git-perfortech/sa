using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MediatRCORSTrial.Core.Entity
{
    public class EntityBase<T>
    {
        public EntityBase()
        {
            InsertDate = DateTime.Now;
        }

        public virtual void Update()
        {
            this.UpdateDate = DateTime.Now;
        }

        public virtual void Activate()
        {
            this.IsActive = true;
            this.InsertDate = DateTime.Now;
        }

        public virtual void Passivate()
        {
            this.IsActive = false;
            this.UpdateDate = DateTime.Now;
        }

        public virtual void Delete()
        {
            this.IsDeleted = true;
            this.IsActive = false;
            this.UpdateDate = DateTime.Now;
        }

        public virtual void SetRowGuid(Guid guid)
        {
            this.RowGuid = guid;
        }

        [Key]
        public T Id { get; protected set; }
        public DateTime InsertDate { get; protected set; }
        public DateTime? UpdateDate { get; protected set; }
        public bool IsActive { get; protected set; }
        public bool IsDeleted { get; protected set; }
        public Guid RowGuid { get; protected set; }

    }
}
