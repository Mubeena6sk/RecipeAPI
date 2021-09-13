using System;
using System.Collections.Generic;

#nullable disable

namespace RecipeAPI.Models
{
    public partial class Recipe
    {
        public Recipe()
        {
            IngredientsIndices = new HashSet<IngredientsIndex>();
        }

        public int Rid { get; set; }
        public string Rname { get; set; }
        public string Instructions { get; set; }

        public virtual ICollection<IngredientsIndex> IngredientsIndices { get; set; }
    }
}
