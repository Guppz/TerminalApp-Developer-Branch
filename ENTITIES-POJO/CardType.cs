﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES_POJO
{
    public class CardType : BaseEntity
    {
        public int IdCardType { get; set; }
        public string CardName { get; set; }

        public CardType() {

        }
    }
} 
