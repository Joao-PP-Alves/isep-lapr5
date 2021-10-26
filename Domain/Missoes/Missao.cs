using DDDNetCore.Domain.Shared;
using System.Collections.Generic;
using DDDNetCore.Domain.Missoes;

namespace DDDNetCore.Domain.Missoes
{
    {
        //[Required]
        public int grauDificuldade {get; private set;}

        //[Required]
        public bool estado {get; private set; }

        public bool Active {get; private set;}

        private Missao()
        {
            this.Active = true;
        }

        public Missao(int grauDificuldade, bool estado)
        {
            this.grauDificuldade = grauDificuldade;
            this.estado = estado;
            this.Active = true;
        }

        public void ChangeGrauDificuldade(int grauDificuldade){
            if(!this.Active){
                throw new BusinessRuleValidationException("It is not possible to make changes to an inactive product.");
            }
            this.grauDificuldade = grauDificuldade;
        }
    }
}