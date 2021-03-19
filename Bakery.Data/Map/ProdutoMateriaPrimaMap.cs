using Bakery.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bakery.Data.Map
{
    public class ProdutoMateriaPrimaMap : IEntityTypeConfiguration<ProdutoMateriaPrima>
    {
        public void Configure(EntityTypeBuilder<ProdutoMateriaPrima> builder)
        {
        }
    }
}
