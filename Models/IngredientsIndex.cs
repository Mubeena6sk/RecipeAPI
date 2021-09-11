using System;
using System.Collections.Generic;

#nullable disable

namespace RecipeAPI.Models
{
    public partial class IngredientsIndex
    {
        public int Id { get; set; }
        public int Rid { get; set; }
        public int Iid { get; set; }

        public virtual Ingredient IidNavigation { get; set; }
        public virtual Recipe RidNavigation { get; set; }
    }
}
