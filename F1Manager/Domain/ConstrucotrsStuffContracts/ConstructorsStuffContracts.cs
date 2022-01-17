using Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.ConstrucotrsStuffContracts
{
    public class ConstructorsStuffContracts: DeleteEntity<int>
    {
        public DateTime DateOfSign { get; set; }
        public DateTime? DateOfEnd { get; set; }
        

        [ForeignKey(nameof(ConstructorId))]
        public virtual Constructors.Constructor Constructor { get; set; }
        public int ConstructorId { get; set; }

        [ForeignKey(nameof(TechnicalStuffId))]
        public virtual TechnicalStuff.TechnicalStuff TechnicalStuff { get; set; }
        public int TechnicalStuffId { get; set; }

        [ForeignKey(nameof(TechnicalStuffRoleId))]
        public virtual TechnicalStuffRole.TechnicalStuffRole TechnicalStuffRole { get; set; }
        public int TechnicalStuffRoleId { get; set; }

    }
}
