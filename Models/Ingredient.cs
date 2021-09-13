using System;
using System.Collections.Generic;

#nullable disable

namespace RecipeAPI.Models
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            IngredientsIndices = new HashSet<IngredientsIndex>();
        }

        public int Iid { get; set; }
        public string Iname { get; set; }

        public virtual ICollection<IngredientsIndex> IngredientsIndices { get; set; }
    }
}
