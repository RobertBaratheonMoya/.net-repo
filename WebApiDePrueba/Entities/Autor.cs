﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDePrueba.Entities
{
    public class Autor
    {
        public int Id { get;  set; }
        [Required]
        public String Nombre { get; set; }
        public List<Libro> Libros { get; set; }
    }
}
